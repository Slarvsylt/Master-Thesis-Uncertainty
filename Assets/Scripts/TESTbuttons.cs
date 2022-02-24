using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TESTbuttons : MonoBehaviour, ISelectHandler
{
    public Button testButton;

	void Start()
	{
		Button btn = testButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		SelectedObject.selectedObject = gameObject;
		Debug.Log("You have clicked the button!");
	}

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log(this.gameObject.name + " was selected");
	}
}
