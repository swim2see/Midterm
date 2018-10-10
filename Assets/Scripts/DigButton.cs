﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigButton : MonoBehaviour
{

	public Button digButton;
	public GameObject digPanel;
	public Vector3 butPosition;
	

	public int digCount;
	
	// Use this for initialization
	void Start () {
		digButton.onClick.AddListener(TaskOnClick);
		digPanel.gameObject.SetActive(false);
		digButton.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (digPanel.activeSelf == true)
			{
				digPanel.gameObject.SetActive(false);
				digButton.gameObject.SetActive(false);
			}
			else
			{
				digPanel.gameObject.SetActive(true);
				digButton.gameObject.SetActive(true);
			}
		}
	}

	// Update is called once per frame
	void TaskOnClick ()
	{
		butPosition = new Vector3(Random.Range(50f,450f),Random.Range(10f,120f),0);
		digButton.gameObject.transform.position = butPosition;
		digCount += 1;
	}
}
