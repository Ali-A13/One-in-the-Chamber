using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class sceneswitch : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector playableDirector;
    public GameObject model;
    public Animation anima;
    public string level;
    public string animname;
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
        model.SetActive(false);
        anima.Play(animname);
        Invoke("DoSomething", 1f); 
    }
        void DoSomething()
    {
        SceneManager.LoadScene(level);
    }

}
