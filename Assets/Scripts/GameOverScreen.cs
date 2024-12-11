using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Western_Env");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
