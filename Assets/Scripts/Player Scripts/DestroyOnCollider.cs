using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollider : MonoBehaviour {

	public string tagNotToCollide = string.Empty;

	void OnTriggerEnter2D(Collider2D target) {
		if ((target.tag == tagNotToCollide) || (target.tag == "Clone")) {
			return;
		}

		try {
			Destroy (AmmoMovement.clone.gameObject);
			AmmoMovement.isShoting = false;
		} catch {}
		try {
			var clones = GameObject.FindGameObjectsWithTag ("Clone");
			foreach (var cl in clones) {
				Destroy(cl);
			}
		} catch {}
	}
}
