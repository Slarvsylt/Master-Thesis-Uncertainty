using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButton : MonoBehaviour
{
    RectTransform rt;
    public bool isShaking = false;

    [SerializeField]
    private float shake;
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    
    void LateUpdate()
    {
        if (isShaking)
        {
            Vector3 newPos = Random.insideUnitSphere * (Time.deltaTime * shake);
            newPos.z = rt.position.z;
            rt.position = newPos;
        }
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = rt.position;

        if (!isShaking)
        {
            isShaking = true;
        }

        yield return new WaitForSeconds(1);

        isShaking = false;
        rt.position = originalPos;
    }
}
