using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class RandomMeter : MonoBehaviour
{
    // Start is called before the first frame update
    private float value;
    public Image front;
    public PostProcessVolume ppv;
    ColorGrading colorGradingLayer = null;

    void Start()
    {
        ppv.profile.TryGetSettings<ColorGrading>(out colorGradingLayer);
    }

    // Update is called once per frame
    void Update()
    {
        front.fillAmount = Mathf.Abs(Mathf.Sin(Time.time*0.1f));
        if(colorGradingLayer != null)
        {
            //colorGradingLayer.hueShift.value = front.fillAmount * 180f;
        }
    }
}
