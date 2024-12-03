using UnityEngine;
using TMPro;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    public GameObject redTumbleweed;
    public GameObject yellowTumbleweed;
    public GameObject greenTumbleweed;
    public TextMeshProUGUI timerText;

    private float timer;
    private bool gameStarted = false;
    private bool greenStarted = false;
    private float greenDelay;

    private static CountDownTimer instance;

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        //hide all tumbleweeds initially
        redTumbleweed.SetActive(false);
        yellowTumbleweed.SetActive(false);
        greenTumbleweed.SetActive(false);
    }

    void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;

            //update the timer display
            if (timerText != null)
                timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer < 3f)
            {
                redTumbleweed.SetActive(true);
                yellowTumbleweed.SetActive(false);
                greenTumbleweed.SetActive(false);
            }
            else if (timer >= 3f && timer < 5f)
            {
                redTumbleweed.SetActive(false);
                yellowTumbleweed.SetActive(true);
                greenTumbleweed.SetActive(false);
                if (!greenStarted)
                {
                    StartCoroutine(StartGreenTumbleweed());
                    greenStarted = true;
                }
            }
            else if (timer >= 5f && timer < (5f + greenDelay))
            {
                redTumbleweed.SetActive(false);
                yellowTumbleweed.SetActive(true);
                greenTumbleweed.SetActive(false);
            }
            else if (timer >= (5f + greenDelay))
            {
                redTumbleweed.SetActive(false);
                yellowTumbleweed.SetActive(false);
                greenTumbleweed.SetActive(true);
            }
        }
    }

    IEnumerator StartGreenTumbleweed()
    {
        greenDelay = Random.Range(0f, 5f);
        yield return new WaitForSeconds(greenDelay);
    }

    public void StartGame()
    {
        gameStarted = true;
        timer = 0f;
        greenStarted = false;
    }
}
