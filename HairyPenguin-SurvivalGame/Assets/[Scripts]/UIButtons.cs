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
    public GameObject ControlsCanvas;
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

    public void OnControlsClicked()
    {
        ControlsCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void OnBackClicked()
    {
        OptionsCanvas.SetActive(false);
        playCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        ControlsCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
    
    public void OnNewGameClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnLoadGameClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
