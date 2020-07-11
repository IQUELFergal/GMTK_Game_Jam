using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class DeadZone : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ( !col.isTrigger && col.GetComponent<PlayerController>() != null)
        {
            gm.ResetPlayer();
        }
    }
}
