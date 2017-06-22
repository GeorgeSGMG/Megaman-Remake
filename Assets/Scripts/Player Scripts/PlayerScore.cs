using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {

	public bool isAlive;

	private GameObject gameFinishedText;
	private GameObject stageMusic;
	private GameObject victoryMusic;
	private GameObject deathMusic;
	private GameObject ammo;

	void Awake () {
		isAlive = true;
		gameFinishedText = GameObject.Find ("LevelFinished");
		gameFinishedText.SetActive (false);
		stageMusic = GameObject.Find ("Stage Music");
		victoryMusic = GameObject.Find ("Victory Music");
		victoryMusic.SetActive (false);
		deathMusic = GameObject.Find ("Death Music");
		deathMusic.SetActive (false);
		ammo = GameObject.Find ("Ammo");
	}

	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag == "Enemy") {
			if (isAlive) {
				stageMusic.SetActive (false);
				deathMusic.SetActive (true);
				isAlive = false;
				GameplayController.instance.DecrementLife ();
				transform.position = new Vector3 (0, 100000, 0);
				ammo.gameObject.transform.position = new Vector3 (0, 100000, 0);
			}
		}

		if (target.tag == "Exit") {
			gameFinishedText.SetActive (true);
			stageMusic.SetActive (false);
			victoryMusic.SetActive (true);
			Time.timeScale = 0f;
		}
	}
} // class
