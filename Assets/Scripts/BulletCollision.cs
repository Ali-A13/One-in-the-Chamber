using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;
using System.Collections;

public class BulletCollision : MonoBehaviour
{
    public float bulletDamage = 100f;
    public bool isDraw = false;
    private Target target;
    private RagdollModeOnOff ragdollMode;

    private Rigidbody bulletRigidbody;

    private void Start()
    {
        target = FindAnyObjectByType<Target>();
        // Get the Rigidbody component of the bullet
        bulletRigidbody = GetComponent<Rigidbody>();
        // Assign the reference to RagdollModeOnOff
        //ragdollModeOnOff = FindObjectOfType<RagdollModeOnOff>(); // Adjust this as per your game structure
    }

    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.transform.GetComponent<Target>(); // Get the Target script from the object we hit
        if (target != null) // If the object has a Target script
        {
            target.TakeDamage(bulletDamage); // Apply damage
            Debug.Log("Hit " + collision.transform.name);

            ragdollMode = collision.transform.GetComponent<RagdollModeOnOff>(); // Get the RagdollModeOnOff script from the object we hit
            if (ragdollMode != null)
            {
                ragdollMode.RagdollModeOn(); // Turn on ragdoll during bullet collision
            }
            else
            {
                Debug.LogWarning("RagdollModeOnOff component is not assigned.");
            }

        }

        // Enable gravity for the bullet upon collision
        if (bulletRigidbody != null)
        {
            bulletRigidbody.useGravity = true;
        }
    }

}
