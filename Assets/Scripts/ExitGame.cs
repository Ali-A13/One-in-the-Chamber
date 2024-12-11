using UnityEngine;

public class ExitGame : MonoBehaviour
{
     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.Escape))
          {
               ExitApplication();
          }
     }

     void ExitApplication()
     {
        #if UNITY_EDITOR
                // Exit play mode in the Unity Editor
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Quit the application in a built executable
                Application.Quit();
        #endif
    }
}
