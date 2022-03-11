using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    [SerializeField]
    private Camera uiCamera;
    private TextMeshProUGUI tooltipText;
    private RectTransform backgroundRectTrans;

    private void Awake()
    {
        instance = this;
        backgroundRectTrans = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        showTooltip("TEsting Tooltip");

    }

    private void Update()
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPosition);
        transform.localPosition = localPosition;
    }

    private void showTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        float padding = 5f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        backgroundRectTrans.sizeDelta = backgroundSize;
    }

    private void hideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string inputText)
    {
        instance.showTooltip(inputText);
    }

    public static void HideTooltip_Static()
    {
        instance.hideTooltip();
    }

}
