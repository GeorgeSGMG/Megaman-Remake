using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	private Text lifeText;

	private int lifeScore;

	void Awake () {
		MakeInstance ();

		lifeText = GameObject.Find ("LifeText").GetComponent<Text> ();
	}

	void OnEnable() {
		SceneManager.sceneLoaded += LevelFinishedLoading;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= LevelFinishedLoading;
	}

	void LevelFinishedLoading(Scene scene, LoadSceneMode mode) {
		if (scene.name == "Gameplay") {
			if (!GameManager.instance.playerDiedGameRestarted) {
				lifeScore = 2;
			} else {
				lifeScore = GameManager.instance.lifeScore;
			}
			lifeText.text = "x" + lifeScore;
		}
	}

	private void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void DecrementLife() {
		lifeScore--;
		if (lifeScore >= 0) {
			lifeText.text = "x" + lifeScore;
		}
		StartCoroutine (PlayerDied ());
	}

	IEnumerator PlayerDied() {
		yield return new WaitForSeconds (2f);

		if (lifeScore < 0) {
			SceneManager.LoadScene ("MainMenu");
		} else {
			GameManager.instance.playerDiedGameRestarted = true;
			GameManager.instance.lifeScore = lifeScore;
			SceneManager.LoadScene ("Gameplay");
		}
	}
} // class
