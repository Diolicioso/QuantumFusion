
using UnityEngine;
using UnityEngine.UI;

public class CoolingToggleImage : MonoBehaviour
{
    public Image uiImageName; 
    public Sprite defaultCoolingSprite;
    public Sprite highCoolingSprite;

    private bool defaultCoolingOn = true;

    void OnMouseDown()
    {
        ToggleCoolingImage();
    }

    void ToggleCoolingImage()
    {
        defaultCoolingOn = !defaultCoolingOn;
        
        if(uiImageName != null)
        {
            uiImageName.sprite = defaultCoolingOn ? defaultCoolingSprite : highCoolingSprite;
        }

        Debug.Log("Cooling image toggled to: " + (defaultCoolingOn ? "Default" : "High"));
    }
}


