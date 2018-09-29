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

	public float contactScore;

	public Text scoreDisplay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		HeadMovement();

		scoreDisplay.text = contactScore.ToString();

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
		Debug.Log(angle);
		
		if(angle >= -10 && angle <= 15)
		{
			contactScore += .1f;
		}
		else
		{
			contactScore -= .1f;
		}

	}
}
