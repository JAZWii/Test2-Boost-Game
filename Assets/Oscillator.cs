using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To prevent multiple scripts 
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

	[SerializeField] Vector3 movementVector;

	[Range(0,1)][SerializeField]float movementFactor;

	private Vector3 _startPosition;
	// Use this for initialization
	void Start ()
	{
		_startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 offeset = movementVector * movementFactor;
		transform.position = _startPosition + offeset;
	}
}
