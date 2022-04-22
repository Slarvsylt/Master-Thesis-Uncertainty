using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RunningButton : MonoBehaviour, ISelectHandler
{
    public RectTransform rt;
    public Vector2 OriginalPos;
    public Vector2 targetPos;
    public Button button;
    public bool Move = false;
    public bool Success = false;
    public TextMeshProUGUI text;
    public AudioClip clip;
    public AudioClip bonk;
    public AudioSource source;
    private Color color;
    public Sprite normal;
    public Sprite damaged;
    
    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<Image>().color;
        rt = gameObject.GetComponent<RectTransform>();
        OriginalPos = rt.anchoredPosition;
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, targetPos, Time.deltaTime*0.5f);
           // rt.anchoredPosition = cubeBezier2(rt.anchoredPosition, new Vector2(0, 0), new Vector2(0, 0), targetPos, Time.deltaTime * 3.5f);
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
        source.Stop();
        source.clip = bonk;
        source.Play();
       // gameObject.GetComponent<Image>().color = color;
        gameObject.GetComponent<Image>().sprite = damaged;
       // Debug.Log(source.clip.name);
        StopRun();
        Success = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
    }

    public IEnumerator StartRun()
    {
        GameObject.Find("dice").GetComponent<TextMeshProUGUI>().text = "Catch Bobo!";
        transform.SetAsLastSibling();
        gameObject.GetComponent<Image>().sprite = normal;
        //gameObject.GetComponent<Image>().color = color;
        Success = false;
        yield return StartCoroutine(Run());
    }

    public void StopRun()
    {
        GameObject.Find("dice").GetComponent<TextMeshProUGUI>().text = "...";
        Move = false;
        StopCoroutine(Run());
        if (source.isPlaying && source.clip.name == "annoying")
        {
            source.Stop();
        }
        //source.Stop();
    }

    public IEnumerator Run()
    {
        Move = true;
        source.clip = clip;
        source.Play();
        while (Move)
        {
           // Vector3 randomPos = GetBottomLeftCorner(rt) - new Vector3(Random.Range(0, rt.rect.x), Random.Range(0, rt.rect.y), 0);
            Vector2 randomPos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
            targetPos = new Vector2(randomPos.x, randomPos.y);
            //SetNewAnchor(new Vector2(randomPos.x, randomPos.y));
            yield return new WaitForSeconds(0.4f);
        }
    }

    private Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    public Vector2 cubeBezier2(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        float r = 1f - t;
        float f0 = r * r * r;
        float f1 = r * r * t * 3;
        float f2 = r * t * t * 3;
        float f3 = t * t * t;
        return new Vector2(
            f0 * p0.x + f1 * p1.x + f2 * p2.x + f3 * p3.x,
            f0 * p0.y + f1 * p1.y + f2 * p2.y + f3 * p3.y
        );
    }
}
