using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FireGun : MonoBehaviour
{
    private GameObject barrel;
    private Vector3 bulletSpawn;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public float spawnOffset = 0.2f;
    private bool hasShot = false;

    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.Find("Barrel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasShot)
        {
            bulletSpawn = barrel.transform.position;
            //bulletSpawn = new Vector3(bulletSpawn.x + xOffset, bulletSpawn.y + yOffset, bulletSpawn.z + zOffset);
            bulletSpawn += barrel.transform.forward * spawnOffset;
            GameObject currentBullet = Instantiate(bullet, bulletSpawn, barrel.transform.rotation);  // Use appropriate rotation if necessary
     
            Rigidbody bulletRB = currentBullet.GetComponent<Rigidbody>();
            Vector3 direction = currentBullet.transform.forward;
            bulletRB.useGravity = false;
            bulletRB.velocity = direction * bulletSpeed;
            hasShot = true;


        }
    }
}
