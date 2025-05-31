using UnityEngine;

public class Cooling : MonoBehaviour
{
    public bool isHighCooling = false;  // Unique state per coolant

    private Renderer rend;
    public Color defaultCoolingColor = Color.cyan;
    public Color highCoolingColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateVisual();
    }

    void OnMouseDown()
    {
        ToggleCoolingMode();
    }

    void ToggleCoolingMode()
    {
        isHighCooling = !isHighCooling;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (rend != null)
            rend.material.color = isHighCooling ? highCoolingColor : defaultCoolingColor;
    }

    public float GetCoolingMultiplier()
    {
        return isHighCooling ? 0.31f : 0.25f;
    }

    public static float GetTotalCoolingEffect()
    {
        float total = 0f;
        Cooling[] allCoolants = FindObjectsOfType<Cooling>();
        foreach (Cooling c in allCoolants)
        {
            total += c.GetCoolingMultiplier();
        }
        return total;
    }
}


