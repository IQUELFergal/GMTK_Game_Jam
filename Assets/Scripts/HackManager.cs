using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackManager : MonoBehaviour
{
    ControlRandomizer randomizer;
    Screen screen;

    public float timeUntilEachHack = 30;
    bool canHack = true;

    void Start()
    {
        randomizer = (ControlRandomizer)FindObjectOfType(typeof(ControlRandomizer));
        screen = (Screen)FindObjectOfType(typeof(Screen));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Hack();
        }
        if (canHack)
        {
            Hack();
            StartCoroutine(WaitUntilNextHack());
        }
    }

    void Hack()
    {
        randomizer.RandomizeControllers();
        screen.Hack(3);
    }

    IEnumerator WaitUntilNextHack()
    {
        canHack = false;
        yield return new WaitForSeconds(timeUntilEachHack);
        canHack = true;
    }

}
