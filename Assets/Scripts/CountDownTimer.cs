using UnityEngine;
using TMPro;
using System.Collections;

public class CountDownTimer : MonoBehaviour
{
    public GameObject redTumbleweed;
    public GameObject yellowTumbleweed;
    public GameObject greenTumbleweed;
    public TextMeshProUGUI timerText;

    public float tumbleweedSpeed = 20f; //speed for all tumbleweeds
    private bool gameStarted = false;

    private Vector3 redStartPos;
    private Vector3 yellowStartPos; 
    private Vector3 greenStartPos;

    private enum TumbleweedState { Idle, RedRolling, YellowRolling, GreenRolling, Finished }
    private TumbleweedState currentState = TumbleweedState.Idle;

    void Start()
    {
        //initial positions for the tumbleweeds
        redStartPos = new Vector3(-20f, redTumbleweed.transform.position.y, redTumbleweed.transform.position.z);
        yellowStartPos = new Vector3(-20f, yellowTumbleweed.transform.position.y, yellowTumbleweed.transform.position.z);
        greenStartPos = new Vector3(-20f, greenTumbleweed.transform.position.y, greenTumbleweed.transform.position.z);

        ResetTumbleweed(redTumbleweed, redStartPos);
        ResetTumbleweed(yellowTumbleweed, yellowStartPos);
        ResetTumbleweed(greenTumbleweed, greenStartPos);
    }

    void Update()
    {
        if (gameStarted)
        {
            switch (currentState)
            {
                case TumbleweedState.RedRolling:
                    MoveAndRotateTumbleweed(redTumbleweed, redStartPos, TumbleweedState.YellowRolling);
                    break;
                case TumbleweedState.YellowRolling:
                    MoveAndRotateTumbleweed(yellowTumbleweed, yellowStartPos, TumbleweedState.GreenRolling);
                    break;
                case TumbleweedState.GreenRolling:
                    MoveAndRotateTumbleweed(greenTumbleweed, greenStartPos, TumbleweedState.Finished);
                    break;
                case TumbleweedState.Finished:
                    gameStarted = false; //count ends once all tumbleweeds have rolled
                    break;
            }
        }
    }

    public void StartGame()
    {
        //reset everything to start the counter
        gameStarted = true;
        currentState = TumbleweedState.RedRolling;

        ResetTumbleweed(redTumbleweed, redStartPos);
        ResetTumbleweed(yellowTumbleweed, yellowStartPos);
        ResetTumbleweed(greenTumbleweed, greenStartPos);
    }

    private void MoveAndRotateTumbleweed(GameObject tumbleweed, Vector3 resetPosition, TumbleweedState nextState)
    {
        //activate non active tumbleweed
        if (!tumbleweed.activeSelf)
            tumbleweed.SetActive(true);

        //move it to the right
        tumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

        //roll the tumbleweed
        if (tumbleweed.transform.childCount > 0)
        {
            tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 2f * Time.deltaTime));
        }

        //check if the tumbleweed is 'off screen'
        if (tumbleweed.transform.position.x > 200f) //adjust for "off screen" boundary
        {
            // Reset position and transition to the next state
            tumbleweed.transform.position = resetPosition;
            tumbleweed.SetActive(false);
            currentState = nextState;
        }
    }

    private void ResetTumbleweed(GameObject tumbleweed, Vector3 startPos)
    {
        tumbleweed.transform.position = startPos;
        tumbleweed.SetActive(false); // Hide initially
    }
}
