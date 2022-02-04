using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthController : MonoBehaviour
{
    private float _CurrentHealth;
    private float _MaxHealth;
    private Slider _UIHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        _UIHealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        _CurrentHealth = _UIHealthBar.maxValue = _MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth(int amount)
    {
        _CurrentHealth += amount;
        if(_CurrentHealth > _MaxHealth)
        {
            _CurrentHealth = _MaxHealth; 
        }
        if(_CurrentHealth < 0)
        {
            _CurrentHealth = 0;
        }
        _UIHealthBar.value = _CurrentHealth;

    }
}
