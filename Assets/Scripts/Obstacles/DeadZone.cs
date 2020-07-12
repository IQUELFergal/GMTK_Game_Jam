using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class DeadZone : MonoBehaviour
{
    PlayerRespawner respawner;

    void Start()
    {
        respawner = (PlayerRespawner)FindObjectOfType(typeof(PlayerRespawner));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ( !col.isTrigger && col.GetComponent<PlayerController>() != null)
        {
            respawner.ResetPlayer();
        }
    }
}
