using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.UI;

public class sceneswitch : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector playableDirector;
    public GameObject model;
    public Animation anima;
    public string level;
    public string animname;
    public TMP_Text skipText;
    public Image mouseClick;

    private void Start()
    {
        StartCoroutine(HideElementsAfterDelay(3f));
        Debug.Log("Start Delay Counter");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DoSomething();
        }
    }

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

    private IEnumerator HideElementsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Hide the text and image
        skipText.gameObject.SetActive(false);
        mouseClick.gameObject.SetActive(false);
        Debug.Log("Delay Counter finished, should hide");
    }

}
