using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private int direction;
	private float turnSpeed = .8f;
	//public Quaternion rotation = Quaternion.identity;

	public Transform human;

	//public float contactScore;

	public Text scoreDisplay;

	public Image contentBar;
	public float contentScore;

	// Use this for initialization
	void Start ()
	{
		contentScore = 100;
	}
	
	// Update is called once per frame
	void Update ()
	{
		contentBar.fillAmount = contentScore / 100;
		
		HeadMovement();

		//scoreDisplay.text = contentScore.ToString();

		direction = Random.Range(0, 200);

		if (direction == 100)
		{
			turnSpeed = turnSpeed * -1;
		}


	}

	void HeadMovement()
	{
		transform.Rotate(0f, turnSpeed, 0f);

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(0f, -1.2f, 0f);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(0f, 1.2f, 0f);
		}
		
		float angle = Quaternion.Angle(transform.rotation, human.rotation);
		//Debug.Log(angle);
		
		//gives score depending on what angle you're facing
		if(angle >= -10 && angle <= 15)
		{
			contentScore += .1f;
		}
		else
		{
			contentScore -= .1f;
		}

	}
}
