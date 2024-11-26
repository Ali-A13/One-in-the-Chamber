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
          Application.Quit();
     }
}
