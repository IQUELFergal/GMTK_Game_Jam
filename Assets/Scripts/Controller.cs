using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Controller : UIBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum Control { none, moveLeft, moveRight, jump, crouch, interact };
    public Control control;
    public Text text;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on " + this.ToString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exiting " + this.ToString());
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entering " + this.ToString());
    }

    public void Setup(int i)
    {
        if (i >= 0 && i < Enum.GetNames(typeof(Control)).Length)
        {
            control = (Control)i;
            text.text = i.ToString() + " " + control.ToString();
        }
        else Debug.LogError("Can't setup the controller with this value : out of bounds");
    }
}
