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

	public AudioSource music;

	public Text eyeContact;
	public bool makingEC;

	//public float contactScore;

	public TextImporter tI;

	public Text scoreDisplay;

	public Image contentBar;
	public float contentScore;

	public RaycastMouse rc;

	private Transform target;
	private Vector3 initialPos;

	public int gameState = 0;

	public GameObject instructionsScreen;
	public GameObject convoEndScreen;
	public GameObject doorEndScreen;
	public GameObject badEndScreen;

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
		Debug.Log(gameState);
		contentBar.fillAmount = contentScore / 100;
		
		//Resets all of the gameplay elements. 
		if (gameState == 0)
		{
	
			convoEndScreen.gameObject.SetActive(false);
			doorEndScreen.gameObject.SetActive(false);
			badEndScreen.gameObject.SetActive(false);
			Time.timeScale = 1;
			contentScore = 100;
			rc.doorClicked = false;
			instructionsScreen.gameObject.SetActive(true);
			if (Input.GetMouseButtonDown(0))
			{
				gameState = 1; 
				tI.EnableTextBox();
			}
		}

		//GAMESTATE 1. MOST GAMEPLAY
		if (gameState == 1)
		{
			instructionsScreen.gameObject.SetActive(false);

			HeadMovement();

			//scoreDisplay.text = contentScore.ToString();

			//keeps the bar from overfilling
			if (contentScore >= 100)
			{
				contentScore = 100;
			}


			//Changes direction of head movement
			direction = Random.Range(0, 200);
			if (direction == 100)
			{
				turnSpeed = turnSpeed * -1;
			}


			//Leaving the party
			if (rc.doorClicked == true)
			{
				doorEndScreen.gameObject.SetActive(true);
				gameState = 2;
			}
		}


		if (contentScore <= 0)
		{
			badEndScreen.gameObject.SetActive(true);
			gameState = 2;
		}

		//Lose/victory screen
		if (gameState == 2)
		{
			//Time.timeScale = 0;
			//instructionsScreen.gameObject.SetActive(true);
			if (Input.GetKeyDown(KeyCode.R))
			{
				gameState = 0;
			}
		}

		//screen shake
		if (pendingShakeDuration > 0 && !isShaking)
		{
			StartCoroutine(DoShake());
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
					contentScore += .01f;
					makingEC = true;
				}
				else if (angle >= 160 && angle <= 220)
				{
					eyeContact.text = "DON'T WATCH ANIME";
//					music.pitch = 1.5f;
					contentScore -= .15f;
					makingEC = false;
				}
				else
				{
					eyeContact.text = "MAKE EYE CONTACT";
//					music.pitch = 1.25f;
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

	public IEnumerator DoShake()
	{
		isShaking = true;

		var startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < startTime + pendingShakeDuration)
		{
			var randomPoint = new Vector3(Random.Range(initialPos.x - 1, initialPos.x + 1), Random.Range(initialPos.y - 1, initialPos.y + 1), initialPos.z);
			target.localPosition = randomPoint;
			yield return null;
		}

		pendingShakeDuration = 0f;
		target.localPosition = initialPos; 
		isShaking = false;
	}

}
