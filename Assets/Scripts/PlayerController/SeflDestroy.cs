using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class SeflDestroy : MonoBehaviour
{

    public GameObject start;
    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = start.transform.position;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
}
