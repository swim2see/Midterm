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

	public bool isTyping;
	public float typeSpeed;

	public ActivateTextAtLine textActivator;
	public DigButton digScript;
	
	public Button compliment;
	
	
	// Use this for initialization
	void Start ()
	{
		// Sets the response prompts to false at Start	
		compliment.gameObject.SetActive(false);
	
		
		//meanComment.IsActive();
		//Allows the text to start typing at the beginning
		isTyping = true;
		

		//When all letters of a text line have been written, move onto the next text line.
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
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//When there is currently no typing
		if (!isActive)
		{
			//activate response buttons
			compliment.gameObject.SetActive(true);
			return;
		}
		else
		{
			compliment.gameObject.SetActive(false);
		}


		//will move to the next line when the human ends a sentence.
		//When human finihses typing a line, move onto the next line in the text file
			if (!isTyping)
			{
				currentLine += 1;
				//checks if the text file has been expended
				if (currentLine >= endAtLine)
				{
					DisableTextBox();
				}
				else
				{
					// Figure out how to stop the couroutine, then replace it with  a new interruption coroutine that imports a new array of text 
					// TextScroll should theoretically function fine with current line. Just change the textLines array into a different array. 
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
	}

	public void EnableTextBox()
	{
		//sets the text box to active
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
