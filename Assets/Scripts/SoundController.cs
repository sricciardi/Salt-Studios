using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour {

    public AudioSource UIPlayer;
    public AudioSource AmbientPlayer;
    public AudioSource ARExpPlayer;

    //public GameObject go_ARExpAudioSource;

    [Header("UI Sound")]
    public AudioClip UIButtonSound;
    public AudioClip UIPanelSound;
    public AudioClip UIBackSound;

    [Header("AMBIENT Sound")]
    public AudioClip AmbientStartSound;
    public AudioClip AmbientLoopSound;

    [Header("AR Exp Sound")]
    public AudioClip ARExpSound;

    private void Awake()
    {
        SceneManager.activeSceneChanged += SceneChanged;
    }

    private void SceneChanged(Scene current, Scene next)
    {
        //if (current.name == "SaltBrochure")
        //{
        //    go_ARExpAudioSource =  GameObject.FindGameObjectWithTag("ARExpAudioSource");
        //    Debug.Log("the gameobject with the audio source is called " + go_ARExpAudioSource.name);
        //}
        //Debug.Log("The current scene is " + current.name);
        //Debug.Log("The next scene is " + next.name);
    }


    private void Start()
    {
        AmbientPlayer.clip = AmbientLoopSound;
        AmbientPlayer.Play();
    }

    public void OnGameEvent(string gameEvent)
    {
        //AmbientPlayer.Stop();
    }

    public void OnButtonEvent(string buttonName)
    {
        Debug.Log("this is the button name " + buttonName);
        switch (buttonName)
        {
            case "UIB-ARButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-ExploreButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-HowToButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-WhoButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-ContactButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-ExitButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                break;
            case "UIB-NAV-SettingsButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIPanelSound;
                UIPlayer.Play();
                break;
            case "UIB-NAV-BackButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIBackSound;
                UIPlayer.Play();
                break;
            case "UIB-AR-SaltBrochureButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                AmbientPlayer.Stop();
                break;
            case "UIB-AR-SaltWineBottleButton":
                //AmbientPlayer.Stop();
                UIPlayer.clip = UIButtonSound;
                UIPlayer.Play();
                AmbientPlayer.Stop();
                break;
        }
    }
}
