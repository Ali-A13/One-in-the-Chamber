using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Adjust to control sensitivity
    public Transform playerBody; // Reference to the player's body for rotation

    private float xRotation = 0f; // Track vertical rotation to clamp it

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update vertical rotation and clamp it to avoid unnatural angles
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply vertical rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player body horizontally
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
