//This script from tutorial by AndroidHelper.
//Link to the channel: https://www.youtube.com/c/huaweisonichelpAHRU
// Make sure you "Scroll View" is either Horizontal or Vertical but NOT both!

using UnityEngine;
using UnityEngine.UI;

public class SaltSnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject[] panPrefab;
    public ScrollRect scrollRect;

    public RectTransform buttonDefault;

    //private GameObject[] panPrefab;
    public GameObject[] instPans;
    public GameObject[] newPanPrefab;

    public GameObject tempButton;

    public Vector2[] pansPos;
    public Vector2[] pansScale;

    public RectTransform contentRect;
    public Vector2 contentVector;

    public int selectedPanID;
    public bool isScrolling;

    public bool horizontalScroll;
    public bool verticalScroll;

    private void Awake()
    {
        horizontalScroll = GetComponentInParent<ScrollRect>().horizontal;
        verticalScroll = GetComponentInParent<ScrollRect>().vertical;
        buttonDefault = panPrefab[0].GetComponent<RectTransform>();
        Debug.Log("panprefab length is " + panPrefab.Length);
    }

    private void Start()
    { 
        contentRect = GetComponent<RectTransform>();
        //panPrefab = new GameObject[panCount];
        tempButton = new GameObject("temp");
        instPans = new GameObject[panPrefab.Length];
        pansPos = new Vector2[panPrefab.Length];
        pansScale = new Vector2[panPrefab.Length];
        //newPanPrefab = new GameObject[panPrefab.Length];

        if (horizontalScroll)
        {
			// Original untouched code
			/*
			for (int i = 0; i < panCount; i++)
		    {
			instPans[i] = Instantiate(panPrefab, transform, false);
			if (i == 0) continue;
			instPans[i].transform.localPosition = new Vector2(instPans[i-1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
				instPans[i].transform.localPosition.y);
			pansPos[i] = -instPans[i].transform.localPosition;
		    }
            */


            for (int i = 0; i < panPrefab.Length; i++)
            {
                //instPans[i] = new GameObject(panPrefab[i].name);
                if (i == 0) continue;
                Debug.Log("number " + i + " is panprefab name is " + panPrefab[i].name.ToString());
                tempButton.transform.localPosition = new Vector2(panPrefab[i-1].transform.localPosition.x + buttonDefault.sizeDelta.x + panOffset,
                    panPrefab[i].transform.localPosition.y);
                Debug.Log("tempButton " + i + " local x position is " + tempButton.transform.localPosition.x);
                panPrefab[i].transform.localPosition = tempButton.transform.localPosition;
                pansPos[i] = -panPrefab[i].transform.localPosition;
            }
        }
        else if (verticalScroll)
        {
            for (int i = 0; i < panPrefab.Length; i++)
            {
                //panPrefab[i] = Instantiate(panPrefab, transform, false);
                //if (i == 0) continue;
                newPanPrefab[i].transform.localPosition = new Vector2(panPrefab[i].transform.localPosition.y + panPrefab[i].GetComponent<RectTransform>().sizeDelta.y + panOffset,
                    panPrefab[i].transform.localPosition.x);
                pansPos[i] = -panPrefab[i].transform.localPosition;
            }
        }
        else
        {

        }
        Debug.Log("WTF is going on " + panPrefab[3].transform.localPosition.x);
    }
    /*
    private void FixedUpdate()
    {
        if (horizontalScroll)
        {
            // Original untouched code
            ////////////////////
            if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
                scrollRect.inertia = false;
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(panPrefab[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(panPrefab[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                panPrefab[i].transform.localScale = pansScale[i];
            }
            float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
            if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
            if (isScrolling || scrollVelocity > 400) return;
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
            /////////////////////

            if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
                scrollRect.inertia = false;
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panPrefab.Length; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(panPrefab[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(panPrefab[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                panPrefab[i].transform.localScale = pansScale[i];
            }
            float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
            if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
            if (isScrolling || scrollVelocity > 400) return;
            contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
        else if (verticalScroll)
        {
            if (contentRect.anchoredPosition.y >= pansPos[0].y && !isScrolling || contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y && !isScrolling)
                scrollRect.inertia = false;
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(newPanPrefab[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(newPanPrefab[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                newPanPrefab[i].transform.localScale = pansScale[i];
            }
            float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
            if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
            if (isScrolling || scrollVelocity > 400) return;
            contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
        else
        {

        }
    }
    */


    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
