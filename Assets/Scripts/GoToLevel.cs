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
            default:
                nextLevelName = "Western_Env";
                break;
        }
        return nextLevelName;
    }
}
