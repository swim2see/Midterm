using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastMouse : MonoBehaviour
{

	public GameObject safetyPanel;
	
	public Text doorText;

	public bool doorClicked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float maxDistance = 1000f;
		
		RaycastHit mouseRayHit = new RaycastHit();
		
		Debug.DrawRay( mouseRay.origin, mouseRay.direction * maxDistance, Color.yellow);
		
		
		if (Physics.Raycast(mouseRay, out mouseRayHit, maxDistance))
		{
			
			// to check the tag (if you wanted to)
			if (mouseRayHit.collider.tag == "Door")
			{
				Debug.Log("the raycast hit object was tagged!");
				safetyPanel.gameObject.SetActive(true);
				doorText.gameObject.SetActive(true);
				if (Input.GetMouseButtonDown(0))
				{
					doorClicked = true;
				}

				if (doorClicked)
				{
					doorText.text = "";
				}
				else
				{
					doorText.text = "LEAVE";
				}

			}
			else
			{
				if (doorClicked)
				{
					doorText.text = "";
				}
				else
				{
					safetyPanel.gameObject.SetActive(false);
					doorText.text = " ";
					doorText.gameObject.SetActive(false);
				}
			}
			
		}
		
	}
}
