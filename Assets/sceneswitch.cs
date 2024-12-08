using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class sceneswitch : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector playableDirector;
    
    void OnEnable()
    {

        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    void OnDisable()
    {

        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineStopped;
        }
    }

    // This method is called when the Timeline stops
    void OnTimelineStopped(PlayableDirector director)
    {


        SceneManager.LoadScene("Western_Env");
    }

}
