using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] string volumeParam = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;

    // Start is called before the first frame update
    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParam);
    }
    void Awake()
    {
        slider.onValueChanged.AddListener(CustomControlSound);
        toggle.onValueChanged.AddListener(ToggleSound);
    }

    private void ToggleSound(bool toggleValue)
    {
        if (toggleValue)//from toggle event if u toggle it will give u true here .
        {
            slider.value = 0;
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat(volumeParam);
        }


        if (slider.value == 0)
        {
            toggle.isOn = true;
        }
    }

    private void CustomControlSound(float arg0)
    {
        //  await Task.Delay(100);
    
        mixer.SetFloat(volumeParam, (1 - Mathf.Sqrt(arg0)) * -80f);
        PlayerPrefs.SetFloat(volumeParam, slider.value);
 

        if (slider.value==0)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
    }
 
}
