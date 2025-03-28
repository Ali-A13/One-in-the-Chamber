using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Enumerator to store level, assigned in each scene
    public enum gameLevel {
        Level1 = 1, 
        Level2 = 2, 
        Level3 = 3, 
        Level4 = 4,
        Level5 = 5,
        Level6 = 6,
        Level7 = 7,
        Level8 = 8,
        Level9 = 9,
        Level10 = 10,
        Level11 = 11,
        Level12 = 12,
        Level13 = 13,
        Level14 = 14,
        Level15 = 15,
        Level16 = 16,
        Level17 = 17,
        Level18 = 18,
        Level19 = 19,
        Level20 = 20,
        Level21 = 21,
        Level22 = 22,
        Level23 = 23,
        Level24 = 24,
        Level25 = 25,
        Level26 = 26,
        Level27 = 27,
        Level28 = 28,
        Level29 = 29,
        Level30 = 30,
        Level31 = 31,
        Level32 = 32,
        Level33 = 33,
        Level34 = 34,
        Level35 = 35,
        Level36 = 36
    };
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
