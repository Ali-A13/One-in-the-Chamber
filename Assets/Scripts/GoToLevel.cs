using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    private string nextLevelName;

    private Scene currentScene;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Get the current scene and its name
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
        //Find level name based on current scene
        nextLevelName = findNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GoToNextLevel());
    }

    IEnumerator GoToNextLevel()
    {
        //Wait on level screen for a bit, then switch
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextLevelName);
    }

    //Finds next level name based on current scene name
    private string findNextLevel()
    {
        switch (sceneName)
        {
            case "Level2Screen":
                nextLevelName = "Level_2";
                break;
            case "Level3Screen":
                nextLevelName = "Level_3";
                break;
            case "Level4Screen":
                nextLevelName = "Level_4";
                break;
            case "Level5Screen":
                nextLevelName = "Level_5";
                break;
            case "Level6Screen":
                nextLevelName = "Level_6";
                break;
            case "Level7Screen":
                nextLevelName = "Level_7";
                break;
            case "Level8Screen":
                nextLevelName = "Level_8";
                break;
            case "Level9Screen":
                nextLevelName = "Level_9";
                break;
            case "Level10Screen":
                nextLevelName = "Level_10";
                break;
            case "Level11Screen":
                nextLevelName = "Level_11";
                break;
            case "Level12Screen":
                nextLevelName = "Level_12";
                break;
            case "Level13Screen":
                nextLevelName = "Level_13";
                break;
            case "Level14Screen":
                nextLevelName = "Level_14";
                break;
            case "Level15Screen":
                nextLevelName = "Level_15";
                break;
            case "Level16Screen":
                nextLevelName = "Level_16";
                break;
            case "Level17Screen":
                nextLevelName = "Level_17";
                break;
            case "Level18Screen":
                nextLevelName = "Level_18";
                break;
            case "Level19Screen":
                nextLevelName = "Level_19";
                break;
            case "Level20Screen":
                nextLevelName = "Level_20";
                break;
            case "Level21Screen":
                nextLevelName = "Level_21";
                break;
            case "Level22Screen":
                nextLevelName = "Level_22";
                break;
            case "Level23Screen":
                nextLevelName = "Level_23";
                break;
            case "Level24Screen":
                nextLevelName = "Level_24";
                break;
            case "Level25Screen":
                nextLevelName = "Level_25";
                break;
            case "Level26Screen":
                nextLevelName = "Level_26";
                break;
            case "Level27Screen":
                nextLevelName = "Level_27";
                break;
            case "Level28Screen":
                nextLevelName = "Level_28";
                break;
            case "Level29Screen":
                nextLevelName = "Level_29";
                break;
            case "Level30Screen":
                nextLevelName = "Level_30";
                break;
            case "Level31Screen":
                nextLevelName = "Level_31";
                break;
            case "Level32Screen":
                nextLevelName = "Level_32";
                break;
            case "Level33Screen":
                nextLevelName = "Level_33";
                break;
            case "Level34Screen":
                nextLevelName = "Level_34";
                break;
            case "Level35Screen":
                nextLevelName = "Level_35";
                break;
            case "Level36Screen":
                nextLevelName = "Level_36";
                break;
            default:
                nextLevelName = "Western_Env";
                break;
        }
        return nextLevelName;
    }
}
