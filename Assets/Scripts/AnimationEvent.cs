using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AnimationEvent : MonoBehaviour {

    private GameObject audioClipPlaying;
    //public AudioClip AE_PlayAtStartAudioClip;
    public GameObject VideoPlayer;
    public GameObject AudioPlayer;
    public GameObject FilmLightRays;
    public GameObject GO_doorAudio;
    private AudioSource doorAudio;

	// Use this for initialization
	//public void PlayIdleLoopSound () {

 //       audioClipPlaying = GameObject.Find("One shot audio");
 //       if ( !audioClipPlaying )
 //       {
 //           AudioSource.PlayClipAtPoint(AE_PlayAtStartAudioClip, this.transform.position, 1);
 //       } else
 //       {
 //           Destroy(audioClipPlaying);
 //           AudioSource.PlayClipAtPoint(AE_PlayAtStartAudioClip, this.transform.position, 1);
 //       }

 //       //AudioSource.PlayClipAtPoint(idleAudioClip, this.transform.position);
	//}
	
    public void PlayVideo() {
        //VideoPlayer = GameObject.Find("VideoPlayer");
        //AudioPlayer = GameObject.Find("AudioPlayer");

        VideoPlayer.SetActive(true);
        AudioPlayer.SetActive(true);
        FilmLightRays.SetActive(true);
    }

    public void SpaceDoorAudio() {

        doorAudio = GO_doorAudio.GetComponent<AudioSource>();
        doorAudio.Play();

    }
}
