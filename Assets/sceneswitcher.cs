using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneSwitcher : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string sceneNameToLoad;

    void OnEnable()
    {
        // Subscribe to the PlayableDirector's stopped event
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineStopped;
        }
    }

    void OnDisable()
    {
        // Unsubscribe when disabled
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineStopped;
        }
    }

    // This method will be called when the timeline stops
    void OnTimelineStopped(PlayableDirector director)
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
