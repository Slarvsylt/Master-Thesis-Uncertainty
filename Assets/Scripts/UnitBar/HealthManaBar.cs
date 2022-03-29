using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaBar : MonoBehaviour
{
    public Unit currentUnit;

    private float lerpTimer;
    public float chipSpeed = 2f;

    public Image frontHealth;
    public Image backHealth;
    public Image frontMana;
    public Image backMana;

    void Update()
    {
        UpdateUI();

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoseHP(Random.Range(25, 50));
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            RestoreHP(Random.Range(25, 50));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            LoseMP(Random.Range(25, 50));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RestoreMP(Random.Range(25, 50));
        }
        */
    }

    public void UpdateUI()
    {
        float fillFH = frontHealth.fillAmount;
        float fillBH = backHealth.fillAmount;
        float hFraction = currentUnit.currentHP / currentUnit.maxHP;
        float fillFM = frontMana.fillAmount;
        float fillBM = backMana.fillAmount;
        float mFraction = currentUnit.currentMP / currentUnit.maxMP;

        if (fillBH > hFraction)
        {
            frontHealth.fillAmount = hFraction;
            backHealth.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealth.fillAmount = Mathf.Lerp(fillBH, hFraction, percentComplete);
        }

        if (fillFH < hFraction)
        {
            backHealth.color = Color.yellow;
            backHealth.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(fillFH, fillBH, percentComplete);
        }

        if (fillBM > mFraction)
        {
            frontMana.fillAmount = mFraction;
            backMana.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backMana.fillAmount = Mathf.Lerp(fillBM, mFraction, percentComplete);
        }

        if (fillFM < mFraction)
        {
            backMana.color = Color.yellow;
            backMana.fillAmount = mFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontMana.fillAmount = Mathf.Lerp(fillFM, fillBM, percentComplete);
        }
    }
    
    /*
    public void LoseHP(float damage)
    {
        currentUnit.currentHP -= damage;
        lerpTimer = 0f;
    }
    
    public void RestoreHP(float damage)
    {
        currentUnit.currentHP += damage;
        lerpTimer = 0f;
    }

    public void LoseMP(float damage)
    {
        currentUnit.currentMP -= damage;
        lerpTimer = 0f;
    }

    public void RestoreMP(float damage)
    {
        currentUnit.currentMP += damage;
        lerpTimer = 0f;
    }
    */
}
