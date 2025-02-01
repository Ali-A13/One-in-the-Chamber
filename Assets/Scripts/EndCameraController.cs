using UnityEngine;

public class EndCameraController : MonoBehaviour
{
    public Transform startPoint;   // Starting position for the camera
    public Transform endPoint;     // Final position for the camera
    public float moveDuration = 5f; // Time it takes to move the camera
    private float elapsedTime = 0f; // Tracks the elapsed time

    private bool isMoving = false;  // Tracks if the camera is moving

    void Start()
    {
        // Set the camera's initial position to the starting point
        if (startPoint != null)
        {
            transform.position = startPoint.position;
            transform.rotation = startPoint.rotation;
        }
    }

    void Update()
    {
        // If the camera is set to move, update its position over time
        if (isMoving && endPoint != null)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / moveDuration);            // Normalize time to 0-1

            // Smoothly interpolate position and rotation
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            transform.rotation = Quaternion.Lerp(startPoint.rotation, endPoint.rotation, t);

            // Stop moving once the duration is reached
            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }

    public void StartMovingCamera()
    {
        // Reset the elapsed time and start moving
        elapsedTime = 0f;
        isMoving = true;
    }
}
