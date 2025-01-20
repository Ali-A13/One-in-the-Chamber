using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    float xRotation;
    float yRotation;

    private WinController winController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set initial camera rotation
        xRotation = 65f;
        yRotation = 2.445f;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Set initial camera scale
        transform.localScale = new Vector3(1f, 1f, 2.4f);

        // Set initial orientation rotation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        winController = FindObjectOfType<WinController>();
        if (winController == null)
        {
            Debug.LogError("WinController not found in the scene. Ensure it is present in the scene.");
        }
    }
    void Update()
    {
        if (!CountDownTimer.gameEnabled) return; // Disable input until the game starts
        if (winController.gameEnd) return; // Disable input when the game ends

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -65f, 65f);
        yRotation = Mathf.Clamp(yRotation, -50f, 50f);

        Vector3 zScale = new Vector3(1, 1, xRotation / 45 + 1);
        if (zScale.z < 1)
        {
            zScale.z = 1;
        }
        transform.localScale = zScale;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
