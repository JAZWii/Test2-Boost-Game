using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private AudioSource _audioSource;
	
	[SerializeField]float mainThrust = 100f;
	[SerializeField]float rcsThrust = 100f;

	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		Thrust();
		Rotate();
	}

	private void Thrust()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			_rigidbody.AddRelativeForce(Vector3.up * mainThrust);
			if (!_audioSource.isPlaying)
				_audioSource.Play();
		}
		else
		{
			_audioSource.Stop();
		}
	}
	
	private void Rotate()
	{
		_rigidbody.freezeRotation = true;

		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
		
		_rigidbody.freezeRotation = false;

	}

	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				
				break;
			case "Fuel":
				
				break;
			default:
				
				break;
			
		}
	}
}
