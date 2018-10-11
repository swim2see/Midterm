using System;
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

	public bool requirePress;
	private bool waitForPress;
	
	public Button compliment;
	public Button meanComment;
	
	public Button topic1;
	
	[Header("Minigames")]
	public TextAsset theText;

	public TextAsset dogTalk;
	
	// Use this for initialization
	void Start ()
	{
		textImporter = FindObjectOfType<TextImporter>();

		Button btn1 = compliment.GetComponent<Button>();

		//btn1.SetActive(false);
		//compliment.gameObject.SetActive(false);
		
		//topic1.gameObject.SetActive(false);
		
		btn1.onClick.AddListener(TaskOnClick);
	}

	private void Update()
	{
		if (digScript.digCount >= 10)
		{
			topic1.gameObject.SetActive(true);
		}
	}


	// Update is called once per frame
	void TaskOnClick () {
		//if (waitForPress && Input.GetKeyDown())
		//{
			textImporter.ReloadScript(theText);
			ResetLines();
		//}

		if (requirePress)
		{
			waitForPress = true;
			return;
		}
	}

	//Resets the text importers values for the new text
	public void ResetLines()
	{
		
		
		//textImporter.currentLine = startLine;
		//textImporter.endAtLine = endLine;
	}

	public void DogTalk()
	{
		//lose points if you interrupt the person speaking
		if (textImporter.isTyping == true)
		{
			playerScript.contentScore -= 10;
		}

		textImporter.DisableTextBox();
		textImporter.ReloadScript(dogTalk);
		textImporter.EnableTextBox();
	}

}
