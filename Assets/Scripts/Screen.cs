using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    [ColorUsageAttribute(true, true)] public Color advicesColor = Color.cyan;
    [ColorUsageAttribute(true, true)] public Color securityCamColor = Color.white;
    [ColorUsageAttribute(true, true)] public Color hackColor = Color.red;
    Color[] colors =  new Color[Enum.GetNames(typeof(ScreenState)).Length];
    public Color currentColor;
    

    public Material material;

    public RawImage rawImage;
    public Image adviceImage;
    public Image hackImage;

    public RenderTexture securityCamTexture;
    public Sprite hackSprite;

    public enum ScreenState { advices, secutityCam, hack };
    ScreenState screenState = ScreenState.advices;

    // Start is called before the first frame update
    void Start()
    {
        colors[0] = advicesColor;
        colors[1] = securityCamColor;
        colors[2] = hackColor;

        UpdateColor();
        UpdateSupport();
    }

    public void SetScreenState(ScreenState state)
    {
        screenState = state;
        UpdateColor();
        UpdateSupport();
    }

    public void SetScreenState(int i)
    {
        if (i >= 0 && i < Enum.GetNames(typeof(ScreenState)).Length)
        {
            screenState = (ScreenState)i;
            UpdateSupport();
            UpdateColor();
        }
    }

    // Update is called once per frame
    void UpdateColor()
    {
        currentColor = colors[(int)screenState];
        material.SetColor("_Color", currentColor);
    }

    void UpdateSupport()
    {
        if (screenState == ScreenState.secutityCam)
        {
            hackImage.gameObject.SetActive(false);
            adviceImage.gameObject.SetActive(false);
            rawImage.gameObject.SetActive(true);
        }
        else if (screenState == ScreenState.hack)
        {
            hackImage.gameObject.SetActive(true);
            adviceImage.gameObject.SetActive(false);
            rawImage.gameObject.SetActive(false);
        }
        else
        {
            hackImage.gameObject.SetActive(false);
            adviceImage.gameObject.SetActive(true);
            rawImage.gameObject.SetActive(false);
        }
    }
    public void Hack(float duration)
    {
        StartCoroutine(HackScreen(duration));
    }
    IEnumerator HackScreen(float duration)
    {

        ScreenState state = screenState;
        SetScreenState(ScreenState.hack);
        yield return new WaitForSeconds(duration);
        SetScreenState(state);
    }
}
