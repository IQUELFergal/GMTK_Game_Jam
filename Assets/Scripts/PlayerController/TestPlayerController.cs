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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < controlRandomizer.controllers.Length; i++)
        {
            controlRandomizer.controllers[i].stringEvent.AddListener(DoSomething);
        }
    }


    void DoSomething(string action)
    {
        //Debug.Log(action);
        switch (action)
        {
            case "none":
                ResetMoveSpeed();
                break;
            case "moveLeft":
                StartCoroutine(Move(-movementStep));
                break;

            case "moveRight":
                StartCoroutine(Move(-movementStep*Time.deltaTime));
                break;

            case "moveLeft"+Controller.continuousAction:
                MoveContinuous(-movementStep);
                break;

            case "moveRight" + Controller.continuousAction:
                //StartCoroutine(Move(movementStep * Time.deltaTime));
                MoveContinuous(movementStep);
                break;

            case "jump":
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

    private void MoveContinuous(float speed)
    {
        if (speed != 0)
        {
            Debug.Log("Moving " + (speed > 0 ? "right" : "left") + " continuously");
            //rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
