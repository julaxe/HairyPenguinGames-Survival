using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public class DayNightCycle : MonoBehaviour
{
    private double _timer;
    [Header("Time variables")]
    [SerializeField] private int increaseRate = 30;
    [SerializeField] private int tickRate = 1;

    [Header("Skyboxes")]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;
    
    [Header("directionalLight")]
    public Light directionalLight;
    [SerializeField] private float dayLightIntensity;
    [SerializeField] private float nightLightIntensity;

    void Start()
    {
        RenderSettings.skybox = daySkybox;
        directionalLight.intensity = dayLightIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

        if (GameTime.TensHours == 1 &&
            GameTime.Hours == 8)
        {
            if (RenderSettings.skybox == nightSkybox) return;
            RenderSettings.skybox = nightSkybox;
            directionalLight.intensity = nightLightIntensity;
            GameTime.Night = true;
        }
        else if (GameTime.TensHours == 0 &&
                 GameTime.Hours == 6)
        {
            if (RenderSettings.skybox == daySkybox) return;
            RenderSettings.skybox = daySkybox;
            directionalLight.intensity = dayLightIntensity;
            GameTime.Night = false;
        }
    }
    
    private void UpdateTime()
    {
        _timer += Time.deltaTime;
        if (_timer > tickRate)
        {
            GameTime.Minutes += increaseRate;
            if (GameTime.Minutes >= 10)
            {
                GameTime.TensMinutes += GameTime.Minutes / 10;
                GameTime.Minutes %= 10;
                if (GameTime.TensMinutes >= 6)
                {
                    GameTime.Hours += GameTime.TensMinutes / 6;
                    GameTime.TensMinutes %= 6;
                    if (GameTime.TensHours >= 2 && GameTime.Hours >= 4)
                    {
                        GameTime.TensHours -= 2;
                        GameTime.Hours -= 4;
                        //GoToNextDay();
                    }
                    else if (GameTime.Hours >= 10)
                    {
                        GameTime.TensHours += GameTime.Hours / 10;
                        GameTime.Hours %= 10;
                    }
                }

            }
            _timer = 0;
            Debug.Log(GameTime.TensHours.ToString() + GameTime.Hours.ToString() + ":" + GameTime.TensMinutes.ToString() + GameTime.Minutes.ToString());
        }
    }
}
