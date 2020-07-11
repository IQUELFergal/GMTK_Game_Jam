using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameInteractable : MonoBehaviour,IInteractable
{
    [HideInInspector] public UnityEvent endGameEvent;

    public void Interact()
    {
        endGameEvent.Invoke();
    }
}
