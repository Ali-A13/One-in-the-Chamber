using UnityEngine;

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
                    MoveAndRotateTumbleweed(redTumbleweed, redStartPos, TumbleweedState.YellowRolling);
                    break;
                case TumbleweedState.YellowRolling:
                    MoveAndRotateTumbleweed(yellowTumbleweed, yellowStartPos, TumbleweedState.GreenRolling);
                    break;
                case TumbleweedState.GreenRolling:
                    // check green tumbleweed position before finishing
                    MoveGreenTumbleweed();
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

    private void MoveAndRotateTumbleweed(GameObject tumbleweed, Vector3 resetPosition, TumbleweedState nextState)
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

    private void MoveGreenTumbleweed()
    {
        if (!greenTumbleweed.activeSelf) greenTumbleweed.SetActive(true);
        greenTumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

        if (greenTumbleweed.transform.childCount > 0)
            greenTumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));

        //enable movement when green tumbleweed reaches x = 2.5
        if (!gameEnabled && greenTumbleweed.transform.position.x >= 2.5f)
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



// using UnityEngine;
// using TMPro;
// using System.Collections;

// public class CountDownTimer : MonoBehaviour
// {
//     public GameObject redTumbleweed;
//     public GameObject yellowTumbleweed;
//     public GameObject greenTumbleweed;

//     public float tumbleweedSpeed = 10f; //speed for all tumbleweeds
//     private bool gameStarted = false;

//     private Vector3 redStartPos;
//     private Vector3 yellowStartPos; 
//     private Vector3 greenStartPos;

//     private enum TumbleweedState { Idle, RedRolling, YellowRolling, GreenRolling, Finished }
//     private TumbleweedState currentState = TumbleweedState.Idle;

//     void Start()
//     {
//         //initial positions for the tumbleweeds
//         redStartPos = new Vector3(redTumbleweed.transform.position.x, redTumbleweed.transform.position.y, redTumbleweed.transform.position.z);
//         yellowStartPos = new Vector3(yellowTumbleweed.transform.position.x, yellowTumbleweed.transform.position.y, yellowTumbleweed.transform.position.z);
//         greenStartPos = new Vector3(greenTumbleweed.transform.position.x, greenTumbleweed.transform.position.y, greenTumbleweed.transform.position.z);

//         ResetTumbleweed(redTumbleweed, redStartPos);
//         ResetTumbleweed(yellowTumbleweed, yellowStartPos);
//         ResetTumbleweed(greenTumbleweed, greenStartPos);

//         StartGame();
//     }

//     void Update()
//     {
//         if (gameStarted)
//         {
//             switch (currentState)
//             {
//                 case TumbleweedState.RedRolling:
//                     MoveAndRotateTumbleweed(redTumbleweed, redStartPos, TumbleweedState.YellowRolling);
//                     break;
//                 case TumbleweedState.YellowRolling:
//                     MoveAndRotateTumbleweed(yellowTumbleweed, yellowStartPos, TumbleweedState.GreenRolling);
//                     break;
//                 case TumbleweedState.GreenRolling:
//                     MoveAndRotateTumbleweed(greenTumbleweed, greenStartPos, TumbleweedState.Finished);
//                     break;
//                 case TumbleweedState.Finished:
//                     //Text on screen to state Fire
//                     gameStarted = false; //count ends once all tumbleweeds have rolled
//                     break;
//             }
//         }
//     }

//     public void StartGame()
//     {
//         //reset everything to start the counter
//         gameStarted = true;
//         currentState = TumbleweedState.RedRolling;

//         ResetTumbleweed(redTumbleweed, redStartPos);
//         ResetTumbleweed(yellowTumbleweed, yellowStartPos);
//         ResetTumbleweed(greenTumbleweed, greenStartPos);
//     }

//     private void MoveAndRotateTumbleweed(GameObject tumbleweed, Vector3 resetPosition, TumbleweedState nextState)
//     {
//         //activate non active tumbleweed
//         if (!tumbleweed.activeSelf)
//             tumbleweed.SetActive(true);

//         //move it to the right
//         tumbleweed.transform.position += Vector3.right * tumbleweedSpeed * Time.deltaTime;

//         //roll the tumbleweed
//         if (tumbleweed.transform.childCount > 0)
//         {
//             tumbleweed.transform.GetChild(0).Rotate(Vector3.back * (tumbleweedSpeed * 100f * Time.deltaTime));
//         }

//         //check if the tumbleweed is 'off screen'
//         if (tumbleweed.transform.position.x > 5f) //adjust for "off screen" boundary
//         {
//             // Reset position and transition to the next state
//             tumbleweed.transform.position = resetPosition;
//             tumbleweed.SetActive(false);
//             currentState = nextState;
//         }
//     }

//     private void ResetTumbleweed(GameObject tumbleweed, Vector3 startPos)
//     {
//         tumbleweed.transform.position = startPos;
//         tumbleweed.SetActive(false); // Hide initially
//     }
// }