using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public TMP_Text winText;
    public TMP_Text lostText;
    public TMP_Text drawText;
    public bool winLost = false;
    private Gun player;
    private EnemyAIShoot enemy;

    

    void Start()
    {
        winText.enabled = false;
        lostText.enabled = false;
        drawText.enabled = false;
        winLost = false;
        player = FindAnyObjectByType<Gun>(); // Find the Gun script in the scene
        enemy = FindAnyObjectByType<EnemyAIShoot>(); // Find the EnemyAIShoot script in the scene
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy(gameObject);
        Debug.Log("Target destroyed");
    }

    public void playerWin()
    {
        winText.enabled = true;
        enemy.dead = true; //Stops AI from shooting
        winLost = true; // did win
        Debug.Log("Win Text Enabled");
        StartCoroutine(CallLoadSceneAfterDelay());
    }

    public void playerLost()
    {
        lostText.enabled = true;
        player.dead = true; //Stops player from shooting
        winLost = true; // did loose
        Debug.Log("Lost Text Enabled");
        StartCoroutine(CallLoadSceneAfterDelay());
    }

    public void playerDraw()
    {
        drawText.enabled = true;
        Debug.Log("Draw Text Enabled");
        StartCoroutine(CallLoadSceneAfterDelay());
    }

    private void loadEndScene()
    {
        SceneManager.LoadScene("GameEndScreen");
        Debug.Log("End Scene Loaded");
    }

    IEnumerator CallLoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(3);
        loadEndScene();
    }
}
