using UnityEngine;
using UnityEngine.UI;

public class ControlRandomizer : MonoBehaviour
{
    public Color green = Color.green;
    public Color red = Color.red;
    bool isRed = true;

    public Image image;
    public Button button;


    void Start()
    {
        image.color = red;
    }
    public void ChangeColor()
    {
        if (isRed)
        {
            image.color = green;
            
        }
        else image.color = red;
        isRed = !isRed;
    }
}