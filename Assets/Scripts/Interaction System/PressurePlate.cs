using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public IInteractable[] interactables;
    public Collider2D trigger;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<Collider2D>();
        if (trigger == null)
        {
            Debug.LogError("No collider2D found : creating a new one...");
            trigger = gameObject.AddComponent<BoxCollider2D>();
        }
        trigger.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].Interact();
        }
    }
}
