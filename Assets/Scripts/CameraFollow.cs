﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private GameObject character;
	private NetLauncher netLauncher;
	private SceneManager sceneManager;
	
	public bool cameraSettled;
	private bool adjustToLaunchPosition;
	private bool adjustToStartPosition;

	void Start ()
	{
		character = GameObject.FindGameObjectWithTag ("character");
		netLauncher = GameObject.FindObjectOfType<NetLauncher> ();
		sceneManager = GameObject.FindObjectOfType<SceneManager> ();
		adjustToLaunchPosition = false;
		adjustToStartPosition = false;
		cameraSettled = false;
	}
	
	void Update ()
	{
		if (!sceneManager.levelStartWait) {
			if (netLauncher.launchEnabled && !adjustToLaunchPosition) {
				transform.localPosition = Vector3.Lerp (transform.localPosition, 
				new Vector3 (character.transform.localPosition.x + 3.5f, transform.localPosition.y, transform.localPosition.z), 
				3f * Time.deltaTime);
				cameraSettled = false;
				if (transform.localPosition.x <= character.transform.localPosition.x + 3.4f) {
					adjustToLaunchPosition = true;
					cameraSettled = true;
				}
			} else {
				if (!adjustToStartPosition) {
					transform.localPosition = Vector3.Lerp (transform.localPosition,
					                                        new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z),
					                                        3f * Time.deltaTime);
					cameraSettled = false;
					if (transform.localPosition.x <= character.transform.localPosition.x + 4.9f) {
						adjustToStartPosition = true;
						cameraSettled = true;
					}
				} else {
					transform.localPosition = new Vector3 (character.transform.localPosition.x + 5f, transform.localPosition.y, transform.localPosition.z);
				}
			}
		}
	}
}
