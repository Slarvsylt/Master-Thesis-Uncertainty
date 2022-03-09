using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Unit currentUnit;

    public Slider sliderHP;

    void Start()
    {
        sliderHP.maxValue = currentUnit.maxHP;
        sliderHP.value = currentUnit.maxHP;
    }

    public void SetHP()
    {
        sliderHP.value = currentUnit.currentHP;
    }

}
