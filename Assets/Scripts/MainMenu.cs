using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public string LevelName;

     public void LoadLevel()
     {
          SceneManager.LoadScene(LevelName);
     }

     public void QuitGame()
     {
          Application.Quit();

#if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
#endif
     }
}
