using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;

[System.Serializable]
public class StringEvent : UnityEvent<string>
{
}

public class Controller : UIBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum Control { none, moveLeft, moveRight, jump, crouch, interact , selfDestroy};
    public Control control = Control.none;
    public Text text;
    [HideInInspector] public StringEvent stringEvent;
    bool isLocked = false;
    [HideInInspector] public const string continuousAction = "Continuous";

    protected override void Awake()
    {
        base.Awake();
        if (stringEvent == null)
            stringEvent = new StringEvent();

        Setup(0);
    }

    private void Update()
    {
        if (isLocked)
        {
            stringEvent.Invoke(control.ToString() + continuousAction);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(eventData.button.ToString() + " click on " + this.ToString());
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            stringEvent.Invoke(control.ToString());
            if (isLocked)
            {
                isLocked = false;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            isLocked = !isLocked;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Exiting " + this.ToString());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Entering " + this.ToString());
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
