using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;
using TMPro; // Import TextMeshPro namespace
public class Gun : MonoBehaviour
{

    public GameObject barrel;
    private Vector3 bulletSpawn;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public float spawnOffset = 0.2f;
    public bool hasShot = false;
    public float xOffset = 0.0f;
    public float yOffset = 0.0f;
    public float zOffset = 0.0f;
    public AudioSource GunSound;
    public Camera fpsCam;
    public bool dead = false;
    public TMP_Text tmpText; 

    [SerializeField] private VisualEffect muzzleFlash;

    private Rigidbody bulletRB;

    void Start()
    {
        //barrel = transform.Find("Grips").gameObject;
        tmpText.text = "1";
    }

    // Update is called once per frame
    void Update()
    {
        if (!CountDownTimer.gameEnabled) return; // Disable input until the game starts
        
        // Only shots once if not dead
        if(Input.GetButtonDown("Fire1") && hasShot == false && !dead)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        muzzleFlash.Play();
        GunSound.Play();
        bulletSpawn = barrel.transform.position;
        bulletSpawn = new Vector3(bulletSpawn.x + xOffset, bulletSpawn.y + yOffset, bulletSpawn.z + zOffset);
        bulletSpawn += barrel.transform.forward * spawnOffset;
        GameObject currentBullet = Instantiate(bullet, bulletSpawn, barrel.transform.rotation);  // Use appropriate rotation if necessary

        bulletRB = currentBullet.GetComponent<Rigidbody>();
        Vector3 direction = currentBullet.transform.forward;
        //bulletRB.useGravity = false;
        SetBulletGravity(false);
        bulletRB.velocity = direction * bulletSpeed;
        hasShot = true; // Shoot only once
        tmpText.text = "0";
        Debug.Log("Shot");
    }

    public bool GetShootStatus()
    {
        return hasShot;
    }

    public void SetShootStatus(bool status)
    {
        hasShot = status;
    }

    public void SetBulletGravity(bool status)
    {
        bulletRB.useGravity = status;
    }

}
