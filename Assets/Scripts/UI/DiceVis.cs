using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceVis : MonoBehaviour
{
    public TextMeshProUGUI text;
    bool isRolling = false;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isRolling)
        {
            text.text = ((int)RandomSystem.RandomRange(0,1000)).ToString();
        }
    }

    public IEnumerator RandomRoll(string s)
    {
        if (!isRolling)
        {
            isRolling = true;
        }
        yield return new WaitForSeconds(1.5f);
        isRolling = false;
        text.text = s;
    }
}
