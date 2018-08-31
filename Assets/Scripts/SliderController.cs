using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    public GameObject go_uiManager;

    public GameObject[] goSliderArray;

    // Use this for initialization
    void Start()
    {
        go_uiManager = GameObject.FindGameObjectWithTag("UIManager");

        go_uiManager.GetComponent<MixerController>();

        goSliderArray = GameObject.FindGameObjectsWithTag("Slider");

        for (int i = 0; i < goSliderArray.Length; i++)
        {
            //goSliderArray[i].GetComponent<Slider>().onValueChanged = 
            switch (goSliderArray[i].name)
            {
                case "MasterVolumeSlider":
                    go_uiManager.GetComponent<MixerController>().masterSlider = goSliderArray[i].GetComponent<Slider>();
                    break;
                //case "UIVolumeSlider":
                //    go_uiManager.GetComponent<MixerController>().uiSlider = goSliderArray[i].GetComponent<Slider>();
                //    break;
                case "AmbientVolumeSlider":
                    go_uiManager.GetComponent<MixerController>().ambientSlider = goSliderArray[i].GetComponent<Slider>();
                    break;
                //case "ARExpVolumeSlider":
                //    go_uiManager.GetComponent<MixerController>().arexpSlider = goSliderArray[i].GetComponent<Slider>();
                //    break;

            }
        }

    }	
}
