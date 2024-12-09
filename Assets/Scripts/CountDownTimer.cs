using UnityEngine;
using System.Collections;
public class CountDownTimer : MonoBehaviour
{
    public GameObject redTumbleweed;
    public GameObject yellowTumbleweed;
    public GameObject greenTumbleweed;

    public float tumbleweedSpeed = 10f;
    private bool gameStarted = false;
    public static bool gameEnabled = false; // shared state for game logic

    private Vector3 redStartPos;
    private Vector3 yellowStartPos; 
    private Vector3 greenStartPos;

    private enum TumbleweedState { Idle, RedRolling, YellowRolling, GreenRolling, Finished }
    private TumbleweedState currentState = TumbleweedState.Idle;

    public bool yellowFinished = false;

    void Start()
    {   
        //initial positions for the tumbleweeds
        redStartPos = redTumbleweed.transform.position;
        yellowStartPos = yellowTumbleweed.transform.position;
        greenStartPos = greenTumbleweed.transform.position;

        ResetTumbleweed(redTumbleweed, redStartPos);
        ResetTumbleweed(yellowTumbleweed, yellowStartPos);
        ResetTumbleweed(greenTumbleweed, greenStartPos);

        StartGame();
    }

    void Update()
    {
        if (gameStarted)
        {
            switch (currentState)
            {
                case TumbleweedState.RedRolling:
                    MoveRedTumbleweed(redTumbleweed, redStartPos, TumbleweedState.YellowRolling);
                    break;
                case TumbleweedState.YellowRolling:
                    MoveYellowTumbleweed(yellowTumbleweed, yellowStartPos, TumbleweedState.GreenRolling);
                    break;
                case TumbleweedState.GreenRolling:
                    // check green tumbleweed position before finishing
                    if (yellowFinished) MoveGreenTumbleweed();        
                    break;
                case TumbleweedState.Finished:
                    gameStarted = false; //end timer
                    break;
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        currentState = TumbleweedState.RedRolling;

        ResetTumbleweed(redTumbleweed, redStartPos);
        ResetTumbleweed(yellowTumbleweed, yellowStartPos);
        ResetTumbleweed(greenTumbleweed, greenStartPos);
        gameEnabled = false; //reset gameEnabled
    }


    private void MoveYellowTumbleweed(GameObject tumbleweed, Vector3 resetPosition, TumbleweedState nextState)
    {
        //activate non active tumbleweed
        if (!tumbleweed.activeSelf) tumbleweed.SetActive(true);
        //move it to the right
        tumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

        //roll the tumbleweed
        if (tumbleweed.transform.childCount > 0)
            tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //check if the tumbleweed is 'off screen'
        if (tumbleweed.transform.position.x > 5f)//adjust for "off screen" boundary
        {
            Debug.Log("Inside 'if' statement for Yellow tumbleweed movement");
            tumbleweed.transform.position = resetPosition;//reset position and transition to the next state
            tumbleweed.SetActive(false);
            StartCoroutine(StartGreenTumbleweedWithDelay());
            currentState = nextState;
        }
    }
    private void MoveRedTumbleweed(GameObject tumbleweed, Vector3 resetPosition, TumbleweedState nextState)
    {
        //activate non active tumbleweed
        if (!tumbleweed.activeSelf) tumbleweed.SetActive(true);
        //move it to the right
        tumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

        //roll the tumbleweed
        if (tumbleweed.transform.childCount > 0)
            tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //check if the tumbleweed is 'off screen'
        if (tumbleweed.transform.position.x > 5f)//adjust for "off screen" boundary
        {
            tumbleweed.transform.position = resetPosition;//reset position and transition to the next state
            tumbleweed.SetActive(false);
            currentState = nextState;
        }
    }

    private IEnumerator StartGreenTumbleweedWithDelay()
    {
        float delay = Random.Range(0f, 5f); // Random delay between 0 and 5 seconds
        yield return new WaitForSeconds(delay);
        currentState = TumbleweedState.GreenRolling; // Transition to green tumbleweed state
        yellowFinished = true;
    }

    private void MoveGreenTumbleweed()
    {
        yellowTumbleweed.SetActive(false);
        if (!greenTumbleweed.activeSelf) greenTumbleweed.SetActive(true);
        greenTumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

        if (greenTumbleweed.transform.childCount > 0)
            greenTumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //enable movement when green tumbleweed reaches x = 1
        if (!gameEnabled && greenTumbleweed.transform.position.x >= 1f)
        {
            gameEnabled = true;
            Debug.Log("Game Enabled!");
        }

        //reset position and transition to the next state
        if (greenTumbleweed.transform.position.x > 5f)
        {
            greenTumbleweed.transform.position = greenStartPos;
            greenTumbleweed.SetActive(false);
            currentState = TumbleweedState.Finished;
        }
    }

    private void ResetTumbleweed(GameObject tumbleweed, Vector3 startPos)
    {
        tumbleweed.transform.position = startPos;
        tumbleweed.SetActive(false);
    }
}