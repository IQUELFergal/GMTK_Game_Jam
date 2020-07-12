
using UnityEngine;

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
