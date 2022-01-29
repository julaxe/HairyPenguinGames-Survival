
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Day", menuName = "ScriptableObjects/Day", order = 1)]
public class Day : ScriptableObject
{
    public List<Event> events;
    private Event _currentEvent;

    private void Start()
    {
        
    }

    public void ActivateDay()
    {
        foreach (var eEvent in events)
        {
            if (eEvent.specificTime) continue;
            _currentEvent = eEvent;
            _currentEvent.ActivateEvent();
        }
    }

    public void EndDay()
    {
        foreach (var eEvent in events)
        {
            _currentEvent = eEvent;
            _currentEvent.EndEvent();
        }
    }
}
