using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public float bulletDamage = 100f;

    // Part of unity Physics System, automatically called when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.transform.GetComponent<Target>(); // Get the Target script from the object we hit
        if (target != null) // If the object has a Target script
        {
            target.TakeDamage(bulletDamage); // Apply damage
            Debug.Log("Hit " + collision.transform.name);
        }
    }
}
