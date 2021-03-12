using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSense = 100f;

    public Transform playerBody;

    float xRotation = 0f;

        void Start()
    {
        //makes the cursor locked to center of screen
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //uses your mouse input times the sensitivity times time.delta (for no difference due to FPS)
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        //inverting mouse movement thus not having the standard inverted mouse
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //make a clamp so you won't "overlook" on your player

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotation

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
