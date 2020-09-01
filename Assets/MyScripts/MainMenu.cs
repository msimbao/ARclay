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
    public void StartSolids()
    {
        // SceneManager.LoadScene("AR");
        StartCoroutine(LoadAsynchronously("Solids"));
    }
    public void StartStarter()
    {
        // StartCoroutine(LoadAsynchronously("Starter"));
    }
     public void StartSmall()
    {
        StartCoroutine(LoadAsynchronously("Small"));
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
    // public void PressButton()
    // {
    //     FindObjectOfType<AudioManager>().Play("Button");
    // }
    //   public void SoundEffectOne()
    // {
    //     FindObjectOfType<AudioManager>().Play("EffectOne");
    // }
    //     public void SoundEffectTwo()
    // {
    //     FindObjectOfType<AudioManager>().Play("EffectTwo");
    // }
    //     public void SoundEffectThree()
    // {
    //     FindObjectOfType<AudioManager>().Play("EffectThree");
    // }
    //         public void SoundEffectFour()
    // {
    //     FindObjectOfType<AudioManager>().Play("EffectFour");
    // }
            public void BackButtonPressed()
    {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            LoaderUtility.Deinitialize();
    }

    IEnumerator LoadAsynchronously (string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        
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
