using UnityEngine;
using System.Collections.Generic;

public class LaserToggle : MonoBehaviour
{
    public static List<LaserToggle> allLasers = new List<LaserToggle>();

    public bool isHighPower = false;
    private Renderer rend;

    public Color lowPowerColor = Color.green;
    public Color highPowerColor = Color.red;

    public bool IsHighPower(){
        return isHighPower;
    }

    void Awake()
    {
        allLasers.Add(this);
    }

    void OnDestroy()
    {
        allLasers.Remove(this);
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateColor();
    }

    void OnMouseDown()
    {
        isHighPower = !isHighPower;
        UpdateColor();
    }

    void UpdateColor()
    {
        if (rend != null)
            rend.material.color = isHighPower ? highPowerColor : lowPowerColor;
    }
}


