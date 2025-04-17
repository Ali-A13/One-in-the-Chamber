using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // This method will be called when a level button is clicked
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
