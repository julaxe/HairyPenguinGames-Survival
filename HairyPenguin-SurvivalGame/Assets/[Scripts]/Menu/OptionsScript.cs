using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsScript : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public Toggle m_Toggle;
    public SaveOptions saveOptions;
    // Start is called before the first frame update
    void Start()
    {
        m_Toggle.isOn = saveOptions.getInverted();
    }

    // Update is called once per frame
    void Update()
    {
        volumeSlider.value = AudioListener.volume;
        saveOptions.setInverted(  m_Toggle.isOn);
    }
    public void OnVulomeChange()
    {

        AudioListener.volume = volumeSlider.value;

    }
   

}
