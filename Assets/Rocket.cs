using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime.DynamicDispatching;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private AudioSource _audioSource;
	private State _state = State.Alive;

	[SerializeField]float mainThrust = 100f;
	[SerializeField]float rcsThrust = 100f;
	[SerializeField]float levelLoadDelay = 2f;

	
	[SerializeField]AudioClip mainEngine;
	[SerializeField]AudioClip success;
	[SerializeField]AudioClip death;

	[SerializeField]ParticleSystem mainEngineParticle;
	[SerializeField]ParticleSystem successParticle;
	[SerializeField]ParticleSystem deathParticle;

	enum State { Alive, Dying, Transcending}
	
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotateInput();
		}
	}

	private void RespondToThrustInput()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
		}
		else
		{
			_audioSource.Stop();
			mainEngineParticle.Stop();
		}
	}

	private void ApplyThrust()
	{
		_rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
		if (!_audioSource.isPlaying)
			_audioSource.PlayOneShot(mainEngine);
		mainEngineParticle.Play();
	}

	private void RespondToRotateInput()
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
		if(_state != State.Alive)
			return;
		
		switch (collision.gameObject.tag)
		{
			case "Friendly":
				
				break;
			case "Fuel":
				
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}

	private void StartSuccessSequence()
	{
		_state = State.Transcending;
		_audioSource.Stop();
		_audioSource.PlayOneShot(success);
		successParticle.Play();
		LoadScene(SceneManager.GetActiveScene().buildIndex + 1, _state, levelLoadDelay);
	}
	
	private void StartDeathSequence()
	{
		_state = State.Dying;
		_audioSource.Stop();
		_audioSource.PlayOneShot(death);
		deathParticle.Play();
		LoadScene(SceneManager.GetActiveScene().buildIndex, _state, levelLoadDelay);
	}

	void LoadScene(int sceneNumber,State state, float delayTime=1f)
	{
		StartCoroutine(LoadSceneIEnum(sceneNumber,state, delayTime));
	}
	
	IEnumerator LoadSceneIEnum(int sceneNumber,State state, float delayTime=0f)
	{
		yield return new WaitForSeconds(delayTime);
		SceneManager.LoadScene(sceneNumber);
	}	
}
