using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class StringEvent : UnityEvent<string>
{
}

public class Controller : UIBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum Control { none, moveLeft, moveRight, jump, crouch, interact , selfDestroy};
    public Control control = Control.none;


    Text text;
    Image image;
    public float flashDuration = 0.5f;
    public Color baseColor = Color.white;
    public Color activatedColor = Color.yellow;


    [HideInInspector] public StringEvent stringEvent;
    bool isLocked = false;
    [HideInInspector] public const string continuousAction = "Continuous";


    protected override void Awake()
    {
        base.Awake();
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
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
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            stringEvent.Invoke(control.ToString());
            StartCoroutine(FlashColor());
            if (isLocked)
            {
                isLocked = false;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            isLocked = !isLocked;
            if (isLocked) image.color = activatedColor;
            else image.color = baseColor;
        }
    }

    public IEnumerator FlashColor()
    {
        image.color = activatedColor;
        yield return new WaitForSeconds(flashDuration);
        image.color = baseColor;
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
