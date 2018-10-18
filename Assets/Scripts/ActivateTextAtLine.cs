﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextAtLine : MonoBehaviour
{

	public int startLine;
	public int endLine;

	public TextImporter textImporter;
	public Player playerScript;
	public DigButton digScript;

	public bool yeahWorthy;

	public bool destroyWhenActivated;

	public GameObject FloatingTextPrefab;
	public GameObject humanEyes;
	
	public Text conversationGauge;

	public bool requirePress;
	private bool waitForPress;
	public bool canYeah;
	
	[Header("Buttons")]
	public Button yeah;
	public Button nah;
	public Button dogButton;
	public Button movieButton;
	public Button parentButton;

	[Header("Text Assets")] 
	public TextAsset introText;
	public TextAsset positiveResponse;
	public TextAsset negativeResponse;
	public TextAsset dogTalk;
	public TextAsset movieTalk;
	public TextAsset parentTalk;

	//incur a penalty if you've used the same conversation topic twice
	[Header("Used Text")]
	public bool talkedDog;
	public bool talkedMovie;
	public bool talkedParents;
	
	// Use this for initialization
	void Start ()
	{
		textImporter = FindObjectOfType<TextImporter>();
		yeah.gameObject.SetActive(false);
		nah.gameObject.SetActive(false);
		canYeah = false;
		talkedDog = false;
		talkedParents = false;
	}

	private void Update()
	{
		if (playerScript.gameState == 0)
		{
			yeah.gameObject.SetActive(false);
			nah.gameObject.SetActive(false);
			canYeah = false;
			talkedDog = false;
			talkedMovie = false;
			talkedParents = false;
			digScript.digCount = 0;
			textImporter.DisableTextBox();
			textImporter.ReloadScript(introText);
			dogButton.gameObject.SetActive(false);
			movieButton.gameObject.SetActive(false);
			parentButton.gameObject.SetActive(false);
			
			digScript.digButton.gameObject.transform.position = new Vector3((Screen.width/2),(Screen.height/2),100f);
		}


		//Unlocks dialogue buttons
		if (digScript.digCount >= 5)
		{
			dogButton.gameObject.SetActive(true);
		}
		
		if (digScript.digCount >= 15)
		{
			movieButton.gameObject.SetActive(true);
		}
		
		if (digScript.digCount >= 25)
		{
			parentButton.gameObject.SetActive(true);
		}
		
		//When there is currently no typing
		if (textImporter.isActive == false && canYeah == true)
		{
			playerScript.contentScore -= 0.1f;
			conversationGauge.text = "RESPOND!";
			//activate response buttons
			yeah.gameObject.SetActive(true);
			nah.gameObject.SetActive(true);
		}
		else if (textImporter.isActive == false && canYeah == false)
		{
			//Change this conversation thing later. It'll fuck you up.
			conversationGauge.text = "CONVERSATIONAL LULL!";
			playerScript.contentScore -= 0.2f;
			yeah.gameObject.SetActive(false);
			nah.gameObject.SetActive(false);
		}
		else
		{
			conversationGauge.text = "";
			yeah.gameObject.SetActive(false);
			nah.gameObject.SetActive(false);
		}

		if (playerScript.contentScore <= 0)
		{
			conversationGauge.text = "Just go home.";
		}

	}


	// Update is called once per frame
	public void positiveReply () {
		if (yeahWorthy == true)
		{
			playerScript.contentScore += 5;
			textImporter.ReloadScript(positiveResponse);
			textImporter.EnableTextBox();
			canYeah = false;
		}
		else
		{
			playerScript.contentScore -= 15;
			textImporter.ReloadScript(negativeResponse);
			textImporter.EnableTextBox();
			canYeah = false;
		}
	}
	
	public void negativeReply () {
		if (yeahWorthy == false)
		{
			playerScript.contentScore += 5;
			textImporter.ReloadScript(positiveResponse);
			textImporter.EnableTextBox();
			canYeah = false;
		}
		else
		{
			playerScript.contentScore -= 15;
			textImporter.ReloadScript(negativeResponse);
			textImporter.EnableTextBox();
			canYeah = false;
		}
	}


	public void DogTalk()
	{
		//lose points if you interrupt the person speaking
		if (textImporter.isTyping == true)
		{
			playerScript.contentScore -= 10;
		}

		if (talkedDog == true)
		{
			playerScript.contentScore -= 20;
			ShowFloatingText();
		}

		textImporter.DisableTextBox();
		textImporter.ReloadScript(dogTalk);
		textImporter.EnableTextBox();
		yeahWorthy = true;
		canYeah = true;
		talkedDog = true;
	}
	
	public void MovieTalk()
	{
		//lose points if you interrupt the person speaking
		if (textImporter.isTyping == true)
		{
			playerScript.contentScore -= 10;
		}

		if (talkedMovie == true)
		{
			playerScript.contentScore -= 20;
		}

		textImporter.DisableTextBox();
		textImporter.ReloadScript(movieTalk);
		textImporter.EnableTextBox();
		yeahWorthy = false;
		canYeah = true;
		talkedMovie = true;
	}
	
	public void ParentTalk()
	{
		playerScript.contentScore -= 10;
		//lose points if you interrupt the person speaking
		if (textImporter.isTyping == true)
		{
			playerScript.contentScore -= 10;
		}

		if (talkedParents == true)
		{
			playerScript.contentScore -= 20;
		}

		textImporter.DisableTextBox();
		textImporter.ReloadScript(parentTalk);
		textImporter.EnableTextBox();
		canYeah = false;
		talkedParents = true;
	}

	private void ShowFloatingText()
	{
		var go = Instantiate(FloatingTextPrefab, conversationGauge.gameObject.transform.position, Quaternion.identity);
		go.transform.parent = conversationGauge.gameObject.transform;
	}

}
