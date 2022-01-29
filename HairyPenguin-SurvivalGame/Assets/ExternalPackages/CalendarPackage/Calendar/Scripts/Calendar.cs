using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    //time variables
    [SerializeField] private int timeSpeedInMinutes = 30;
    [SerializeField] private int cooldownInSeconds = 1;
    [SerializeField] private bool debugModeNextDay = true;
    private double timer = 0;
    
    //public variables  to setup for the calendar in the editor
    public List<Season> seasons;
    public GameObject seasonPrefab;
    public GameObject dayPrefab;

    //private variables for the manage of the calendar
    private List<GameObject> _listOfSeasons;
    private GameObject _currentSeason;
    private int _currentSeasonIndex;
    private GameObject _seasonsParentObject;

    //UI variables
    private int _seasonShowing;
    private GameObject _seasonTitle;
    private GameObject _timeTitle;
    
    

    void Start()
    {
        _seasonsParentObject = transform.Find("Seasons").gameObject;
        _seasonTitle = transform.Find("Seasons/Header/SeasonTitle").gameObject;
        _timeTitle = transform.Find("Time").gameObject;
        
        _listOfSeasons = new List<GameObject>();
        InitializeCalendar();
        _seasonShowing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.N) && debugModeNextDay)
        // {
        //    GoToNextDay();
        // }
        UpdateTime();
    }

    public void InitializeCalendar()
    {
        foreach(var season in seasons)
        {
            var temp = Instantiate(seasonPrefab, _seasonsParentObject.transform);
            temp.GetComponent<SeasonComponent>().InitializeSeason(dayPrefab, season);
            _listOfSeasons.Add(temp);
        }

        StartCalendarSeason1();
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime;
        if (timer > cooldownInSeconds)
        {
            GameTime.Minutes += timeSpeedInMinutes;
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
                        GoToNextDay();
                    }
                    else if (GameTime.Hours >= 10)
                    {
                        GameTime.TensHours += GameTime.Hours / 10;
                        GameTime.Hours %= 10;
                       
                    }
                }

            }
            UpdateTimeTitle();
            timer = 0;
        }
    }
    public void StartCalendarSeason1()
    {
        _currentSeasonIndex = 0;
        _currentSeason = _listOfSeasons[_currentSeasonIndex];
        _currentSeason.GetComponent<SeasonComponent>().StartSeasonDay1();
        ShowSeason(_currentSeason);
    }

    private void GoToNextDay()
    {
        _currentSeason.GetComponent<SeasonComponent>().GoNextDay();
        if (_currentSeason.GetComponent<SeasonComponent>().IsSeasonOver())
        {
            GoToNextSeason();
        }
    }
    public void GoToNextSeason()
    {
        _currentSeasonIndex += 1;
        if (_currentSeasonIndex < _listOfSeasons.Count)
        {
            _currentSeason = _listOfSeasons[_currentSeasonIndex];
        }
        else
        {
            _currentSeasonIndex = 0;
            _currentSeason = _listOfSeasons[_currentSeasonIndex];
        }
        
        _currentSeason.GetComponent<SeasonComponent>().StartSeasonDay1();
        ShowSeason(_currentSeason);
    }
    public void ShowNextSeason()
    {
        _seasonShowing += 1;
        if (_seasonShowing < _listOfSeasons.Count)
        {
            ShowSeason(_listOfSeasons[_seasonShowing]);
        }
        else
        {
            _seasonShowing = 0;
            ShowSeason(_listOfSeasons[_seasonShowing]);
        }
    }

    public void ShowPreviousSeason()
    {
        _seasonShowing -= 1;
        if (_seasonShowing >= 0)
        {
            ShowSeason(_listOfSeasons[_seasonShowing]);
        }
        else
        {
            _seasonShowing = _listOfSeasons.Count - 1;
            ShowSeason(_listOfSeasons[_seasonShowing]);
        }
    }

    private void ShowSeason(GameObject seasonToShow)
    {
        foreach (var season in _listOfSeasons)
        {
            if (season == seasonToShow)
            {
                season.SetActive(true);
                UpdateSeasonTitle(season.GetComponent<SeasonComponent>().GetSeason().seasonName);
            }
            else
            {
                season.SetActive(false);
            }
        }
    }

    private void UpdateSeasonTitle(string title)
    {
        _seasonTitle.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = title;
    }

    private void UpdateTimeTitle()
    {
        _timeTitle.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Time: " + GameTime.TensHours.ToString() + GameTime.Hours.ToString() +  ":" + GameTime.TensMinutes + GameTime.Minutes;
    }
}
