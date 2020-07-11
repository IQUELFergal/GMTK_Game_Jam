using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackManager : MonoBehaviour
{
    ControlRandomizer randomizer;

    public float timeUntilEachHack = 30;
    bool canHack = true;

    void Start()
    {
        randomizer = (ControlRandomizer)FindObjectOfType(typeof(ControlRandomizer));
    }

    void Update()
    {
        if (canHack)
        {
            Hack();
            StartCoroutine(WaitUntilNextHack());
        }
    }

    void Hack()
    {
        randomizer.RandomizeControllers();
    }

    IEnumerator WaitUntilNextHack()
    {
        canHack = false;
        yield return new WaitForSeconds(timeUntilEachHack);
        canHack = true;
    }

}
