using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.GraphicsBuffer;

public class EnemyAIShoot : MonoBehaviour
{
    public enum difficultyLevel { Easy = 4, Medium = 3, Hard = 2, Dan = 0};
    public difficultyLevel difficulty;

    public GameObject player;
    public GameObject bullet;

    //public float lookDuration = 1.0f;
    public Vector3 lookOffset = new Vector3(0, 0, 0);

    private float unholsterTime;
    private float aimTime;
    private float shootTime;
    private float aimAccuracy;
    public float aimOffsetWeight = 1.0f;

    private Vector3 playerLocation;

    private bool readyToFire = false;
    //private bool dead = false;
    private Coroutine coroutine;

    public float bulletSpeed = 10f;
    public float spawnOffset = 0.2f;
    public float xOffset = 0.0f;
    public float yOffset = 0.0f;
    public float zOffset = 0.0f;
    public AudioSource GunSound;
    [SerializeField] private VisualEffect muzzleFlash;

    private Vector3 startPoint;
    private Vector3 endPoint;
    public Vector3 endOffset;
    public float arcHeight = 5f; // The height of the arc
    public float duration = 2f; // How long it takes to travel the arc




    // Start is called before the first frame update
    void Start()
    {
        //Get difficulty factor from selected difficulty
        float difficultyFactor = (int)difficulty;

        //Aim accuracy between 0 (dead accurate) and difficulty factor, multiplied by a weighted factor
        aimAccuracy = Random.Range(0, difficultyFactor) * aimOffsetWeight;

        //Timings range from difficultyFactor - 1 to difficulty factor (ex: Easy: 3-4 seconds per action)
        unholsterTime = Random.Range(difficultyFactor - 1, difficultyFactor);
        aimTime = Random.Range(difficultyFactor - 1, difficultyFactor);
        shootTime = Random.Range(difficultyFactor - 1, difficultyFactor);

        //Getting Player location
        playerLocation = player.transform.position;

        startPoint = transform.position;
        endPoint = startPoint + endOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //Still need stuff here for waiting until indicator is GO, waiting on indicator



        ////Need proper hitbox before implementing death, waiting on hitbox
        //if (dead)
        //{
        //    StopAllCoroutines();
        //    Debug.Log("You Won!");
        //}

        //After Indicator  is GO
        if (coroutine == null)
        {
            //Start unholster timer
            coroutine = StartCoroutine(WaitToUnholster());
        }

        //After aiming and everything
        if (readyToFire)
        {
            Shoot();
            readyToFire = false;
        }
    }

    IEnumerator LookAtPlayer()
    {
        float timeElasped = 0f;
        float t = 0f;
        Quaternion targetRotation;


        while (timeElasped < aimTime)
        {
            // Increment the time elapsed by the time since the last frame
            timeElasped += Time.deltaTime;

            // Calculate the normalized time (0 to 1)
            t = Mathf.Clamp01(timeElasped / aimTime);

            // Interpolate between start and end positions
            transform.position = Vector3.Lerp(startPoint, endPoint, t);

            targetRotation = Quaternion.LookRotation(playerLocation - endPoint);
            targetRotation *= Quaternion.Euler(lookOffset);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, t * 0.06f);

            yield return null;
        }
    }

    //Called When light is GO
    IEnumerator WaitToUnholster()
    {
        //Waits for unholsterTime
        yield return new WaitForSeconds(unholsterTime);
        Debug.Log("Unholstered");
        //After unholstering, start aim wait
        StartCoroutine(WaitToAim());
    }

    //Called when done unholstering
    IEnumerator WaitToAim()
    {
        yield return StartCoroutine(LookAtPlayer());
        Debug.Log("Aimed");
        //After aiming, start shooting wait
        StartCoroutine(WaitToShoot());
    }

    //Called when done aiming
    IEnumerator WaitToShoot()
    {
        //Wait for shootTime
        yield return new WaitForSeconds(shootTime);
        //Set ready
        readyToFire = true;
        //Smoothing function likely not completely done so now look directly at player
        transform.LookAt(playerLocation);
        //transform.position = gunEndPos;
    }

    void Shoot()
    {
        //Similar to Gun script
        muzzleFlash.Play();
        GunSound.Play();
        Vector3 bulletSpawn = transform.position;
        bulletSpawn = new Vector3(bulletSpawn.x + xOffset, bulletSpawn.y + yOffset, bulletSpawn.z + zOffset);
        bulletSpawn += transform.forward * spawnOffset;

        //Offset rotation by aimAccuracy, defined in Start function
        Quaternion rotationOffset = Quaternion.Euler(aimAccuracy, aimAccuracy, aimAccuracy);
        Quaternion finalRotation = rotationOffset * transform.rotation;

        //Spawn bullet with offsetRotation (leads to different aim based on difficulty)
        GameObject currentBullet = Instantiate(bullet, bulletSpawn, finalRotation);

        //Similar to Gun script
        Rigidbody bulletRB = currentBullet.GetComponent<Rigidbody>();
        Vector3 direction = currentBullet.transform.forward;
        bulletRB.useGravity = false;
        bulletRB.velocity = direction * bulletSpeed;
        Debug.Log("Enemy Shot");
    }
}