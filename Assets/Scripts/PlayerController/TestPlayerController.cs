using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public ControlRandomizer controlRandomizer;
    public float movementStep = 1;
    public float speed = 6;
    public float actionTime = 1;
    Rigidbody2D rb;

    public bool isGrounded;
    public Transform feetPosition;
    public float checkRadius;
    [SerializeField] public LayerMask groundLayerMask;
    private bool isJumping;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        /*if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            // myAnimator.ResetTrigger("jump");
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else isJumping = false;
        }
        else isJumping = false;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (isGrounded && isJumping)
        {
            isGrounded = false;
            // myAnimator.SetTrigger("jump");
        }*/
    }

    void DoSomething(string action)
    {
        //Debug.Log(action);
        switch (action)
        {
            case "none":
                ResetMoveSpeed();
                break;

            case "none" + Controller.continuousAction:
                ResetMoveSpeed();
                break;


            //Move Left
            case "moveLeft":
                StartCoroutine(Move(-movementStep));
                break;

            case "moveLeft" + Controller.continuousAction:
                MoveContinuous(-movementStep);
                break;



            //Move Right
            case "moveRight":
                StartCoroutine(Move(-movementStep*Time.deltaTime));
                break;

            case "moveRight" + Controller.continuousAction:
                MoveContinuous(movementStep);
                break;




            case "jump":
                StartCoroutine(Jump(movementStep));
                break;

            case "crouch":
                break;

            case "interact":
                break;

            case "selfDestroy":
                break;

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
        if(speed != 0)
        {
            Debug.Log("Moving " + (speed > 0 ? "right" : "left"));
            rb.velocity = new Vector2(speed, rb.velocity.y);
            yield return new WaitForSeconds(actionTime);
            ResetMoveSpeed();
        }
    }

    IEnumerator Jump(float speed)
    {
        if (speed != 0)
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * speed);
            yield return new WaitForSeconds(actionTime);
        }
    }

    private void MoveContinuous(float speed)
    {
        if (speed != 0)
        {
            Debug.Log("Moving " + (speed > 0 ? "right" : "left") + " continuously");
            //rb.velocity = new Vector2(speed, rb.velocity.y);
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
}
