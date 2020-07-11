using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public ControlRandomizer controlRandomizer;
    public float movementStep = 1;
    public float speed = 10;
    public float actionTime = 1;
    bool isMoving = false;
    bool isInteracting = false;
    Rigidbody2D rb;
    ColliderInteractor interactor;

    public bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    [SerializeField] public LayerMask groundLayerMask;

    bool isCrouched = false;

    float myScale = 0.5f;
    


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

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
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
                ResetMoveSpeed();
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
                StartCoroutine(Move(-movementStep*Time.deltaTime));
                break;

            case "moveRight" + Controller.continuousAction:
                MoveContinuous(movementStep);
                break;

            // Jump
            case "jump":
                if (isGrounded)
                {
                    rb.AddForce(Vector2.up * 250);
                }                    
                break;

            case "jump" + Controller.continuousAction:
                // rb.AddForce(Vector2.up * speed); 
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

    IEnumerator Move(float speed)
    {
        if(speed != 0 && !isMoving)
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

    // Jumping
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

    // Crouching 
    private void Crouch()
    {
        if (!isCrouched)
        {
            transform.localScale = new Vector2(1, myScale);
            isCrouched = !isCrouched;
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
            isCrouched = !isCrouched;
        }
    }


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
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        isInteracting = false;
    }

    private void CrouchContinuous()
    {
        if (!isInteracting)
        {
            StartCoroutine(CancelCrouch(5.0f));
        }
    }

    IEnumerator CancelCrouch(float duration)
    {
        Crouch();
        yield return new WaitForSeconds(duration);
        Crouch();
    }
}
