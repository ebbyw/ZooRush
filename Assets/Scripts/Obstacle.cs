﻿using UnityEngine;
using System.Collections;
/** Handles all collsion based interactions with the character and 
 * the object the script is attached to.
 *
 *	@author Ebtissam Wahman
 */ 
public class Obstacle : MonoBehaviour
{
	private bool inFront; // Is the character in front or behind the obstacle?
	public bool canMove; // Cane this object move on it's own or based on physics?
	private Renderer[] sprites; //All sprites that are a child of the object

	void Start ()
	{
		sprites = transform.GetComponentsInChildren<Renderer> ();
	}

	void Update ()
	{
		if (inFront) {
			foreach (Renderer sprite in sprites) {
				if (!sprite.name.Contains ("Ground Shadow") && sprite.sortingLayerName != "Obstacles-Behind") {
					sprite.sortingLayerName = "Obstacles-Behind";
				}
			}
		} else {
			foreach (Renderer sprite in sprites) {
				if (!sprite.name.Contains ("Ground Shadow") && sprite.sortingLayerName != "Obstacles-InFront") {
					sprite.sortingLayerName = "Obstacles-InFront";
				}
			}
		}
		if (canMove) {
			if (transform.position.x > GameObject.FindObjectOfType<PlayerControls> ().transform.position.x + 40f) {
				destroyObstacle ();
			} else {
				if (transform.position.y < -10f || transform.position.y > 10f) {
					destroyObstacle ();
				}
			}
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		inFront = true;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		inFront = false;
	}

	public void collisionDetected ()
	{
		GetComponent<Animator> ().SetTrigger ("Flash");
	}

	private void destroyObstacle ()
	{
		Destroy (gameObject);
	}

}
