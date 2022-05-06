using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PerformativeButton : MonoBehaviour, ISelectHandler
{
    public RectTransform rt;
    public Vector2 OriginalPos;
    public Vector2 targetPos;
    public Button button;
    public bool Success = false;
    public TextMeshProUGUI text;
    public AudioClip clip;
    public AudioClip successSound;
    public AudioClip failSound;
    public AudioSource source;
    private Color color;
    private bool run = false;

    private string key;

    public List<string> KeyList;

    // Start is called before the first frame update
    void Start()
    {
        //color = gameObject.GetComponent<Image>().color;
        rt = gameObject.GetComponent<RectTransform>();
        OriginalPos = rt.anchoredPosition;
       // button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), key)))
            {
                Success = true;
                source.clip = successSound;
                StopRun();
            }
        }
    }

    void TaskOnClick()
    {
    }

    public IEnumerator StartRun()
    {
        transform.SetAsLastSibling();
        Success = false;
        source.clip = failSound;
        Debug.Log("GO!");
        yield return StartCoroutine(Run());
    }

    public void StopRun()
    {
        StopCoroutine(Run());
        run = false;
        key = "";
        if (source.isPlaying && source.clip.name == clip.name)
        {
            source.Stop();
        }
        if (Success)
        {
            text.text = "+Atk.";
        }
        else
        {
            text.text = "...";
        }
        //source.Stop();
    }

    public IEnumerator Run()
    {
        //source.clip = clip;
        //source.Play();
        key = KeyList[(int)RandomSystem.RandomRange(0,KeyList.Count)];
        Debug.Log(key);
        text.text = "Press " + key;
        run = true;
        yield return new WaitForSeconds(0.0f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
    }
}
