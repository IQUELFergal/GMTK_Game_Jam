using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameInteractable : MonoBehaviour,IInteractable
{
    public UnityEvent endGameEvent;

    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}
