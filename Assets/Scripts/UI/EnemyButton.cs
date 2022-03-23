using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class EnemyButton : MonoBehaviour
{
    RectTransform rt;
    public bool isShaking = false;

    [SerializeField]
    private float shake;

    private Vector3 startPos;

    public UIParticleSystem ps;
    ParticleSystem.MainModule settings;

    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        startPos = rt.position;
        settings = ps.pSystem.main;
    }

    
    void LateUpdate()
    {
        if (isShaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shake);
            newPos.z = 0;
            rt.position = startPos + newPos;
        }
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = rt.position;

        if (!isShaking)
        {
            isShaking = true;
        }
        yield return new WaitForSeconds(1.5f);

        isShaking = false;
        rt.position = originalPos;
    }

    public IEnumerator Particles(Color color)
    {
        settings.startColor = color;
        ps.StartParticleEmission();
        yield return new WaitForSeconds(1.5f);
        ps.StopParticleEmission();
    }
}
