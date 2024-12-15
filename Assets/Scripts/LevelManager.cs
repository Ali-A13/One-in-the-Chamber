using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Enumerator to store level, assigned in each scene
    public enum gameLevel {Level1 = 1, Level2 = 2, Level3 = 3, Level4 = 4};
    public gameLevel level;
    private int currentLevel;

    //WinController script
    public WinController WinScript;
    private WinController script;

    private bool playerWon;


    // Start is called before the first frame update
    void Start()
    {
        //Get script
        script = WinScript.GetComponent<WinController>();
        //Get level
        currentLevel = (int)level;
        //Save current level in PlayerPrefs (carried across scenes)
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        //Check if player won from WinController
        playerWon = script.playerWon();
        if (playerWon)
        {
            //If won, save highest won level in PlayerPrefs
            PlayerPrefs.SetInt("HighestLevelWon", currentLevel);
            PlayerPrefs.Save();
        }
    }
}
