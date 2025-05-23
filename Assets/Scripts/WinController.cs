using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    //Ensure you assign the objects that contain the "Target" Script in Unity
    public GameObject player;
    public GameObject enemy;

    //Ensure you assing the guns that contain the "Gun" and "EnemyAIShoot" scripts in unity
    public GameObject playerGun;
    public GameObject enemyGun;

    private Target playerTarget;
    private Target enemyTarget;
    private Gun playerWeapon;
    private EnemyAIShoot enemyWeapon;


    private bool playerWin = false;
    private bool playerLoss = false;
    private bool draw = false;
    public bool gameEnd = false;

    public TMP_Text winText;
    public TMP_Text lostText;
    public TMP_Text drawText;

    //public Animator anim;

    public AudioSource winSound;

    public Camera mainCamera;
    public Camera endCamera;
    private EndCameraController endCamMotion;

    // Start is called before the first frame update
    void Start()
    {
        //Enabling main camera and disabling end camera
        if (mainCamera != null && endCamera != null)
        {
            mainCamera.enabled = true;
            endCamera.enabled = false;
        }

        //Disabling all text
        winText.enabled = false;
        lostText.enabled = false;
        drawText.enabled = false;

        //Getting target scripts 
        playerTarget = player.GetComponent<Target>();
        enemyTarget = enemy.GetComponent<Target>();

        //get gun and ai scripts
        playerWeapon = playerGun.GetComponent<Gun>();
        enemyWeapon = enemyGun.GetComponent<EnemyAIShoot>();

        // make sure winSound is assigned
        winSound = GetComponent<AudioSource>();
        if (winSound == null)
        {
            Debug.LogWarning("No AudioSource found or assigned. Please add one.");
        }

        // Get the EndCameraController component
        endCamMotion = endCamera.GetComponent<EndCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //While game hasn't ended
        if (!gameEnd)
        {
            //Check if player died, if so, set playerLoss
            if (playerTarget.GetDead() == true)
            {
                playerLoss = true;
            }
            //Check if enemy died, if so, set playerWin
            if (enemyTarget.GetDead() == true)
            {
                playerWin = true;
                //anim.SetBool("isdead", true);
            }

            //Check if both are dead (same time), if so, set draw
            if (playerWin && playerLoss)
            {
                draw = true;
                //anim.SetBool("isdead", true);
            }

            //Check if both player and AI have shot
            if ((playerWeapon.GetShootStatus() == true) && (enemyWeapon.GetShootStatus() == true))
            {
                Coroutine coroutine = null;
                if (coroutine == null)
                {
                    //Check if both miss
                    coroutine = StartCoroutine(checkForMiss());
                }
            }

            //Game ended if any of the three occur
            if (playerWin || playerLoss || draw)
                gameEnd = true;

            //If game has ended
            if (gameEnd)
            {
                //Disable Target scripts and stop both player and AI from shooting
                playerTarget.enabled = false;
                enemyTarget.enabled = false;
                playerWeapon.SetShootStatus(true);
                enemyWeapon.SetShootStatus(true);

                //Process end
                CallEndFunctions();
            }
        }
    }

    //Function to see if both players miss
    IEnumerator checkForMiss()
    {
        //Wait for a couple of seconds after both fire
        yield return new WaitForSeconds(3);
        //If no game end conditions occur, set draw
        if (!playerWin && !playerLoss && !draw)
            draw = true;
    }

    //Function to process winner
    void CallEndFunctions()
    {
        //Switch camera to end camera
        switchCamera();

        //Handle each condition accordingly
        if (draw)
            StartCoroutine(handleDraw());
        else if (playerWin)
            StartCoroutine(handlePlayerWin());
        else if (playerLoss)
            StartCoroutine(handlePlayerLoss());

    }

    //Function to handle player win. Shows win text then displays end scene after some time 
    IEnumerator handlePlayerWin()
    {
        enemyGun.SetActive(false);  // Make player gun disappear
        winText.enabled = true;

        // play the 'cha-ching'
        if (winSound != null)
        {
            winSound.Play();
        }

        yield return new WaitForSeconds(3);
        loadEndScene();

    }

    //Function to handle player win. Shows win text then displays end scene after some time 
    IEnumerator handlePlayerLoss()
    {
        //SceneManager.LoadScene("Deathanimation");
        playerGun.SetActive(false);  // Make player gun disappear
        lostText.enabled = true;

        yield return new WaitForSeconds(3);
        loadEndScene();

    }

    //Function to handle draw. Shows draw text then displays end scene after some time 

    IEnumerator handleDraw()
    {
        drawText.enabled = true;
        yield return new WaitForSeconds(3);
        loadEndScene();
    }

    //Function to load the end scene. Does as it says
    private void loadEndScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameEndScreen");
        Debug.Log("End Scene Loaded");
    }

    //Returns when player has won only (not a draw)
    public bool playerWon()
    {
        if (playerWin && !playerLoss)
            return true;
        else
            return false;
    }

    private void switchCamera()
    {
        if (mainCamera != null && endCamera != null)
        {
            mainCamera.enabled = false; // Disable main camera
            endCamera.enabled = true; // Enable end camera
            endCamMotion.StartMovingCamera(); // Start moving the camera
        }
        else
        {
            Debug.LogWarning("No cameras found or assigned. Please add one.");
        }


    }
}

