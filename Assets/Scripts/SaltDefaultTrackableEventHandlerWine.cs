using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SaltDefaultTrackableEventHandlerWine : DefaultTrackableEventHandler
{

    public VideoPlayer vp_Video;
    public MeshRenderer vp_Mesh;
    private Color meshDefaultColor;
    private bool hasVideoStarted;
    private int meshShaderID;
    private float fadePerSecond = 1.5f;

    private void Awake()
    {
        meshDefaultColor = vp_Mesh.GetComponent<Renderer>().material.color;
        meshShaderID = Shader.PropertyToID(vp_Mesh.material.shader.name);
        Debug.Log("TRACK AWAKE - The mesh shader ID is: " + meshShaderID + " The mesh default color is: " + meshDefaultColor);
        vp_Video.Prepare();
    }

    protected override void Start()
    {
        //meshDefaultColor = vp_Mesh.GetComponent<Renderer>().material.color;
        //meshShaderID = Shader.PropertyToID(vp_Mesh.material.shader.name);
        base.Start();
        //vp_Video.Prepare();
    }

    private void Update()
    {
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<VideoPlayer>().isPlaying)
        {
            //Debug.Log("This method was called");
            FadeAlpha(vp_Mesh);
        }
    }

    private void FadeAlpha(MeshRenderer mr)
    {
        var material = mr.GetComponent<Renderer>().material;
        var color = material.color;

        material.color = new Color(color.r, color.g, color.b, color.a + (fadePerSecond * Time.deltaTime));
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        RenderSettings.ambientIntensity = 5;
        //vp_Video.Play();
        //hasVideoStarted = true;
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<VideoPlayer>() != null)
        {
            mTrackableBehaviour.gameObject.GetComponentInChildren<VideoPlayer>().Play();
        }
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>() != null)
        {
            mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>().Play();
        }

        var lightComponents = GetComponentsInChildren<Light>(true);

        foreach (var component in lightComponents)
        {
            Debug.Log("I found a light " + component);
            component.enabled = true;
        }

    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        RenderSettings.ambientIntensity = 1;
        //vp_Video.Stop();  
        //hasVideoStarted = false;
        //vp_Mesh.GetComponent<Renderer>().material.SetColor(meshShaderID, meshDefaultColor);
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<VideoPlayer>() != null)
        {
            mTrackableBehaviour.gameObject.GetComponentInChildren<VideoPlayer>().Stop();
        }
        if (mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>() != null)
        {
            mTrackableBehaviour.gameObject.GetComponentInChildren<AudioSource>().Stop();
        }

        var lightComponents = GetComponentsInChildren<Light>(true);

        foreach (var component in lightComponents)
        {
            Debug.Log("I found a light " + component);
            component.enabled = false;
        }

        //vp_Mesh.GetComponent<Renderer>().material.SetColor(meshShaderID, meshDefaultColor);
        vp_Mesh.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0);
        Debug.Log("TRACK LOST - The mesh shader ID is: " + Shader.PropertyToID(vp_Mesh.material.shader.name) + " The mesh default color is: " + vp_Mesh.GetComponent<Renderer>().material.color);
    }
}
