using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    private float cameraVerticalRotation = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Collect mouse input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Debugging Mouse Input
        Debug.Log($"Mouse X: {inputX}, Mouse Y: {inputY}");

        // Rotate the camera vertically (local X-axis)
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        // Rotate the player horizontally (Y-axis)
        if (player != null)
        {
            player.Rotate(Vector3.up * inputX);
        }
    }
}
