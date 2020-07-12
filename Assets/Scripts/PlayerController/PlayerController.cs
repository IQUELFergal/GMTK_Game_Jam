using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ControlRandomizer controlRandomizer;
    public float moveSpeed = 1;
    //public float speed = 10;
    //public float moveDuration= 1;

    [Header("Jump settings")]
    public float jumpDelay = 0;
    public float jumpForce = 250;

    [Header("Crouch settings")]
    public float crouchDelay = 1;

    [Header("Interaction settings")]
    public float interactionDelay = 1;
    public float lightFlashDuration = 0.25f;

    Animator animator;

    public SpriteRenderer lightSr;
    public Color lightActivationColor;
    public Color baseLightColor;
    bool isMoving = false;
    bool isInteracting = false;
    bool isCrouched = false;
    bool canCrouch = true;
    bool canJump = true;

    Rigidbody2D rb;
    ColliderInteractor interactor;

    public bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    [SerializeField] public LayerMask groundLayerMask;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponent<ColliderInteractor>();
        animator = GetComponent<Animator>();
        lightSr.color = baseLightColor;
        for (int i = 0; i < controlRandomizer.controllers.Length; i++)
        {
            controlRandomizer.controllers[i].stringEvent.AddListener(DoSomething);
        }
    }


    void Update()
    {
        // moving 
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, groundLayerMask);

        animator.SetBool("isCrouched", isCrouched);
        animator.SetBool("isMoving", isMoving);
    }

    void DoSomething(string action)
    {
        // Debug.Log(action);
        switch (action)
        {
            case "none":
                break;

            case "none" + Controller.continuousAction:
                break;

            // Move Left
            case "moveLeft":
                Move(-moveSpeed);
                break;

            case "moveLeft" + Controller.continuousAction:
                MoveContinuous(-moveSpeed);
                break;

            // Move Right
            case "moveRight":
                Move(moveSpeed);
                break;

            case "moveRight" + Controller.continuousAction:
                MoveContinuous(moveSpeed);
                break;

            // Jump
            case "jump":
                Jump();
                break;

            case "jump" + Controller.continuousAction:
                JumpContinuous();
                break;

            // crouch
            case "crouch":
                Crouch();
                break;
            case "crouch" + Controller.continuousAction:
                CrouchContinuous();
                break;

            // interact
            case "interact":
                InteractContinuous();
                break;

            case "interact" + Controller.continuousAction:
                InteractContinuous();
                break;

            // die
            case "selfDestroy":
                FindObjectOfType<PlayerRespawner>().ResetPlayer();
                break;
            case "selfDestroy" + Controller.continuousAction:
                FindObjectOfType<PlayerRespawner>().ContinuousResetPlayerPosition();
                break;

            // default case
            default:
                Debug.LogError("This action does not exist !");
                break;
        }
    }



    // Move =======================================================================================
    private void Move(float speed)
    {
        transform.Translate(Vector3.right * speed / 2);
    }

    private void MoveContinuous(float speed)
    {
        if (speed != 0)
        {
            Debug.Log("Moving " + (speed > 0 ? "right" : "left") + " continuously");
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }



    // Jumping ===========================================================================
    private void Jump()
    {
        if (canJump)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }            
    }

    private void JumpContinuous()
    {
        if (canJump)
        {
            Jump();
            StartCoroutine(CancelJump(jumpDelay));
        }
    }

    IEnumerator CancelJump(float duration)
    {
        canJump = false;
        yield return new WaitForSeconds(duration);
        canJump = true;
    }    




    // Crouch ================================================================
    private void Crouch()
    {
        if (canCrouch)
        {
            if (!isCrouched)
            {
                Debug.Log("Crouching");
                GetComponent<BoxCollider2D>().size = new Vector2(0.65f, 1);
                isCrouched = true;
            }
            else
            {
                Debug.Log("Uncrouching");
                GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                isCrouched = false;
            }
        }
    }

    private void CrouchContinuous()
    {
        if (canCrouch)
        {
            Crouch();
            StartCoroutine(CancelCrouch(crouchDelay));
        }
    }

    IEnumerator CancelCrouch(float duration)
    {
        canCrouch = false;
        yield return new WaitForSeconds(duration);
        canCrouch = true;
    }

    

    // Interact =============================================================
    private void InteractContinuous()
    {
        if (!isInteracting)
        {
            Debug.Log("Interacting");
            interactor.Interact();
            StartCoroutine(FlashLight(0.25f));
            StartCoroutine(CancelInteraction(interactionDelay));
        }
    }

    IEnumerator CancelInteraction(float duration)
    {
        isInteracting = true;
        yield return new WaitForSeconds(duration);
        isInteracting = false;
    }

    IEnumerator FlashLight(float duration)
    {
        lightSr.color = lightActivationColor;
        yield return new WaitForSeconds(duration);
        lightSr.color = baseLightColor;
    }
}
