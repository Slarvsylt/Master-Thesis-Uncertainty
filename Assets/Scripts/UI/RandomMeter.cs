using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;

public class RandomMeter : MonoBehaviour, ISelectHandler
{

    private float value;
    public Image front;
    public PostProcessVolume ppv;
    ColorGrading colorGradingLayer = null;
    public Button testButton;
    public float perc = 1.0f;
    private bool fill = false;
    public AudioSource MusicSource;
   // public float ShakeAmount; //Amount the UI will shake

    void Start()
    {
        ppv.profile.TryGetSettings<ColorGrading>(out colorGradingLayer);
        Button btn = testButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (fill)
        {
            front.fillAmount += 0.001f;
        }
        else
        {
            front.fillAmount -= 0.001f;
        }

        if (front.fillAmount <= 0.001f)
        {
            fill = true;
        }
        if(front.fillAmount >= 0.995)
        {
            fill = false;
        }
        */
        if(perc < 0)
        {
            perc = 0;
        }
        if (perc > 1)
        {
            perc = 1;
        }

        front.fillAmount = perc;
        SetPitch(perc);
        if(colorGradingLayer != null)
        {
            colorGradingLayer.hueShift.value = 180f - (180f*perc) ;
        }
    }

    void TaskOnClick()
    {
        front.fillAmount += 0.08f;
        Debug.Log("You have clicked the Sanity!");
    }

    public void SetPitch(float p)
    {
        MusicSource.pitch = 1 + (1-p)*0.1f;
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " was selected");
    }


}
