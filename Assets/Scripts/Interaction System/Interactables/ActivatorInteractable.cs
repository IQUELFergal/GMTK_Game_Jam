using UnityEngine;

public class ActivatorInteractable : MonoBehaviour, IInteractable
{
    public string interactionText { get { return InteractionText; } }
    [SerializeField] private string InteractionText = "Activate";

    public IInteractable[] interactables;

    void IInteractable.Interact()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].Interact();
        }
    }
}
