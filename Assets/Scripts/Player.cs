using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private int direction;
	private float turnSpeed = .1f;

	private float movementSpeed;
	//public Quaternion rotation = Quaternion.identity;

	public Transform human;

	public Text eyeContact;
	public bool makingEC;

	//public float contactScore;

	public Text scoreDisplay;

	public Image contentBar;
	public float contentScore;

	public RaycastMouse rc;

	private Transform target;
	private Vector3 initialPos;

	private int gameState = 0;

	// Use this for initialization
	void Start ()
	{
		gameState = 0;
		contentScore = 100;
		target = GetComponent<Transform>();
		initialPos = target.localPosition;
		movementSpeed = .2f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		contentBar.fillAmount = contentScore / 100;

		//if (gameState == 1)
		//{


			HeadMovement();

			//scoreDisplay.text = contentScore.ToString();

			if (contentScore >= 100)
			{
				contentScore = 100;
			}



			direction = Random.Range(0, 200);

			if (direction == 100)
			{
				turnSpeed = turnSpeed * -1;
			}



			if (rc.doorClicked == true)
			{
				Time.timeScale = 0;
				gameState = 2;
			}
		//}


		if (contentScore <= 0)
		{
			Time.timeScale = 0;
			gameState = 2;
		}
		
	}

	void HeadMovement()
	{
		if (contentScore > 0)
		{
			if (rc.doorClicked == false)
			{
				transform.Rotate(0f, turnSpeed, 0f);

				if (Input.GetKey(KeyCode.A))
				{
					transform.Rotate(0f, -movementSpeed, 0f);
				}

				if (Input.GetKey(KeyCode.D))
				{
					transform.Rotate(0f, movementSpeed, 0f);
				}

				float angle = Quaternion.Angle(transform.rotation, human.rotation);
				//Debug.Log(angle);

				//gives score depending on what angle you're facing
				if (angle >= -10 && angle <= 15)
				{
					eyeContact.text = "";
					contentScore += .1f;
					makingEC = true;
				}
				else if (angle >= 160 && angle <= 220)
				{
					eyeContact.text = "DON'T WATCH ANIME";
					contentScore -= .15f;
					makingEC = false;
				}
				else
				{
					eyeContact.text = "MAKE EYE CONTACT";
					contentScore -= .1f;
					makingEC = false;
				}
			}
		}
	}
	
	
	private float pendingShakeDuration = 0f;

	public void Shake(float duration)
	{
		if (duration > 0)
		{
			pendingShakeDuration += duration;
		}
	}

	private bool isShaking = false;

	/*public IEnumerator DoShake()
	{
		isShaking = true;

		pendingShakeDuration = 0f;
		target.localPosition = initialPos; 

		/isShaking = false;
	}*/

}
