using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ControlRandomizer controlRandomizer;
    public float movementStep = 1;
    public float speed = 10;
    public float actionTime = 1;

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
        
    float crouchScale = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponent<ColliderInteractor>();
        for (int i = 0; i < controlRandomizer.controllers.Length; i++)
        {
            controlRandomizer.controllers[i].stringEvent.AddListener(DoSomething);
        }
    }


    void Update()
    {
        // moving 
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, groundLayerMask);
    }

    void DoSomething(string action)
    {
        // Debug.Log(action);
        switch (action)
        {
            case "none":
                break;

            case "none" + Controller.continuousAction:
                ResetMoveSpeed();
                break;

            // Move Left
            case "moveLeft":
                StartCoroutine(Move(-movementStep));
                break;

            case "moveLeft" + Controller.continuousAction:
                MoveContinuous(-movementStep);
                break;

            // Move Right
            case "moveRight":
                StartCoroutine(Move(-movementStep * Time.deltaTime));
                break;

            case "moveRight" + Controller.continuousAction:
                MoveContinuous(movementStep);
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

    private void ResetMoveSpeed()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }


    // Move =======================================================================================
    IEnumerator Move(float speed)
    {
        if (speed != 0 && !isMoving)
        {
            isMoving = true;
            Debug.Log("Moving " + (speed > 0 ? "right" : "left"));
            rb.velocity = new Vector2(speed, rb.velocity.y);
            yield return new WaitForSeconds(actionTime);
            ResetMoveSpeed();
            isMoving = false;
        }
    }

    private void MoveContinuous(float speed)
    {
        if (speed != 0)
        {
            Debug.Log("Moving " + (speed > 0 ? "right" : "left") + " continuously");
            // rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }




    // Jumping ===========================================================================
    private bool IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            // myAnimator.SetBool("land", true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(feetPosition.position, checkRadius);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    /*myAnimator.ResetTrigger("jump");
                    myAnimator.SetBool("land", false);*/
                    return true;
                }
            }
        }
        return false;
    }

    private void Jump()
    {
        if (canJump)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * 250);
            }
        }            
    }

    private void JumpContinuous()
    {
        if (canJump)
        {
            Jump();
            StartCoroutine(CancelJump(3.0f));
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
                transform.localScale = new Vector2(1, crouchScale);
                isCrouched = true;
            }
            else
            {
                Debug.Log("Uncrouching");
                transform.localScale = new Vector2(1, 1);
                isCrouched = false;
            }
        }
    }

    private void CrouchContinuous()
    {
        if (canCrouch)
        {
            Crouch();
            StartCoroutine(CancelCrouch(1.0f));
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
            StartCoroutine(CancelInteraction(1));
        }
    }

    IEnumerator CancelInteraction(float duration)
    {
        isInteracting = true;
        yield return new WaitForSeconds(duration);
        isInteracting = false;
    }
}
