using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerInput : MonoBehaviour
{
    //movement variables
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;
    [SerializeField]
    float speedLimit = 2.5f;
    [SerializeField]
    float rotationSens = 4;
    [SerializeField]
    [Range(0, 1)]
    float airControl = 0.5f;

    //components
    PlayerController playerController;
    Rigidbody rigidbody;
    Animation playerAnimator;
   
    PlayerInput playerInput;
    // movement references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    float playerRotation;



    private void Awake()
    {
        playerAnimator = GetComponent<Animation>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
       
        playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator.Play("CharacterArmature|Idle");
    }

    // Update is called once per frame
    void Update()
    {

        UpdateInput();
        CheckJump();
        if (!(inputVector.magnitude > 0) && !playerController.isInAir)
        {
            playerAnimator.Play("CharacterArmature|Idle");
            moveDirection = Vector3.zero;
        }
        else if (!playerController.isInAir && !playerController.isJumping)
        {
            playerAnimator.Blend("CharacterArmature|Run", 1.0f);
        }

        moveDirection = transform.forward * inputVector.y;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        if (Mathf.Abs(rigidbody.velocity.z) > speedLimit || Mathf.Abs(rigidbody.velocity.x) > speedLimit)
        {

            rigidbody.velocity = new Vector3(rigidbody.velocity.x * airControl, rigidbody.velocity.y, rigidbody.velocity.z * airControl);
        }
        

        transform.position += movementDirection;
        Rotate();
    }

    private void UpdateInput()
    {
        inputVector = playerInput.actions["Movement"].ReadValue<Vector2>();
        playerController.isRunning = playerInput.actions["Run"].IsPressed();
        playerController.isJumping = playerInput.actions["Jump"].IsPressed();
        playerController.isPickingUp = playerInput.actions["Interact"].IsPressed();
    }

    private void CheckJump()
    {
        if (playerController.isInAir) return;
        if (!playerController.isJumping) return;

        //jump
        rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        playerController.isInAir = true;
        playerController.isJumping = true;
        playerAnimator.Play("CharacterArmature|Jump");
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSens * inputVector.x); 
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isInAir = false;
        playerController.isJumping = false;
        playerAnimator.Blend("CharacterArmature|Run", 1.0f);
    }

}