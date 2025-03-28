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
            case 5:
                previousLevelName = "Level5Screen";
                break;
            case 6:
                previousLevelName = "Level6Screen";
                break;
            case 7:
                previousLevelName = "Level7Screen";
                break;
            case 8:
                previousLevelName = "Level8Screen";
                break;
            case 9:
                previousLevelName = "Level9Screen";
                break;
            case 10:
                previousLevelName = "Level10Screen";
                break;
            case 11:
                previousLevelName = "Level11Screen";
                break;
            case 12:
                previousLevelName = "Level12Screen";
                break;
            case 13:
                previousLevelName = "Level13Screen";
                break;
            case 14:
                previousLevelName = "Level14Screen";
                break;
            case 15:
                previousLevelName = "Level15Screen";
                break;
            case 16:
                previousLevelName = "Level16Screen";
                break;
            case 17:
                previousLevelName = "Level17Screen";
                break;
            case 18:
                previousLevelName = "Level18Screen";
                break;
            case 19:
                previousLevelName = "Level19Screen";
                break;
            case 20:
                previousLevelName = "Level20Screen";
                break;
            case 21:
                previousLevelName = "Level21Screen";
                break;
            case 22:
                previousLevelName = "Level22Screen";
                break;
            case 23:
                previousLevelName = "Level23Screen";
                break;
            case 24:
                previousLevelName = "Level24Screen";
                break;
            case 25:
                previousLevelName = "Level25Screen";
                break;
            case 26:
                previousLevelName = "Level26Screen";
                break;
            case 27:
                previousLevelName = "Level27Screen";
                break;
            case 28:
                previousLevelName = "Level28Screen";
                break;
            case 29:
                previousLevelName = "Level29Screen";
                break;
            case 30:
                previousLevelName = "Level30Screen";
                break;
            case 31:
                previousLevelName = "Level31Screen";
                break;
            case 32:
                previousLevelName = "Level32Screen";
                break;
            case 33:
                previousLevelName = "Level33Screen";
                break;
            case 34:
                previousLevelName = "Level34Screen";
                break;
            case 35:
                previousLevelName = "Level35Screen";
                break;
            case 36:
                previousLevelName = "Level36Screen";
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
                nextLevelName = "Level5Screen";
                break;
            case 6:
                nextLevelName = "Level6Screen";
                break;
            case 7:
                nextLevelName = "Level7Screen";
                break;
            case 8:
                nextLevelName = "Level8Screen";
                break;
            case 9:
                nextLevelName = "Level9Screen";
                break;
            case 10:
                nextLevelName = "Level10Screen";
                break;
            case 11:
                nextLevelName = "Level11Screen";
                break;
            case 12:
                nextLevelName = "Level12Screen";
                break;
            case 13:
                nextLevelName = "Level13Screen";
                break;
            case 14:
                nextLevelName = "Level14Screen";
                break;
            case 15:
                nextLevelName = "Level15Screen";
                break;
            case 16:
                nextLevelName = "Level16Screen";
                break;
            case 17:
                nextLevelName = "Level17Screen";
                break;
            case 18:
                nextLevelName = "Level18Screen";
                break;
            case 19:
                nextLevelName = "Level19Screen";
                break;
            case 20:
                nextLevelName = "Level20Screen";
                break;
            case 21:
                nextLevelName = "Level21Screen";
                break;
            case 22:
                nextLevelName = "Level22Screen";
                break;
            case 23:
                nextLevelName = "Level23Screen";
                break;
            case 24:
                nextLevelName = "Level24Screen";
                break;
            case 25:
                nextLevelName = "Level25Screen";
                break;
            case 26:
                nextLevelName = "Level26Screen";
                break;
            case 27:
                nextLevelName = "Level27Screen";
                break;
            case 28:
                nextLevelName = "Level28Screen";
                break;
            case 29:
                nextLevelName = "Level29Screen";
                break;
            case 30:
                nextLevelName = "Level30Screen";
                break;
            case 31:
                nextLevelName = "Level31Screen";
                break;
            case 32:
                nextLevelName = "Level32Screen";
                break;
            case 33:
                nextLevelName = "Level33Screen";
                break;
            case 34:
                nextLevelName = "Level34Screen";
                break;
            case 35:
                nextLevelName = "Level35Screen";
                break;
            case 36:
                nextLevelName = "Level36Screen";
                break;
            case 37:
                nextLevelName = "TitleScreen";
                break;
            default:
                nextLevelName = "Western_Env";
                break;
        }
        return nextLevelName;
    }
}
