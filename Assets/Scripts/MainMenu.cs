using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public string LevelName;
     public Animation anim;
     public void LoadLevel()
     {
          anim.Play("fadein");
          Invoke("DoSomething", 1f);
     }
     
     void DoSomething()
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
