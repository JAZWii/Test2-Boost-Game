using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private AudioSource _audioSource;
	
	
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessInput();
	}

	private void ProcessInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			_rigidbody.AddRelativeForce(Vector3.up);
			if(!_audioSource.isPlaying)
				_audioSource.Play();
		}
		else
		{
			_audioSource.Stop();
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(-Vector3.forward);
		}
	}
}
