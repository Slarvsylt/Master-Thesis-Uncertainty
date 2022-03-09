using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    public Unit currentUnit;

    public Slider sliderMP;

    void Start()
    {
        sliderMP.maxValue = currentUnit.maxMP;
        sliderMP.value = currentUnit.maxMP;
    }

    public void SetMP()
    {
        sliderMP.value = currentUnit.currentMP;
    }

}
