using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForRenderCameraOnCanvas : MonoBehaviour {

    public GameObject go_renderCamera;
    public Camera renderCamera;
    public Canvas settingsPanelCanvas;

    private void Awake()
    {
        //go_renderCamera = GameObject.FindGameObjectWithTag("MainCamera");
        settingsPanelCanvas = this.GetComponent<Canvas>();

        /*
        if (go_renderCamera.name == "MainUICamera")
        {
            //renderCamera = go_renderCamera.GetComponent<Camera>();
            this.GetComponent<Canvas>().worldCamera = go_renderCamera.GetComponent<Camera>();
            //settingsPanelCanvas.worldCamera = renderCamera;
            //this.GetComponent<Renderer>() = renderCamera;
        }
        */

        //renderCamera = this.GetComponent<Renderer>();
    }

    private void Init()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera") && GameObject.FindGameObjectWithTag("MainCamera").name == "MainUICamera")
        {
            go_renderCamera = GameObject.FindGameObjectWithTag("MainCamera");
            this.GetComponent<Canvas>().worldCamera = go_renderCamera.GetComponent<Camera>();
        }
        //go_renderCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    // Use this for initialization
    void Start () {
        Debug.Log("this is the render camera " + this.GetComponent<Renderer>());
		if (this.GetComponent<Renderer>() == null)
        {
            Debug.Log("There is no renderer attached to this gameobject");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (settingsPanelCanvas.worldCamera == null)
        {
            Debug.Log("There is no render camera attached to the canvas");
            Init();
        }

        /*
        if (go_renderCamera.name == "MainUICamera" && settingsPanelCanvas.worldCamera == null)
        {
            Debug.Log("I am inside the IF statement");
            //renderCamera = go_renderCamera.GetComponent<Camera>();
            this.GetComponent<Canvas>().worldCamera = go_renderCamera.GetComponent<Camera>();
            //settingsPanelCanvas.worldCamera = renderCamera;
            //this.GetComponent<Renderer>() = renderCamera;
        }
        */
    }
}
