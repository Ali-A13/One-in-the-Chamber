using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableLevelButton : MonoBehaviour
{
    public int levelIndex; // The level number this button represents
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        int highestLevelWon = PlayerPrefs.GetInt("HighestLevelWon", 0);

        // Disable the button if the level is locked
        if (levelIndex > highestLevelWon + 1)
        {
            button.interactable = false;
        }
    }
}
