
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 1)]
public class Event : ScriptableObject
{
    public Sprite image;

    public bool specificTime = false;
    [Range(0,23)]
    public int time;
    
    public List<Effect> effects;
    private Effect _currentEffect;

    public void ActivateEvent()
    {
        if (effects.Count > 0)
        {
            foreach (var effect in effects)
            {
                _currentEffect = effect;
                _currentEffect.ActivateEffect();
            }
        }
    }

    public void EndEvent()
    {
        foreach (var effect in effects)
        {
            _currentEffect = effect;
            _currentEffect.ExitEffect();
        }
    }

}
