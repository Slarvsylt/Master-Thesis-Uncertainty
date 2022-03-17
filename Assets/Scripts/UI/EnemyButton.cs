using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    RectTransform rt;
    public bool isShaking = false;

    [SerializeField]
    private float shake;

    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        startPos = rt.position;
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
        yield return new WaitForSeconds(0.5f);

        isShaking = false;
        rt.position = originalPos;
    }
}
