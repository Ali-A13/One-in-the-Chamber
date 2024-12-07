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

    private void Start()
    {
        player = FindAnyObjectByType<Gun>(); // Find the Gun script in the scene
        enemy = FindAnyObjectByType<EnemyAIShoot>(); // Find the EnemyAIShoot script in the scene
        target = FindAnyObjectByType<Target>();
    }

    private void Update()
    {
        if (player.hasShot && enemy.hasShot && !isDraw)
        {
            StartCoroutine(CallPlayerDrawAfterDelay());
            isDraw = true;
        }
    }

    private void CallPlayerDraw()
    {
        if (!(target.winLost))
        {
            target.playerDraw();
            Debug.Log("Game Draw");
        }
        else
        {
            Debug.Log("Game already won or lost");
        }
    }

    IEnumerator CallPlayerDrawAfterDelay()
    {
        yield return new WaitForSeconds(1);
        CallPlayerDraw();
    }

    // Part of unity Physics System, automatically called when a collision occurs
    void OnCollisionEnter(Collision collision)
    {
        Target target = collision.transform.GetComponent<Target>(); // Get the Target script from the object we hit
        if (target != null) // If the object has a Target script
        {
            target.TakeDamage(bulletDamage); // Apply damage
            Debug.Log("Hit " + collision.transform.name);

            //player looses -> show loose screen
            if (target.CompareTag("Player"))
            {
                target.playerLost();
                Debug.Log("Game Lost");
            }
            else if (target.CompareTag("AI")) //player wins -> show win screen
            {
                target.playerWin();
                Debug.Log("Game Won");
            }

        }
    }
    
}
