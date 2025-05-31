using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;
    public GameObject loadingScreen;
    public Slider loadingBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void SwitchToScene(int id)
{
    loadingScreen.SetActive(true);
    loadingBar.value = 0;
    StartCoroutine(SwitchToSceneAsync(id));
}

IEnumerator SwitchToSceneAsync(int id)
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
    while (!asyncLoad.isDone)
    {
        loadingBar.value = asyncLoad.progress;
        yield return null;
    }

    yield return new WaitForSeconds(0.2f);
    loadingScreen.SetActive(false);
}

    void Start()
    {

    }

    void Update()
    {

    }
}
