using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    //movement variables
    [SerializeField]
    float walkSpeed = 5;
    [SerializeField]
    float runSpeed = 10;
    [SerializeField]
    float jumpForce = 5;
    [SerializeField]
    float airControl = 0.5f;
    

    //components
    PlayerController playerController;
    Rigidbody rigidbody;
    Animation playerAnimator;
    protected ElfInput playerInput;

    // movement references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;



    private void Awake()
    {
        playerAnimator = GetComponent<Animation>();
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        playerInput = new ElfInput();
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
        } else if(!playerController.isInAir && !playerController.isJumping)
        {
            playerAnimator.Blend("CharacterArmature|Run", 1.0f);
        }

        moveDirection = transform.forward * inputVector.y;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        if (playerController.isInAir){

             movementDirection = moveDirection* airControl * (currentSpeed * Time.deltaTime);
        }
        transform.position += movementDirection;

    }

    private void UpdateInput()
    {
        inputVector = playerInput.PlayerActionMap.Movement.ReadValue<Vector2>();
        playerController.isRunning = playerInput.PlayerActionMap.Run.IsPressed();
        playerController.isJumping = playerInput.PlayerActionMap.Jump.IsPressed();
        playerController.isPickingUp = playerInput.PlayerActionMap.Interact.IsPressed();
    }

    private void CheckJump()
    {
        if (playerController.isInAir) return ;
        if (!playerController.isJumping) return ;
        
        //jump
        rigidbody.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
        playerController.isInAir = true;
        playerController.isJumping = true;
        playerAnimator.Play("CharacterArmature|Jump");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !playerController.isJumping) return;

        playerController.isInAir = false;
        playerController.isJumping = false;
        playerAnimator.Blend("CharacterArmature|Run", 1.0f);
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