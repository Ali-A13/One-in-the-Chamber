using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPositon;
    private void Start()
    {
        // Set the initial camera position
        //transform.position = new Vector3(0.067f, 0.009f, 0.0468f);
    }

    private void Update()
    {
        transform.position = cameraPositon.position;
    }
}
