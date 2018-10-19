using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{

	public float duration = 10f;
	
	public Light lt;
	public Light lt2;
	public Light lt3;
	public Light lt4;
	public Light lt5;
	public Light lt6;
	public Light lt7;
	public Light lt8;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update() {
		float phi = Time.time / duration * 2 * Mathf.PI;
		float amplitude = Mathf.Cos(phi) * 10F;
		lt.intensity = amplitude;
		lt2.intensity = amplitude;
		lt3.intensity = amplitude;
		lt4.intensity = amplitude;
		lt5.intensity = amplitude;
		lt6.intensity = amplitude;
		lt7.intensity = amplitude;
		lt8.intensity = amplitude;
	}
}