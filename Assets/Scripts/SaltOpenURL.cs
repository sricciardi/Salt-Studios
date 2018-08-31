using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltOpenURL : MonoBehaviour {

    public string whatToOpen;

    public void ButtonOpenURL()
    {
        Application.OpenURL(whatToOpen);
    }
}
