using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoozyUI;
using UnityEngine.Scripting;


public class SwipeButtonController : MonoBehaviour
{
    public GameObject GlobalVariables;

    // Use this for initialization
    void Start()
    {
        GlobalVariables = GameObject.FindGameObjectWithTag("UIManager");
    }

    // Update is called once per frame
    void Update()
    {

        if (!GlobalVariables.GetComponent<GlobalVariables>().hasSwipeAnimationPlayed)
        {
            if (gameObject.GetComponent<UIButton>().isActiveAndEnabled)
            {
                Debug.Log("the swipe button has been enabled and is active");
                GlobalVariables.GetComponent<GlobalVariables>().hasSwipeAnimationPlayed = true;
            }
        }


        //if (gameObject.GetComponent<UIButton>().normalLoop.TotalDuration == 4) ;
        //{
        //    Debug.Log("The loop total duration is greater than or equal to 1");
        //}

        if (Input.anyKeyDown || Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {
            Debug.Log("You just pressed the screen");
            //gameObject.GetComponent<Animator>().enabled = true;
            GlobalVariables.GetComponent<GlobalVariables>().isFirstRun = false;
            //gameObject.SetActive(false);
        }

    }

    public void DisableGameObjectController()
    {
        gameObject.SetActive(false);
    }


}
