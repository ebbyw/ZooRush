﻿using UnityEngine;
using System.Collections;

/** Data and functions specific to Animal objects.
 * 
 * @author Ebtissam Wahman
 */ 
public class Animal : MonoBehaviour
{

	public bool caught; //Indicator for whehter the Animal has been caught by the player
	public Vector2 speed; //Current speed of the animal object
	public Sprite animalIcon; //Icon used in the distance meter
	public AudioClip audioClip; // Animal audio sound clip

	private Animator animator; //Animator for the animal's running sprites
	private AudioSource audioSource; //Audio Source that plays sound clip
	private Rigidbody2D animalPhysics; //The rigid body component that controls our animal's physics

	void Start ()
	{
		if (PlayerPrefs.GetInt ("Sound") != 0) { // sound is enabled, we will add a sound source for the animal
			audioSource = gameObject.AddComponent<AudioSource> (); //adding the sound source
			audioSource.playOnAwake = false; //disable playing on instatiation
			audioSource.clip = audioClip; 
		}

		animator = GetComponent<Animator> (); //assigns the pointer to the animator component
		caught = false; //default value for whether the animal has been caught
		animalPhysics = transform.parent.rigidbody2D; //setting the pointer to our rigid body physics controller
		animalPhysics.velocity = new Vector2 (0f, 0f); //we set the initial velocity to 0
		GameObject.Find ("Animal Icon").GetComponent<SpriteRenderer> ().sprite = animalIcon; //Changes the icon in the distance meter
	}
	
	void Update ()
	{
		animator.SetFloat ("Speed", animalPhysics.velocity.x); //Tells the animator state machine what the current speed value is
		switch (GameStateMachine.currentState) {//Checking what our current game state is
		case (int)GameStateMachine.GameState.Intro:
		case (int)GameStateMachine.GameState.Play://If we are in the intro or play state, then we want our animal to maintain speed and the animal sound to be active
			setSpeed ();
			if (audioSource) { //start audio playback
				if (!audioSource.isPlaying) {
					audioSource.Play ();
				}
			}
			break;
		case (int)GameStateMachine.GameState.Paused://If we're paused, then the animal needs to stop and audio must pause as well
			animalPhysics.velocity = new Vector2 (0f, 0f);
			if (audioSource) {
				if (audioSource.isPlaying) {
					audioSource.Pause ();
				}
			}
			break;
		case (int)GameStateMachine.GameState.PauseToPlay: //going from the pause to play state, we want a slight delay before resuming speed
			StartCoroutine (waitToResume (0.1f));
			break;
		default:
			animalPhysics.velocity = new Vector2 (0f, 0f);
			break;
		}
	}

	/**
	 * Sets the speed to the animal's standard running speed.
	 */ 
	public void setSpeed ()
	{
		animalPhysics.velocity = speed; //assigns the rigidbody component the desired velocity
		if (Random.Range (0, 101) == 37) {//.01% chance per frame of moving the animal up or down
			Vector2 randomY = new Vector2 (0, ((Random.Range (0, 2) == 1) ? -1 : 1) * Random.Range (600, 751));
			animalPhysics.AddForce (randomY);
		}
	}

	/** Resumes the default speed of the animal after a delay.
	* @param time the wait time before resetting the speed of the animal
	*/
	
	private IEnumerator waitToResume (float time)
	{
		yield return new WaitForSeconds (time);
		animalPhysics.velocity = speed;
		GameStateMachine.requestPlay ();
	}
}
