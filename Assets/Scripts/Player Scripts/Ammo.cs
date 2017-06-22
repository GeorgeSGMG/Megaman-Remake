using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

	public float lifetime = 1f;

	// Use this for initialization
	void Start () {
		//Invoke ("Die", lifetime);
	}
	
	void OnTriggerEnter2D(Collider2D target) {
		if (target.tag != "Enemy") {
			return;
		}

		Destroy (target.gameObject);
	}

	public void Die() {
		Destroy (gameObject);
	}
}
