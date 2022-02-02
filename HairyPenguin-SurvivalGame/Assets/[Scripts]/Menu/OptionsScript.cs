using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsScript : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.value = AudioListener.volume;
    }
    public void OnVulomeChange()
    {

        AudioListener.volume = volumeSlider.value;

    }
}
