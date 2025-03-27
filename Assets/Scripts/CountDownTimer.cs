using UnityEngine;
using System.Collections;
public class CountDownTimer : MonoBehaviour
{
    public GameObject redTumbleweed;
    public GameObject yellowTumbleweed;
    public GameObject greenTumbleweed;
    public GameObject stopPoint;

    public float tumbleweedSpeed = 10f;
    private bool gameStarted = false;
    public static bool gameEnabled = false; // shared state for game logic

    private Vector3 redStartPos;
    private Vector3 yellowStartPos; 
    private Vector3 greenStartPos;

    private enum TumbleweedState { Idle, RedRolling, YellowRolling, GreenRolling, Finished }
    private TumbleweedState currentState = TumbleweedState.Idle;

    public bool yellowFinished = false;

    public enum MovementDirection { Right, Left, Forward, Backward }
    public MovementDirection tumbleweedDirection = MovementDirection.Right;

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
        tumbleweed.transform.position += GetDirectionVector() * tumbleweedSpeed * Time.deltaTime;

        //roll the tumbleweed
        if (tumbleweed.transform.childCount > 0)
            tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //check if the tumbleweed is 'close enough' to the stop point (using distance)
        if (Vector3.Distance(tumbleweed.transform.position, stopPoint.transform.position) < 0.5f) // adjust for "close enough" distance
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
        tumbleweed.transform.position += GetDirectionVector() * tumbleweedSpeed * Time.deltaTime;

        //roll the tumbleweed
        if (tumbleweed.transform.childCount > 0)
            tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //check if the tumbleweed is 'close enough' to the stop point (using distance)
        if (Vector3.Distance(tumbleweed.transform.position, stopPoint.transform.position) < 0.5f) // adjust for "close enough" distance
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
        greenTumbleweed.transform.position += GetDirectionVector() * tumbleweedSpeed * Time.deltaTime;

        if (greenTumbleweed.transform.childCount > 0)
            greenTumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //enable movement when green tumbleweed reaches x = 1
        if (!gameEnabled && IsTumbleweedInView(greenTumbleweed))
        {
            gameEnabled = true;
            Debug.Log("Game Enabled!");
        }

        //check if the tumbleweed is 'close enough' to the stop point (using distance)
        if (Vector3.Distance(greenTumbleweed.transform.position, stopPoint.transform.position) < 0.5f) // adjust for "close enough" distance
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

    // Helper function to check if the tumbleweed is within the player's view
    private bool IsTumbleweedInView(GameObject tumbleweed)
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(tumbleweed.transform.position);
        // Check if the viewport position is within the camera's view
        // The values of viewportPosition.x and viewportPosition.y should be between 0 and 1 for the object to be visible in the camera's frustum.
        return (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1);
    }

    private Vector3 GetDirectionVector()
    {
        switch(tumbleweedDirection)
        {
            case MovementDirection.Right:
                return Vector3.right;
            case MovementDirection.Left:
                return Vector3.left;
            case MovementDirection.Forward:
                return Vector3.forward;
            case MovementDirection.Backward:
                return Vector3.back;
            default:
                return Vector3.right;
        }

    }
}