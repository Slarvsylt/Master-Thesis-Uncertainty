using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceVis : MonoBehaviour
{
    public TextMeshProUGUI text;
    bool isRolling = false;
    private float fontSize;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        fontSize = text.fontSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isRolling)
        {
            text.text = ((int)RandomSystem.RandomRange(-10000,10000)).ToString();
            text.fontSize = RandomSystem.RandomRange(40,65);
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
        text.fontSize = fontSize;
    }
}
