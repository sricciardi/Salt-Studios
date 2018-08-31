using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachAudioSourceToSoundController : MonoBehaviour {

    [Header("GameObject Target Parent")]
    public GameObject go_targetParent;

    public GameObject go_uiManager;
    public GameObject go_arexpAudioSource;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("UIManager"))
        {
            go_uiManager = GameObject.FindGameObjectWithTag("UIManager");
            if (go_uiManager.GetComponent<SoundController>().ARExpSound == null)
            {
                Debug.Log("the audio clip name is " + go_arexpAudioSource.GetComponent<AudioSource>().clip);
                Debug.Log("there is no sound clip in the sound controller");
                go_uiManager.GetComponent<SoundController>().ARExpSound = go_arexpAudioSource.GetComponent<AudioSource>().clip;
            }
        }

        //if (GameObject.FindGameObjectWithTag("ARExpAudioSource"))
        //{
        //    go_arexpAudioSource = GameObject.FindGameObjectWithTag("ARExpAudioSource");
        //}
    }

    private void OnEnable()
    {
        //if (go_uiManager.GetComponent<SoundController>().ARExpSound == null)
        //{
        //    go_uiManager.GetComponent<SoundController>().ARExpSound = this.GetComponent<AudioClip>();
        //}
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
