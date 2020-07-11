using UnityEngine;

[RequireComponent(typeof(Plot))]
public class PlotInteractable : MonoBehaviour, IInteractable
{
    public string interactionTextWithSeed = "Harvest";
    public string interactionTextWithoutSeed = "Plant";

    public string interactionText { get { return InteractionText; } }
    private string InteractionText = "";


    Plot plot;

    void Start()
    {
        plot = GetComponent<Plot>();
    }

    void Update()
    {
        if (plot.seed != null)
        {
            InteractionText = interactionTextWithSeed;
        }
        else InteractionText = interactionTextWithoutSeed;
    }

    void IInteractable.Interact()
    {
        if (plot.seed != null)
        {
            plot.Harvest();
        }
        //else plot.Plant();
        
    }
}
