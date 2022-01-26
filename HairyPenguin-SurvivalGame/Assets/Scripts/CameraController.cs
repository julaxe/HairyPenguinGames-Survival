using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    private float mouseX = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        playerBody.Rotate(Vector3.up * mouseSensitivity * mouseX);
    }

    public void OnCameraMovement(InputValue val)
    {
        mouseX = val.Get<float>();
    }
}
