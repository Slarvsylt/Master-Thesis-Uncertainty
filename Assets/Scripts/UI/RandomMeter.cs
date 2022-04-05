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
    void Start()
    {
        ppv.profile.TryGetSettings<ColorGrading>(out colorGradingLayer);
        Button btn = testButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        front.fillAmount -= 0.001f;
        perc = front.fillAmount;
        if(colorGradingLayer != null)
        {
            //colorGradingLayer.hueShift.value = front.fillAmount * 180f;
        }
    }

    void TaskOnClick()
    {
        front.fillAmount += 0.01f;
        Debug.Log("You have clicked the Sanity!");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
    }


}
