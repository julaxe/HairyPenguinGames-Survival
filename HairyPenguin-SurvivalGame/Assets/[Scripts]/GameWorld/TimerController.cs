using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public static float timer = 0;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.RoundToInt(timer % 60);

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

        text.text = minutes_str + " : " + seconds_str;
    }
}
