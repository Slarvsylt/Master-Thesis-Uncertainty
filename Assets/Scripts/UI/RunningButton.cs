using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunningButton : MonoBehaviour, ISelectHandler
{
    public RectTransform rt;
    public Vector2 OriginalPos;
    public Vector2 targetPos;
    public Button button;
    public bool Move = false;
    public bool Success = false;
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        OriginalPos = rt.anchoredPosition;
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime*3.0f);
        }
    }

    public void SetNewAnchor(Vector2 na)
    {
        rt.anchorMin = na;
        rt.anchorMax = na;
    }

    void TaskOnClick()
    {
        //rt.anchorMin = OriginalPos;
        //   rt.anchorMax = OriginalPos;
        // Move = false;
        StopRun();
        Success = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
    }

    public void StartRun()
    {
        Success = false;
        StartCoroutine(Run());
    }

    public void StopRun()
    {
        Move = false;
        StopCoroutine(Run());
    }

    public IEnumerator Run()
    {
        Move = true;
        while (Move)
        {
           // Vector3 randomPos = GetBottomLeftCorner(rt) - new Vector3(Random.Range(0, rt.rect.x), Random.Range(0, rt.rect.y), 0);
            Vector2 randomPos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
            targetPos = new Vector2(randomPos.x, randomPos.y);
            //SetNewAnchor(new Vector2(randomPos.x, randomPos.y));
            yield return new WaitForSeconds(0.5f);
        }
        Move = false;
    }

    private Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }
}
