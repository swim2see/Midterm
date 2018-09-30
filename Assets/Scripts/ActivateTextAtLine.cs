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
	}
	
	// Update is called once per frame
	void Update () {
		if (waitForPress && Input.GetKeyDown(KeyCode.E))
		{
			theTextBox.ReloadScript(theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox();
		}

		if (requirePress)
		{
			waitForPress = true;
			return;
		}
	}
	
	
}
