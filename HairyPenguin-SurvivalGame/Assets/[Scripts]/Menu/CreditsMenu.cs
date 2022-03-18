using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsMenu : MonoBehaviour
{
    public TextMeshProUGUI timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
        float minutes = Mathf.Floor(TimerController.timer / 60);
        float seconds = Mathf.RoundToInt(TimerController.timer % 60);

        string minutes_str = minutes.ToString();
        string seconds_str = Mathf.RoundToInt(seconds).ToString();

        if (minutes < 10)
        {
            minutes_str = "0" + minutes.ToString();
        }
        if (seconds < 10)
        {
            seconds_str = "0" + Mathf.RoundToInt(seconds).ToString();
        }

        timeRemaining.text = "Time Spent Playing: " +  minutes_str + " : " + seconds_str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
