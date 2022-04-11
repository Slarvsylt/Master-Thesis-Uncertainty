using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunningButton : MonoBehaviour, ISelectHandler
{
    public RectTransform rt;
    public Vector2 OriginalPos;
    public bool Move = false;
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        OriginalPos = rt.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            rt.position = Vector2.Lerp(rt.anchoredPosition, rt.anchorMin, Time.deltaTime);
        }
    }

    public void SetNewAnchor(Vector2 na)
    {
        rt.anchorMin = na;
        rt.anchorMax = na;
        Move = true;
    }

    void TaskOnClick()
    {
        rt.anchorMin = OriginalPos;
        rt.anchorMax = OriginalPos;
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
    }
}
