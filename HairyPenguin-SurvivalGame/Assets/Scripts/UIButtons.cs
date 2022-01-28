using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIButtons : MonoBehaviour
{

    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Showcase");
    }

    public void OnOptionsClicked()
    {
        SceneManager.LoadScene("Options");
    }


    public void OnCreditsClicked()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
