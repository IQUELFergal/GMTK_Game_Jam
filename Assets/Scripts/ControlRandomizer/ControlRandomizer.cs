using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRandomizer : MonoBehaviour
{
    public Controller[] controllers;
    

    void Start()
    {

        int enumLength = Enum.GetNames(typeof(Controller.Control)).Length;
        if (enumLength > controllers.Length)
        {
            Debug.Log("Not enough controllers for the current enum of controls");
            return;
        }

        //RandomizeControllers();
        //SetupControllers();
    }

    public void RandomizeControllers()
    {
        Debug.Log("Randomizing controls");

        int enumLength = Enum.GetNames(typeof(Controller.Control)).Length;
        
        //Create a temporary list containing every controller inside the controller array
        List<Controller> controllerList = new List<Controller>();
        for (int i = 0; i < controllers.Length; i++)
        {
            controllerList.Add(controllers[i]);
        }

        //Sets up a random controller for each control inside the enum
        for (int i = 1; i < enumLength; i++)
        {
            int n = UnityEngine.Random.Range(0, controllerList.Count);
            controllerList[n].Setup(i);
            controllerList.RemoveAt(n);
        }

        //Sets up the rest of the controllers without control so the dont do anything
        for (int i = 0; i < controllerList.Count; i++)
        {
            controllerList[i].Setup(0);
        }
    }

    public void SetupControllers()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].Setup(0);
        }
    }
    
    [ContextMenu("Setup sprite")]
    void SetupSprites()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SetupSprite();
        }   
    }
}