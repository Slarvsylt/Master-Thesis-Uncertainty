using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI damageText;

    private void Start()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        //damageText = animator.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        damageText.text = text;
    }
}
