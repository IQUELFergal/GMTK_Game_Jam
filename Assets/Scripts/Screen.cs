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
    public Image image;

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
    }

    public void SetScreenState(int i)
    {
        if (i >= 0 && i < Enum.GetNames(typeof(ScreenState)).Length)
        {
            screenState = (ScreenState)i;
            UpdateColor();
            UpdateSupport();
        }
    }

    // Update is called once per frame
    void UpdateColor()
    {
        currentColor = colors[(int)screenState];
    }

    void UpdateSupport()
    {
        if (screenState == ScreenState.secutityCam)
        {
            rawImage.gameObject.SetActive(true);
            image.gameObject.SetActive(false);
        }
        else
        {
            image.gameObject.SetActive(true);
            rawImage.gameObject.SetActive(false);
        }
    }
}
