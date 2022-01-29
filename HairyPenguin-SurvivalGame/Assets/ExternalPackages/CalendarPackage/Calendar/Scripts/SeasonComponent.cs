
using System.Collections.Generic;
using UnityEngine;

public class SeasonComponent : MonoBehaviour
{
    private Season _season;
    private List<GameObject> _days;
    private GameObject _currentDay;
    private int _currentDayIndex;
    private bool _isSeasonOver = false;

    public void InitializeSeason(GameObject dayPrefab, Season season)
    {
        _season = season;
        _days = new List<GameObject>();
        for (int i = 0; i < _season.days.Count; i++)
        {
            var temp = Instantiate(dayPrefab, transform.Find("SeasonDays"));
            temp.GetComponent<DayComponent>().SetDay(_season.days[i]);
            temp.GetComponent<DayComponent>().SetDayNumber(i+1);
            _days.Add(temp);
        }
    }

    public void GoNextDay()
    {
        _currentDayIndex += 1;
        if (_currentDayIndex < _days.Count)
        {
            if(_currentDay != null)
                _currentDay.GetComponent<DayComponent>().SetDayActive(false);
            _currentDay = _days[_currentDayIndex];
            _currentDay.GetComponent<DayComponent>().SetDayActive(true);
        }
        else
        {
            if(_currentDay != null)
                _currentDay.GetComponent<DayComponent>().SetDayActive(false);
            _isSeasonOver = true;
        }
    }

    public void StartSeasonDay1()
    {
        _currentDayIndex = -1;
        GoNextDay();
        _isSeasonOver = false;
    }

    public bool IsSeasonOver()
    {
        return _isSeasonOver;
    }

    public Season GetSeason()
    {
        return _season;
    }
    
}
