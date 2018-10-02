using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextAtLine : MonoBehaviour
{

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextImporter theTextBox;

	public bool destroyWhenActivated;

	public bool requirePress;
	private bool waitForPress;
	
	public Button compliment;
	public Button meanComment;
	
	// Use this for initialization
	void Start ()
	{
		theTextBox = FindObjectOfType<TextImporter>();

		Button btn1 = compliment.GetComponent<Button>();

		//btn1.SetActive(false);
		//compliment.gameObject.SetActive(false);
		
		btn1.onClick.AddListener(TaskOnClick);
	}
	
	
	// Update is called once per frame
	void TaskOnClick () {
		//if (waitForPress && Input.GetKeyDown())
		//{
			theTextBox.ReloadScript(theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox();
		//}

		if (requirePress)
		{
			waitForPress = true;
			return;
		}
	}



}
