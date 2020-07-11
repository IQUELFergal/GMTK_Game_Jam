using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class SeflDestroy : MonoBehaviour
{

    public GameObject start;

    public void Destroy()
    {
        gameObject.SetActive(false);
        transform.position = start.transform.position;
        gameObject.SetActive(true);
    }
}
