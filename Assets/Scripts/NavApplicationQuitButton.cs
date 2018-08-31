using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavApplicationQuitButton : MonoBehaviour {

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Application quitting!");
    }
}
