using UnityEngine;
using UnityEngine.UI;
using DoozyUI;
using DG.Tweening;
using System.Collections;

public class SnapScrollingNew : MonoBehaviour {
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 100f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Range(0f, 1f)]
    public float unfocusedTrans;
    [Range(0f, 1f)]
    public float focusedTrans;
    [Range(0f, 2f)]
    public float unfocusedScale;
    [Range(0f, 2f)]
    public float focusedScale;
    [Header("Other Objects")]

    public GameObject[] carouselButtons;
    public bool startWelcomeScreen;
    private float height;
    private float mousePositionStartY;
    private float mousePositionEndY;
    private float dragAmount;
    private float screenPosition;
    private float lastScreenPosition;
    private float lerpTimer;
    private float lerpPage;
    private float screenHeight;
    private float screenWidth;

    public int selectedButton;
    public int maxSelectedButton;

    public int swipeThrustHold = 30;
    private bool canSwipe;

    public GameObject canvasWindow;

    #region Mono Functions

    void Start() {
        height = canvasWindow.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < carouselButtons.Length; i++) {

            carouselButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, ((-height + -panOffset) * i));

        }
        selectedButton = 0;
        maxSelectedButton = carouselButtons.Length - 1;

        startWelcomeScreen = true;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        //Debug.Log("Screen Height: " + screenHeight + " Screen Width: " + screenWidth);
        lastScreenPosition = screenPosition;
        //Debug.Log("screenposition at start is " + screenPosition);
        lerpTimer = lerpTimer + Time.deltaTime;

        StartCoroutine("AnimateButtonCoroutine");
    }

    IEnumerator AnimateButtonCoroutine() {
        AnimateMenu("down");
        yield return null;
    }

    void Update() {

        if (!startWelcomeScreen) {
            //startWelcomeScreen = true;
            return;
        }

        lerpTimer = lerpTimer + Time.deltaTime;
        if (lerpTimer < .333) {
            //Debug.Log("I am LERPING the PAGE - LastScreenPosition " + lastScreenPosition + " - LerpPage: " + lerpPage);
            screenPosition = Mathf.Lerp(lastScreenPosition, lerpPage * 1, lerpTimer * 3);
            lastScreenPosition = screenPosition;
        }

        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > 0 && Input.mousePosition.x < screenWidth) {
            canSwipe = true;
            mousePositionStartY = Input.mousePosition.y;
            Debug.Log("Mouse Button X Position " + Input.mousePosition.x);
        }

        if (Input.GetMouseButtonUp(0) && Input.mousePosition.x > 0 && Input.mousePosition.x < screenWidth) {
            //canSwipe = true;
            if (Mathf.Abs(dragAmount) < swipeThrustHold) {
                lerpTimer = 0;
                mousePositionStartY = Input.mousePosition.y;
                Debug.Log("MousePositionY: " + mousePositionStartY);
                if (mousePositionStartY > 0 && mousePositionStartY <= ((screenHeight / 2) - (height / 2)) && selectedButton != maxSelectedButton) {
                    AnimateMenu("down");
                } else if (mousePositionStartY > ((screenHeight / 2) + (height / 2)) && mousePositionStartY <= screenHeight && selectedButton != 0) {
                    AnimateMenu("up");
                }
            }
        }

        if (Input.GetMouseButton(0)) {
            if (canSwipe) {
                mousePositionEndY = Input.mousePosition.y;
                dragAmount = mousePositionEndY - mousePositionStartY;
                screenPosition = lastScreenPosition + dragAmount;
            }
        }

        if (Mathf.Abs(dragAmount) > swipeThrustHold && canSwipe) {
            canSwipe = false;
            lastScreenPosition = screenPosition;
            if (selectedButton == 0 && dragAmount > 0)
                lerpTimer = 0;
            else if (selectedButton < maxSelectedButton)
                OnSwipeComplete();
            else if (selectedButton == maxSelectedButton && dragAmount < 0)
                lerpTimer = 0;
            else if (selectedButton == maxSelectedButton && dragAmount > 0)
                OnSwipeComplete();
        }

        if (Input.GetMouseButtonUp(0)) {
            if (Mathf.Abs(dragAmount) < swipeThrustHold) {
                lerpTimer = 0;
            }
        }
        StartCoroutine("RefreshButtons");
        //for (int i = 0; i < carouselButtons.Length; i++) {
        //    carouselButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, screenPosition + ((-height + -panOffset) * i));
        //    Debug.Log("The SelectedButton is " + selectedButton);
        //    if (i == selectedButton) {
        //        carouselButtons[i].GetComponent<RectTransform>().localScale = Vector3.Lerp(carouselButtons[i].GetComponent<RectTransform>().localScale, new Vector3(focusedScale, focusedScale, focusedScale), Time.deltaTime * 5);
        //        carouselButtons[i].GetComponentInChildren<Image>().SetTransButtonImageColor(focusedTrans);
        //        carouselButtons[i].GetComponent<UIButton>().Interactable = true;
        //        carouselButtons[i].GetComponent<UIButton>().enabled = true;
        //    } else {
        //        carouselButtons[i].GetComponent<RectTransform>().localScale = Vector3.Lerp(carouselButtons[i].GetComponent<RectTransform>().localScale, new Vector3(unfocusedScale, unfocusedScale, unfocusedScale), Time.deltaTime * 5);
        //        carouselButtons[i].GetComponentInChildren<Image>().SetTransButtonImageColor(unfocusedTrans);
        //        carouselButtons[i].GetComponent<UIButton>().Interactable = false;
        //        carouselButtons[i].GetComponent<UIButton>().enabled = false;
        //    }
        //}
    }

    #endregion

    IEnumerator RefreshButtons() {
        for (int i = 0; i < carouselButtons.Length; i++) {
            carouselButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, screenPosition + ((-height + -panOffset) * i));
            Debug.Log("The SelectedButton is " + selectedButton);
            if (i == selectedButton) {
                carouselButtons[i].GetComponent<RectTransform>().localScale = Vector3.Lerp(carouselButtons[i].GetComponent<RectTransform>().localScale, new Vector3(focusedScale, focusedScale, focusedScale), Time.deltaTime * 5);
                carouselButtons[i].GetComponentInChildren<Image>().SetTransButtonImageColor(focusedTrans);
                carouselButtons[i].GetComponent<UIButton>().Interactable = true;
                carouselButtons[i].GetComponent<UIButton>().enabled = true;
            } else {
                carouselButtons[i].GetComponent<RectTransform>().localScale = Vector3.Lerp(carouselButtons[i].GetComponent<RectTransform>().localScale, new Vector3(unfocusedScale, unfocusedScale, unfocusedScale), Time.deltaTime * 5);
                carouselButtons[i].GetComponentInChildren<Image>().SetTransButtonImageColor(unfocusedTrans);
                carouselButtons[i].GetComponent<UIButton>().Interactable = false;
                carouselButtons[i].GetComponent<UIButton>().enabled = false;
            }
        }
        yield return null;
    }

    private void OnSwipeComplete() {
        lastScreenPosition = screenPosition;
        if (dragAmount > 0)  // SWIPING UP!
        {
            Debug.Log("DRAG AMOUNT GREATER THAN 0! SWIPING UP!!!");
            if (Mathf.Abs(dragAmount) > (swipeThrustHold)) {
                AnimateMenu("up");
            } else {
                lerpTimer = 0;
            }

        } else if (dragAmount < 0)  // SWIPING DOWN!
        {
            Debug.Log("DRAG AMOUNT LESS THAN 0! SWIPING DOWN!!!");
            if (Mathf.Abs(dragAmount) > (swipeThrustHold)) {
                AnimateMenu("down");
            } else {
                lerpTimer = 0;
            }
        }
    }

    private void AnimateMenu(string buttonDirection) {
        switch (buttonDirection) {
            case "up":
                lerpTimer = 0;
                selectedButton--;
                lerpPage = (height + panOffset) * selectedButton;
                StartCoroutine("RefreshButtons");
                break;
            case "down":
                lerpTimer = 0;
                selectedButton++;
                lerpPage = (height + panOffset) * selectedButton;
                StartCoroutine("RefreshButtons");
                break;
            default:
                break;
        }
    }
}
