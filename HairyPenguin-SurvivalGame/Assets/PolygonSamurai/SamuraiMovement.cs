using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class SamuraiMovement : MonoBehaviour
{ //movement variables
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
    Animator playerAnimator;

    PlayerInput playerInput;
    // movement references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    float playerRotation;

    PauseMenuScript pauseMenu;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();
        pauseMenu = GetComponent<PauseMenuScript>();
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
        playerAnimator.SetFloat("VelX", inputVector.y);
        if (playerInput.actions["Interact"].IsPressed() && !playerController.isJumping && !playerController.isInAir)
        {

            playerAnimator.SetBool("AttackPressed", true);

        }
       if( playerAnimator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("Basic Attack"))
        {
            playerAnimator.SetBool("AttackPressed", false);
        }
        moveDirection = transform.forward * inputVector.y;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        if (Mathf.Abs(rigidbody.velocity.z) > speedLimit || Mathf.Abs(rigidbody.velocity.x) > speedLimit)
        {

            rigidbody.velocity = new Vector3(rigidbody.velocity.x * airControl, rigidbody.velocity.y, rigidbody.velocity.z * airControl);
        }


        transform.position += movementDirection;
        if (!PauseMenuScript.isGamePaused)
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
        playerAnimator.SetBool("JumpPressed", true);
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
        playerAnimator.SetBool("JumpPressed", false);

    }

}
