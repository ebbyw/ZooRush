﻿using UnityEngine;
using System.Collections;

public class TutorialMode : MonoBehaviour
{
	private bool tutorialEnabled;
	
	private bool dialogPresent;
	private bool infectionDialog;
	private bool powerUpDialog;
	private bool painBarDialog;
	private bool introDialog;
	private int i = 0;
	SceneManager sceneManager;
	
	private GameObject tutText;
	private GameObject objectScanner;
	
	void Start ()
	{
		sceneManager = GetComponent<SceneManager> ();
		tutorialEnabled = sceneManager.tutEnabled;
		dialogPresent = false;
		infectionDialog = false;
		powerUpDialog = false;
		painBarDialog = false;
		introDialog = false;
		tutText = GameObject.Find ("Tutorial Text");
		objectScanner = GameObject.Find ("Tutorial - Object Scanner");
		tutText.GetComponent<GUIText> ().text = "";
		tutText.GetComponentInChildren<GUITexture> ().enabled = false;
	}
	
	void Update ()
	{
		if (tutorialEnabled) {
			RaycastHit2D detected = Physics2D.Raycast (objectScanner.transform.position, Vector2.up);
			if (detected.collider != null) {
				Debug.Log (detected.collider.name);
			}
			if (!dialogPresent) {
				//First check for intro dialog
				//Second check for pain dialog
				//Then check for the presence of any other objects
				
//				if (!introDialog) {
//					sceneManager.isPlaying = false;
//					introSequence ();
//				}
				if (detected.collider != null) {
					if (detected.collider.name.Contains ("Infection")) {
						Vector3 objectScreenPoint = camera.WorldToScreenPoint (detected.transform.position);
						Vector3 ratio = new Vector3 ((objectScreenPoint.x / Screen.width), (objectScreenPoint.y / Screen.height));
						ratio.x -= 0.1f;
						ratio.y += 0.15f;
						ratio.z = tutText.transform.position.z;
						if (!infectionDialog) {
							tutText.transform.position = ratio;
							tutText.GetComponentInChildren<GUITexture> ().enabled = true;
							sceneManager.isPlaying = false;
							infectionSequence ();
						}
					}
				}

				if (detected.collider != null) {
					
					if (detected.collider.name.Contains ("Power Up")) {
						Vector3 objectScreenPoint = camera.WorldToScreenPoint (detected.transform.position);
						Vector3 ratio = new Vector3 ((objectScreenPoint.x / Screen.width), (objectScreenPoint.y / Screen.height));
						ratio.x -= 0.1f;
						ratio.y += 0.15f;
						ratio.z = tutText.transform.position.z;
						if (!powerUpDialog) {
							tutText.transform.position = ratio;
							tutText.GetComponentInChildren<GUITexture> ().enabled = true;
							sceneManager.isPlaying = false;
							powerUpSequence ();
						}
					}
				}

			}
		}
	}

	private string[] introDialogScript = {
		"This is an infection,","touching one slows you down \nand causes pain","Try your best to avoid them!"
	};

	private string[] infectionDialogScript = {
		"This is an infection,","touching one slows you down \nand causes pain","Try your best to avoid them!"
	};

	private string[] powerUpDialogScript = {
		"This is a power up,","touching one reduces your crisis \nmeter and makes you healthier.","Try your best to collect them!"
	};
	
	private void introSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = introDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= introDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			introDialog = true;
			i = 0;
			dialogPresent = false;
		}

	}

	private void infectionSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = infectionDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= infectionDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			infectionDialog = true;
			i = 0;
			dialogPresent = false;
		}
		
	}

	private void powerUpSequence ()
	{	
		tutText.GetComponent<GUIText> ().text = powerUpDialogScript [i];
		if (InputManager.enter) {
			i++;
		}
		if (i >= powerUpDialogScript.Length) {
			tutText.GetComponent<GUIText> ().text = "";
			tutText.GetComponentInChildren<GUITexture> ().enabled = false;
			dialogPresent = true;
			sceneManager.isPlaying = true;
			powerUpDialog = true;
			i = 0;
			dialogPresent = false;
		}
		
	}
	  
}
