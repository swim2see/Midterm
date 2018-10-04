using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TextImporter : MonoBehaviour
{

	public GameObject textBox; 
	
	public TextAsset textFile;

	public Text theText; 

	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public bool isActive = true;

	private bool isTyping;
	public float typeSpeed;

	public ActivateTextAtLine textActivator; 
	
	public Button compliment;
	
	// Use this for initialization
	void Start ()
	{
		//textActivator = GetComponent<ActivateTextAtLine>();
		//compliment.IsActive;
		//meanComment.IsActive();
		isTyping = true;
		
		//Button btn1 = compliment.GetComponent<Button>();

		if (endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}
		
		if (textFile != null)
		{
			// splits the text whenever \n is inputted
			textLines = (textFile.text.Split('\n'));
		}
		//This would cause "index out of range" errors when placed above the if statements above
		if (isActive == true)
		{
			EnableTextBox();
		}
		else
		{
			DisableTextBox();
			textActivator.compliment.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isActive)
		{
			return;
		}

		//theText.text = textLines[currentLine];

		//if (Input.GetKeyDown(KeyCode.E))
		//{
			//will move to the next line when the human ends a sentence.
		//THERE IS AN ISSUE HERE. IS TYPING DEFAULTS TO FALSE
			if (!isTyping)
			{
				currentLine += 1;
				
				if (currentLine >= endAtLine)
				{
					DisableTextBox();
					//btn1.gameObject.IsActive(true);
					//meanComment.IsActive();
				}
				else
				{
					StartCoroutine(TextScroll(textLines[currentLine]));
				}
				
			}

		Debug.Log(isTyping);
	}

	private IEnumerator TextScroll(string lineOfText)
	{
		int letter = 0;
		theText.text = "";
		isTyping = true;
		while (isTyping == true && letter < lineOfText.Length)
		{
			//gets every letter from the imported text
			theText.text += lineOfText[letter];
			letter += 1;
			//adds every letter dependent on type speed. (WaitForSeconds can only be used in a coroutine). typeSpeed is
			//set in the inspector
			yield return new WaitForSeconds(typeSpeed);
		}

		isTyping = false;

		//isTyping = false;
		//theText.text = lineOfText;
	}

	public void EnableTextBox()
	{
		textBox.SetActive(true);
		isActive = true;
		
		StartCoroutine(TextScroll(textLines[currentLine]));
	}

	public void DisableTextBox()
	{
		textBox.SetActive(false);
		isActive = false; 
	}

	public void ReloadScript(TextAsset theText)
	{
		if (theText != null)
		{
			//This resets the array of text back to 1, then it adds the number of text lines in the new text box
			//imagine if your first text box had 9 lines, then the next one had 6. Without this, there would be
			//3 lines of empty dialogue at the end of your new text file.
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));
		}
	}
}
