using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ARExpMixerController : MonoBehaviour {

    public AudioMixer audioMixer;

    //[Space(10)]
    //public Slider masterSlider;
    //public Slider uiSlider;
    //public Slider ambientSlider;
    //public Slider arexpSlider;

    //public GameObject[] goSlider;
    //public Slider[] arraySlider;

    private void Awake()
    {
        //goSlider = GameObject.FindGameObjectsWithTag("Slider");

        //for (int i = 0; i < goSlider.Length; i++)
        //{
        //    switch (goSlider[i].name)
        //    {
        //        case "MasterVolumeSlider":
        //            masterSlider = goSlider[i].GetComponent<Slider>();
        //            break;
        //        case "UIVolumeSlider":
        //            uiSlider = goSlider[i].GetComponent<Slider>();
        //            break;
        //        case "AmbientVolumeSlider":
        //            ambientSlider = goSlider[i].GetComponent<Slider>();
        //            break;
        //        case "ARExpVolumeSlider":
        //            arexpSlider = goSlider[i].GetComponent<Slider>();
        //            break;

        //    }
        //    //if (goSlider[i].name == "MasterVolumeSlider")
        //    //{
        //    //    masterSlider = goSlider[i].GetComponent<Slider>();
        //    //}
        //}
    }

    private void Start()
    {
        //masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0);
        //uiSlider.value = PlayerPrefs.GetFloat("uiVolume", 0);
        //ambientSlider.value = PlayerPrefs.GetFloat("ambientVolume", 0);
        //arexpSlider.value = PlayerPrefs.GetFloat("arexpVolume", 0);
    }

    private void OnDisable()
    {
        float masterVolume;
        float uiVolume;
        float ambientVolume;
        float arexpVolume;

        audioMixer.GetFloat("masterVolume", out masterVolume);
        audioMixer.GetFloat("uiVolume", out uiVolume);
        audioMixer.GetFloat("ambientVolume", out ambientVolume);
        audioMixer.GetFloat("arexpVolume", out arexpVolume);

        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("uiVolume", uiVolume);
        PlayerPrefs.SetFloat("ambientVolume", ambientVolume);
        PlayerPrefs.SetFloat("arexpVolume", arexpVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetUIVolume(float volume)
    {
        audioMixer.SetFloat("uiVolume", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        audioMixer.SetFloat("ambientVolume", volume);
    }

    public void SetARExpVolume(float volume)
    {
        audioMixer.SetFloat("arexpVolume", volume);
    }
}
