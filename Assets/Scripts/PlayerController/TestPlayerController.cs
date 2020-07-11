using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    public ControlRandomizer controlRandomizer;
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
        Debug.Log(action);
        switch (action)
        {
            case "moveLeft":
                MoveLeft();
                break;

            case "moveRight":
                MoveRight();
                break;

            case "moveLeft"+Controller.continuousAction:
                MoveLeftContinuous();
                break;

            case "moveRight" + Controller.continuousAction:
                MoveRightContinuous();
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



    void MoveLeft()
    {
        rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
    }

    private void MoveLeftContinuous()
    {
        rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
    }



    void MoveRight()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void MoveRightContinuous()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
