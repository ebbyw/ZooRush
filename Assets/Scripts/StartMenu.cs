﻿using UnityEngine;
using System.Collections;

/** Controls user interaction with the splash screen.
 *	@author Ebtissam Wahman
 */
public class StartMenu : MonoBehaviour
{
	public DirectToScene playButton;
	
	void Start ()
	{
		Application.targetFrameRate = 60;
		if (PlayerPrefs.GetInt ("Levels Unlocked", 0) > 1) {
			playButton.sceneName = "MultipleCameraTest";
		} else {
			StoryModeHandler.NextSceneName = "Level1-Tutorial";
			playButton.sceneName = "Story";
		}
	}
}
