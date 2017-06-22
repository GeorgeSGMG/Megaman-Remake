using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private GameObject player;
	private GameObject ammo;
	private PlayerScore playerScore;

	private float minX = 0f, maxX = 170f;

	void Awake () {
		player = GameObject.Find ("Player");
		ammo = GameObject.Find ("Ammo");
		playerScore = player.GetComponent<PlayerScore> ();
	}
	
	void Update () {
		FollowPlayer ();
	}

	private void FollowPlayer() {
		if (playerScore.isAlive) {
			Vector3 temp = transform.position;
			temp.x = player.transform.position.x;
			if (temp.x < minX) {
				temp.x = minX;
			}
			if (temp.x > maxX) {
				temp.x = maxX;
			}
			temp.y = player.transform.position.y + 2f;
			transform.position = temp;
			temp.x = player.transform.position.x;
			temp.y = player.transform.position.y;
			temp.z = ammo.transform.position.z;
			ammo.transform.position = temp;
		}
	}
} // class
