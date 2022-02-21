using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public Transform playerBody;

    private float mouseX = 0;
    
    protected ElfInput playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new ElfInput();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.isGamePaused)
        {
            mouseX = playerInput.PlayerActionMap.Movement.ReadValue<Vector2>().x;
            playerBody.Rotate(Vector3.up * rotationSpeed * mouseX);
        }
    }
    
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
