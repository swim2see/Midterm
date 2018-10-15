using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoorScript : MonoBehaviour, IPointerClickHandler
{

	public GameObject safetyPanel;

	public Text doorText;

	private bool mouseOverDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/* if (IsMouseOverDoor())
		{
			safetyPanel.gameObject.SetActive(true);
		}
		else
		{
			safetyPanel.gameObject.SetActive(false);
		} */
	}

	private bool IsMouseOverDoor()
	{
		return EventSystem.current.IsPointerOverGameObject();
	}

	public void OnPointerClick(PointerEventData pointerEventData)
	{
		Debug.Log("Left Party");	
	}
}
