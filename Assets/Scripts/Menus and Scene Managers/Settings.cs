﻿using UnityEngine;
using System.Collections;

public class Settings : OtherButtonClass
{
	public ToggleButton Music;
	public ToggleButton Sound;
	public RectangleButton ChangeCharacter;
	public CameraFollow cameraFollower;

	public Character character;
	public TextMesh characterSpeech;
	private float sensitivity;
	private int charIndex;
	private int charMaxIndex;
	private string[] characterNames = {"David", "Lisa", "Christina","Zane"};

	void Awake ()
	{
		sensitivity = PlayerPrefs.GetFloat ("Sensitivity", 1);
		PlayerPrefs.SetFloat ("Sensitivity", sensitivity);

		charMaxIndex = 2; // 0 for david, 1 for lisa
		//Set Characters as available in player prefs
		PlayerPrefs.SetInt (characterNames [0], 1); 
		PlayerPrefs.SetInt (characterNames [1], 1);
		PlayerPrefs.SetInt (characterNames [2], 1); 
		PlayerPrefs.SetInt (characterNames [3], 1);
		//Set the limit based on which characters are unlocked
		for (int i = 2; i < characterNames.Length; i++) {
			if (PlayerPrefs.HasKey (characterNames [i])) {
				if (PlayerPrefs.GetInt (characterNames [i], 0) > 0) {
					charMaxIndex++;
				}
			}
		}
		for (int i = 0; i <= charMaxIndex; i++) {
			if (PlayerPrefs.GetInt ("Character Selected", 0) == i) {
				characterSpeech.text = string.Format ("Hi, I'm {0}", characterNames [i]);
				charIndex = i;
				break;
			}
		}
	}

	void Start ()
	{
		GameState.currentState = GameState.States.Intro;
		cameraFollower.cameraFollowEnabled = true;
		cameraFollower.characterOffset = Camera.main.transform.position.x - character.transform.position.x;
		cameraFollower.cameraSettled = true;
		//Simultaneously set the Music and Sound Player Prefs (if they don't exist) while updating toggle buttons
		PlayerPrefs.SetInt ("Music", (Music.setToggleValue (PlayerPrefs.GetInt ("Music", 1) > 0) ? 1 : 0));
		PlayerPrefs.SetInt ("Sound", (Sound.setToggleValue (PlayerPrefs.GetInt ("Sound", 1) > 0) ? 1 : 0));
		GameState.requestPlay ();
	}
	
	public override void otherButtonAction (Button thisButton)
	{
		switch (thisButton.name) {
		case "Sound Toggle Button":
			PlayerPrefs.SetInt ("Sound", (Sound.getToggleValue () ? 1 : 0));
			break;
		case "Music Toggle Button":
			PlayerPrefs.SetInt ("Music", (Music.getToggleValue () ? 1 : 0));
			MenuMusic menumusic = GameObject.FindObjectOfType<MenuMusic> ();
			if (menumusic != null) {
				menumusic.menuMusicUpdate ();
			}
//			if (!Music.getToggleValue ()) {
//				Camera.main.audio.mute = true;
//			} else {
//				Camera.main.audio.mute = false;
//			}
			break;
		case "Change Character":
			charIndex = (charIndex + 1) % charMaxIndex;
			characterSpeech.text = string.Format ("Hi, I'm {0}", characterNames [charIndex]);
			PlayerPrefs.SetInt ("Character Selected", charIndex);
			Debug.Log ("Current Character is " + charIndex);
			character.changeCharacter ();
			break;
		default:
			break;
		}
	}
}
