using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltDefaultTrackableEventHandlerWinLose : DefaultTrackableEventHandler {

    public GameObject goAnimation;
    public GameObject goAudio;
    //public GameObject goAnimationLose;
    private Animator animator;
    //private Animator animatorLose;
    //public Animation loseAnimation;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        animator = goAnimation.GetComponent<Animator>();
        //animatorLose = goAnimationLose.GetComponent<Animator>();
    }

    protected override void OnTrackingFound() {
        base.OnTrackingFound();
        if (!goAnimation.activeSelf && !goAudio.activeSelf) {
            goAnimation.SetActive(true);
            if (goAnimation.name == "YouWin") {
                animator.Play("YouWinAnim");
                goAudio.SetActive(true);
            } else {
                animator.Play("YouLoseAnim");
                goAudio.SetActive(true);
            }
            

            //goAnimationLose.SetActive(true);
            //animatorLose.Play("YouLoseAnim");
        }   
    }

    protected override void OnTrackingLost() {
        base.OnTrackingLost();   
        if (goAnimation.activeSelf && goAudio.activeSelf) {
            animator.StopPlayback();
            goAnimation.SetActive(false);
            goAudio.SetActive(false);

            //animatorLose.StopPlayback();
            //goAnimationLose.SetActive(false);
        }     
    }
}
