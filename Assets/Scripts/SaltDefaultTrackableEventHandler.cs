using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltDefaultTrackableEventHandler : DefaultTrackableEventHandler {

    public GameObject GO_Shaker;
    public GameObject GO_FilmRays;
    private GameObject go_OneShotAudio;
    public GameObject GO_VideoPlayer;
    public GameObject GO_VideoAudio;

    protected override void Start()
    {
        base.Start();

        if (GameObject.Find("One shot audio") != null)
        {
            go_OneShotAudio = GameObject.Find("One shot audio");
        }

        //if (GameObject.Find("VideoAudio") != null)
        //{
        //    go_VideoAudio = GameObject.Find("VideoAudio");
        //}

        //if (GameObject.Find("VideoPlayer") != null)
        //{
        //    GameObject go_VideoPlayer = GameObject.Find("VideoPlayer");
        //}
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        var animatorComponents = GetComponentsInChildren<Animator>(true);

        GO_Shaker.SetActive(true);
        // Enable animators:
        foreach (var component in animatorComponents)
        {
            Debug.Log("This is the animator component name " + component.name);

            if (component == true)
            {
                component.enabled = true;
                if (component.name == "SaltShakerAni5")
                {
                    component.Play("Take 001");
                }
            }
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        var animatorComponents = GetComponentsInChildren<Animator>(true);

        GO_Shaker.SetActive(false);
        GO_FilmRays.SetActive(false);
        // Enable animators:
        foreach (var component in animatorComponents)
        {
            component.StopPlayback();
            component.enabled = false;
        }

        // Disable sound:
        if (go_OneShotAudio != null)
        {
            Destroy(go_OneShotAudio);
        }

        //Sound fix for audio that is playing as part of a videoplayer attached to a gameobject(03 - 06 - 2018)
        if (GO_VideoPlayer != null)
            {
                GO_VideoPlayer.SetActive(false);
                Debug.Log("VideoPlayer stop fix");
            }

        if (GO_VideoAudio != null)
        {
            GO_VideoAudio.SetActive(false);
            Debug.Log("VideoAudio stop fix");
        }

        // Sound fix for audio that is still playing when tracking is lost (03-06-2018)
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>() != null)
        {
            mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>().Stop();
            //videoPlayer.Stop();
        }

    }


}
