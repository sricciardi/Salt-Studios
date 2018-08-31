using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vbExitButton : MonoBehaviour {

    public Button vbExit;

    // Use this for initialization
    void Start() {
        Button btn = vbExit.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update() {


    }

    void TaskOnClick() {
        Application.Quit();
    }
}
