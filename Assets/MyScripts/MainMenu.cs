using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    // Start is called before the first frame update
    public void StartAR()
    {
        // SceneManager.LoadScene("AR");
        StartCoroutine(LoadAsynchronously("AR"));
    }
    public void StartXR()
    {
        StartCoroutine(LoadAsynchronously("XR"));
    }
    public void QuitGame()
    {
        Debug.Log("QUITT!!");
        Application.Quit();
    }
    public void BackButton()
    {
        FindObjectOfType<AudioManager>().Play("Back");
    }
    public void PressButton()
    {
        FindObjectOfType<AudioManager>().Play("Button");
    }
            public void BackButtonPressed()
    {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            LoaderUtility.Deinitialize();
    }

    IEnumerator LoadAsynchronously (string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            // Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }
}
