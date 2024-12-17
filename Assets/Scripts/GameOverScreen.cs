using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject nextButton;

    private int previousLevel;
    private string previousLevelName;
    private string nextLevelName;
    private int highestLevelWon;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //retrieve previous level
        previousLevel = PlayerPrefs.GetInt("CurrentLevel");

        //Setting highest level won to 0 if no level cleared
        if (!PlayerPrefs.HasKey("HighestLevelWon"))
        {
            PlayerPrefs.SetInt("HighestLevelWon", 0);
            PlayerPrefs.Save();
        }

        //Retrieve highest Level Won
        highestLevelWon = PlayerPrefs.GetInt("HighestLevelWon");
        Debug.Log(highestLevelWon);

        //Find next level name
        nextLevelName = findNextLevel();
        
        //Enable or disable "next" button based on comparison between highest level won and previous level
        if (highestLevelWon >= previousLevel)
            nextButton.SetActive(true);
        else
            nextButton.SetActive(false);
    }

    public void RestartButton()
    {
        //Find previous level name, then switch to it - with delay
        previousLevelName = findPreviousLevel();
        StartCoroutine(LoadSceneWithDelay(previousLevelName));
    }

    public void MainMenuButton()
    {
        //Load Main menu - with delay
        StartCoroutine(LoadSceneWithDelay("TitleScreen"));
    }

    public void NextLevelButton()
    {
        //Load next level - with delay
        StartCoroutine(LoadSceneWithDelay(nextLevelName));
    }

    //coroutine to load the scene with delay
    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(0.5f); //0.5 second delay
        SceneManager.LoadScene(sceneName);
    }

    //Finds name of previous level
    public string findPreviousLevel()
    {
        switch(previousLevel)
        {
            case 1:
                previousLevelName = "Western_Env";
                break;
            case 2:
                previousLevelName = "Level2Screen";
                break;
            case 3:
                previousLevelName = "Level3Screen";
                break;
            case 4:
                previousLevelName = "Level4Screen";
                break;
            default:
                previousLevelName = "Western_Env";
                break;
        }
        return previousLevelName;
    }

    //Find next level name based on previous level
    public string findNextLevel()
    {
        int nextLevel = previousLevel + 1;
        switch(nextLevel)
        {
            case 2:
                nextLevelName = "Level2Screen";
                break;
            case 3:
                nextLevelName = "Level3Screen";
                break;
            case 4:
                nextLevelName = "Level4Screen";
                break;
            case 5:
                nextLevelName = "TitleScreen";
                break;
            default:
                nextLevelName = "Western_Env";
                break;
        }
        return nextLevelName;
    }
}
