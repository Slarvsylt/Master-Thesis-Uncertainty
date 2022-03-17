using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextController : MonoBehaviour
{

    private static PopUpText popUpText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        popUpText = Resources.Load<PopUpText>("PopUpTextParent");
    }

    public static void CreatePopUpText(string text, Transform location)
    {
        PopUpText instance = Instantiate(popUpText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = new Vector3(location.position.x + Random.Range(-0.5f, 0.5f), location.position.y + Random.Range(-0.5f, 0.5f), location.position.z);
        instance.SetText(text);
    }
}
