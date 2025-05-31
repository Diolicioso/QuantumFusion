using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ReactorSystem : MonoBehaviour
{
    public CameraShake cameraShake;

    [Header("Core Stats")]
    public int temperature = 10000;
    public int pressure = 0;
    public int fluctuation = 0;
    public float integrity = 100f;

    [Header("Modifiers")]
    public readonly int laserCount = 3;   // Up to 3
    public int totalNetOutput = 0; 

    [Header("Quota System")]
    public int winQuotaTarget = 5000;
    private float quotaTimer = 0f;
    public float quotaInterval = 1f;

    [Header("Extraction System")]
    public readonly float extractionRate = 0.02f; 

    [Header("Game State")]
    public bool hasWon = false;

    [Header("UI")]
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI pressureText;
    public TextMeshProUGUI fluctuationText;
    public TextMeshProUGUI quotaText;
    public GameObject winText;
    public TextMeshProUGUI integrityText;
    public TextMeshProUGUI radiationText;
    public TextMeshProUGUI quotaDisplayText;
    public GameObject gameUIGroup;

    [Header("Failure UI Group")]
    public AudioSource failAudio;
    public GameObject failGroup0;
    public GameObject failGroup0_Secondary;
    public GameObject failGroup0_Third;
    public GameObject failGroup; 
    public GameObject failGroup2;
    public GameObject failGroup2_Secondary;
    public GameObject failGroup2_Third;
    public GameObject failGroup3;


    void Start()
{
    if (winText != null)
        winText.gameObject.SetActive(false);
}

    void Update()
{
    if (hasWon) return;

    quotaTimer += Time.deltaTime;
    if (quotaTimer >= quotaInterval)
    {
        quotaTimer = 0f;
        Tick();
        UpdateUI();           
        fluctuation = 0;      
    }

    CheckWinCondition();

    if (temperature >= 50000)
    {
        HandleFailure();
    }
}


   void Tick()
{
    int previousTemperature = temperature;

    LaserToggle[] allLasers = FindObjectsOfType<LaserToggle>();
    float laserMultiplier = 0.0f;
    foreach (LaserToggle laser in allLasers)
    {
        laserMultiplier += laser.isHighPower ? 0.30f : 0.252f;
    }
    int laserIncrease = Mathf.RoundToInt(temperature * laserMultiplier);

    float totalCoolingEffect = Cooling.GetTotalCoolingEffect(); // Uses individual coolant multipliers
    int coolantDecrease = Mathf.RoundToInt(temperature * totalCoolingEffect);

    int netChange = laserIncrease - coolantDecrease;
    temperature += netChange;

    fluctuation = temperature - previousTemperature;

    pressure += Mathf.RoundToInt(temperature * 0.01f);

    float integrityLoss = pressure * 0.001f;
    integrity -= integrityLoss;
    integrity = Mathf.Max(integrity, 0f);


    totalNetOutput += Mathf.RoundToInt(temperature * extractionRate);
}




   void CheckWinCondition()
{
    if (totalNetOutput >= winQuotaTarget)
    {
        hasWon = true;

        if (winText != null)
        {
            winText.gameObject.SetActive(true);
        }

        if (gameUIGroup != null)
            gameUIGroup.SetActive(false);

        Debug.Log("You Win! Quota goal reached.");
    }
}
    private IEnumerator ShowFailSequence()
{
     if (cameraShake != null)
        cameraShake.StartShake(999f, 0.15f);
    
    if (failAudio != null)
        failAudio.Play();

    yield return new WaitForSecondsRealtime(6f);
    
    if (failGroup0 != null)
{
    failGroup0.SetActive(true);

    if (failGroup0_Secondary != null)
        failGroup0_Secondary.SetActive(true);
        
    if (failGroup0_Third != null)
        failGroup0_Third.SetActive(true);

    Transform image = failGroup0.transform.Find("FailImage");
    if (image != null)
    {
        image.gameObject.SetActive(true);
        Debug.Log("FailImage shown.");
    }

    for (int i = 1; i <= 7; i++) 
{
    Transform text = failGroup0.transform.Find($"DiagnosticText{i}");
    Transform text2 = failGroup0_Secondary != null ? failGroup0_Secondary.transform.Find($"DiagnosticText{i}") : null;
    Transform text3 = failGroup0_Third != null ? failGroup0_Third.transform.Find($"DiagnosticText{i}") : null;

    if (text != null) text.gameObject.SetActive(true);
    if (text2 != null) text2.gameObject.SetActive(true);
    if (text3 != null) text3.gameObject.SetActive(true);

    yield return new WaitForSecondsRealtime(4f); 
}

    yield return new WaitForSecondsRealtime(5f); 

}

    if (failGroup != null)
    {
        if (failGroup0 != null)
            failGroup0.SetActive(false);
        
        if (failGroup0_Secondary != null)
            failGroup0_Secondary.SetActive(false);
        
        if (failGroup0_Third != null)
            failGroup0_Third.SetActive(false);

        failGroup.SetActive(true);

        Transform text1 = failGroup.transform.Find("FailText1");
        Transform text2 = failGroup.transform.Find("FailText2");
        Transform image = failGroup.transform.Find("FailImage");

        if (text1 != null) text1.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        if (text2 != null) text2.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        if (image != null) image.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(7f);
    }

    // Show failGroup2
    if (failGroup2 != null)
    {
        if (failGroup != null)
            failGroup.SetActive(false);

        failGroup2.SetActive(true);

        if (failGroup2_Secondary != null)
            failGroup2_Secondary.SetActive(true);
        
        if (failGroup2_Third != null)
            failGroup2_Third.SetActive(true);

        Transform text1 = failGroup2.transform.Find("Text");
        Transform text2 = failGroup2.transform.Find("Text1");
        Transform text3 = failGroup2.transform.Find("Text2");
        Transform text4 = failGroup2.transform.Find("Text3");
        Transform image = failGroup2.transform.Find("Logo");

        if (text1 != null) text1.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        if (text2 != null) text2.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        if (text3 != null) text3.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        if (text4 != null) text4.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        if (image != null) image.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
    }

    // Show failGroup3
    if (failGroup3 != null)
{
    if (failGroup2 != null)
        failGroup2.SetActive(false);

    failGroup3.SetActive(true);

    Transform image = failGroup3.transform.Find("FailImage");
    if (image != null)
    {
        image.gameObject.SetActive(true);
        Debug.Log("FailImage shown.");
    }

    for (int i = 1; i <= 5; i++) 
    {
        Transform text = failGroup3.transform.Find($"DisplayText{i}");
        if (text != null)
        {
            Debug.Log($"Displaying DisplayText{i}");
            text.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(6f);
        }
        else
        {
            Debug.LogWarning($"Could not find DisplayText{i} in failGroup3.");
        }
    }
}


    if (failAudio != null)
        failAudio.Stop();
    
    if (cameraShake != null)
    cameraShake.StopShake();

    Debug.Log("You Fail! Temperature exceeded safe limits.");
}



    void HandleFailure()
{
    hasWon = true;

    if (gameUIGroup != null)
        gameUIGroup.SetActive(false);

    StartCoroutine(ShowFailSequence());
}


    void UpdateUI()
    {
        if (temperatureText != null)
            temperatureText.text = " " + temperature;

        if (pressureText != null)
            pressureText.text = " " + pressure;

        if (fluctuationText != null)
        {
            string sign = fluctuation >= 0 ? "+" : "";
            fluctuationText.text = " " + sign + fluctuation;
        }

        if (radiationText != null)
        {
            float radiationPercent = Mathf.Clamp01(temperature / 50000f); // Adjust scale as needed
            int radiationDisplay = Mathf.RoundToInt(radiationPercent * 100);
            radiationText.text = " " + radiationDisplay + "%";
        }
        if (quotaText != null)
            quotaText.text = " " + totalNetOutput;

        if (quotaDisplayText != null)
            quotaDisplayText.text = " " + totalNetOutput + " / " + winQuotaTarget;

        if (integrityText != null)
            integrityText.text = " " + Mathf.RoundToInt(integrity) + "%";

    }
}









