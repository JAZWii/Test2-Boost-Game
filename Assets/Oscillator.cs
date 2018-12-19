using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To prevent multiple scripts 
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{

	[SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
	[SerializeField] float period = 5f;
	
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
		if (period <= Mathf.Epsilon)
			return;
		
		float cycle = Time.time / period;
		const float tau = Mathf.PI * 2;
		float rawSinWave = Mathf.Sin(cycle * tau);

		movementFactor = rawSinWave / 2f + 0.5f; 
		
		Vector3 offeset = movementVector * movementFactor;
		transform.position = _startPosition + offeset;
	}
}
