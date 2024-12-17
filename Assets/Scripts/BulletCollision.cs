using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    public float bulletDamage = 100f;
    public bool isDraw = false;
    private Gun player;
    private EnemyAIShoot enemy;
    private Target target;

    private Rigidbody bulletRigidbody;

    private void Start()
    {
        target = FindAnyObjectByType<Target>();
        // Get the Rigidbody component of the bullet
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.transform.GetComponent<Target>(); // Get the Target script from the object we hit
        if (target != null) // If the object has a Target script
        {
            target.TakeDamage(bulletDamage); // Apply damage
            Debug.Log("Hit " + collision.transform.name);
        }

        // Enable gravity for the bullet upon collision
        if (bulletRigidbody != null)
        {
            bulletRigidbody.useGravity = true;
        }
    }

}
