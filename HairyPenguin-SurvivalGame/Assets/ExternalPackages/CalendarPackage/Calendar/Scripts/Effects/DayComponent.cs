
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayComponent : MonoBehaviour
{
    private Day _day;
    private GameObject _gridImages;
    private GameObject _dayNumber;
    private GameObject _isActive;
    private bool _dayIsActive;

    private void Update()
    {
        if (_dayIsActive)
        {
            int actualTimeInHours = (GameTime.TensHours * 10) + GameTime.Hours;
            foreach (var eEvent in _day.events)
            {
                if (eEvent.specificTime)
                {
                    if (eEvent.time == actualTimeInHours)
                    {
                        eEvent.ActivateEvent();
                    }
                }
            }
        }
    }

    public Day GetDay()
    {
        return _day;
    }

    public void SetDay(Day day)
    {
        _day = day;
        _gridImages = transform.Find("GridImages").gameObject;
        
        _isActive = transform.Find("IsActive").gameObject;
        _isActive.SetActive(false);
        
        //set images inside;
        if (day.events.Count > 0)
        {
            float relation = 50.0f;
            if (day.events.Count >= 3)
            {
                if (day.events.Count % 2 == 0)
                {
                    relation = 100.0f / (day.events.Count / 2.0f); 
                }
                else
                {
                    relation = 100.0f / (int) ((day.events.Count - 1.0f) / 2.0f);
                }
            }
                
            
            _gridImages.GetComponent<GridLayoutGroup>().cellSize = new Vector2(relation, relation);

            foreach (var eEvent in day.events)
            {
                GameObject eventImage = new GameObject("Image")
                {
                    transform =
                    {
                        parent = _gridImages.transform
                    }
                };
                eventImage.AddComponent<Image>().sprite = eEvent.image;
            }
        }
    }

    public void SetDayNumber(int dayNumber)
    {
        _dayNumber = transform.Find("DayNumber").gameObject;
        _dayNumber.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = dayNumber.ToString();
    }

    public void SetDayActive(bool active)
    {
        _dayIsActive = active;
        _isActive = transform.Find("IsActive").gameObject;
        _isActive.SetActive(active);
        if (active)
        {
            _day.ActivateDay();
        }
        else
        {
            _day.EndDay();
        }
    }
    
}
