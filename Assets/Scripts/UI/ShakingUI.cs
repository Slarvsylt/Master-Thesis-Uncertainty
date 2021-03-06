using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ShakingUI : MonoBehaviour
{
    RectTransform rt;
    public bool isShaking = false;

    [SerializeField]
    private float shake = 0.0f; // Needs large numbers

    public Vector3 startPos;
    public Vector3 newPos;

    public RandomMeter rm;

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        startPos = rt.anchoredPosition;
        newPos = startPos;
    }


    void LateUpdate()
    {
        shake = (1f - rm.perc) * 100f;
        if (shake > 5f && !isShaking)
        {
            StartCoroutine(Shake());
        }
        else if (shake < 5f)
        {
            isShaking = false;
        }

        if (isShaking)
        {
            //  rt.position = startPos + newPos;
            rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, startPos + newPos, Time.deltaTime * 5f);
        }
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = rt.anchoredPosition;
        if (!isShaking)
        {
            isShaking = true;
        }

        while (isShaking)
        {
            newPos = Random.insideUnitSphere * (shake);
            newPos.z = 0;
            yield return new WaitForSeconds(0.05f);
        }
        isShaking = false;
        rt.anchoredPosition = originalPos;
    }
}
