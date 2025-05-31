using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LaserToggleImage : MonoBehaviour
{
    public TextMeshProUGUI targetText;   
    public Color highPowerColor = Color.red;
    public Color lowPowerColor = Color.white;
    public int lowPowerValue = 50;       //  laser is OFF
    public int highPowerValue = 100;     //  laser is ON

    private bool isLaserOn = true;

    void Start()
    {
        UpdateLaserText();
    }

    void OnMouseDown()
    {
        ToggleLaserPower();
    }

    void ToggleLaserPower()
    {
        isLaserOn = !isLaserOn;
        UpdateLaserText();
    }

    void UpdateLaserText()
    {
        if (targetText != null)
        {
            int currentPower = isLaserOn ? lowPowerValue : highPowerValue;
            targetText.text = isLaserOn ? lowPowerValue.ToString() : highPowerValue.ToString();
            targetText.color = isLaserOn ? lowPowerColor : highPowerColor;
            targetText.text = currentPower + "%";
        }

        Debug.Log("Laser power changed to: " + (isLaserOn ? lowPowerValue : highPowerValue) + "%");
    }

    // Optional getter if other scripts need to know the state
    public bool IsLaserHighPower()
    {
        return isLaserOn;
    }
}



