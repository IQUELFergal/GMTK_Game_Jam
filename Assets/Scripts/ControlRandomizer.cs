using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRandomizer : MonoBehaviour
{
    public Color green = Color.green;
    public Color red = Color.red;
    public Color baseColor = Color.white;
    bool isRed = true;

    public Image image;
    public Controller[] controllers;
    

    void Start()
    {
        image.color = baseColor;

        int enumLength = Enum.GetNames(typeof(Controller.Control)).Length;
        if (enumLength > controllers.Length)
        {
            Debug.Log("Not enough controllers for the current enum of controls");
            return;
        }
        
        RandomizeControllers();
    }


    //Debug
    public void ChangeColor()
    {
        if (isRed)
        {
            StartCoroutine(FlashColor(green));
        }
        else StartCoroutine(FlashColor(red));
        isRed = !isRed;
    }

    public IEnumerator FlashColor(Color color)
    {
        image.color = color;
        yield return new WaitForSeconds(.5f);
        image.color = baseColor;
    }

    //End of Debug




    void RandomizeControllers()
    {
        Debug.Log("Randomizing controls");

        int enumLength = Enum.GetNames(typeof(Controller.Control)).Length;
        
        //Creer list de controllers a partir de l'array
        List<Controller> controllerList = new List<Controller>();
        for (int i = 0; i < controllers.Length; i++)
        {
            controllerList.Add(controllers[i]);
        }

        for (int i = 1; i < enumLength; i++)
        {
            int n = UnityEngine.Random.Range(0, controllerList.Count);
            controllerList[n].Setup(i);
            controllerList.RemoveAt(n);
        }

        for (int i = 0; i < controllerList.Count; i++)
        {
            controllerList[i].Setup(0);
        }
    }

}