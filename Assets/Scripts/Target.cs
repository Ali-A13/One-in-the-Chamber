using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class Target : MonoBehaviour
{
    public float health = 50f;
    private bool isDead = false;



    void Start()
    {

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
        isDead = true;
        //Destroy(gameObject);
        Debug.Log("Target destroyed");
    }

    public bool GetDead()
    {
        return isDead;
    }

}
