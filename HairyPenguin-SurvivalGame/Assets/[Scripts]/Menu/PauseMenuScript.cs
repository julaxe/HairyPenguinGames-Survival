using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject pauseMenuToggle;

    public void Resume()
    {
        pauseMenuToggle.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }
    public void Pause()
    {
        pauseMenuToggle.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePaused = true;
    }
    public void LoadMainMenu()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Retry()
    {
        
        SceneManager.LoadScene("SampleScene");
    }
}
