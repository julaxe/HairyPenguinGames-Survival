using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIButtons : MonoBehaviour
{
    public GameObject playCanvas;
    public GameObject MainMenuCanvas;
    public GameObject OptionsCanvas;
    public GameObject CreditsCanvas;
   
    public GameObject HowToPlayCanvas;
    public SaveOptions saveOptions;
    public void OnPlayClicked()
    {
        playCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }
   
    public void OnOptionsClicked()
    {
        OptionsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }


    public void OnCreditsClicked()
    {
        CreditsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

  
    public void OnHowToPlayClicked()
    {
        HowToPlayCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void OnBackClicked()
    {
        OptionsCanvas.SetActive(false);
        playCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
       
        HowToPlayCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
    
    public void OnNewGameClicked()
    {
        saveOptions.setLoading(false);
        SceneManager.LoadScene("MainLevel");
    }
    public void OnLoadGameClicked()
    {
        saveOptions.setLoading(true);
        SceneManager.LoadScene("MainLevel");
    }
}
