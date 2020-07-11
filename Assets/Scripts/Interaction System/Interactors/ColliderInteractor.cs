using UnityEngine;

public class ColliderInteractor : MonoBehaviour
{
    public IInteractable currentInteractable = null;

    public void Interact()
    {
        if (currentInteractable == null) return;
        currentInteractable.Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        currentInteractable = interactable;
    }
       
    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable == null) return;
        if (interactable != currentInteractable) return;
        
        currentInteractable = null;
    }
}