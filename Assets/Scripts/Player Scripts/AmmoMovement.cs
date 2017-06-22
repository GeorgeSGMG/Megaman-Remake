using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMovement : MonoBehaviour {

	private readonly object _locker = new object ();

	private Transform theTransform;
	public static Transform clone;
	private Rigidbody2D myBody;
	public static float speed = 30f;
	public float lifetime = 1f;
	public static bool isShoting = false;
	private Animator anim;

	void Awake() {
		theTransform = GetComponent<Transform> ();
		myBody = GetComponent<Rigidbody2D> ();
		anim = GameObject.Find("Player").GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		//theTransform.position += transform.forward * speed * Time.deltaTime;

		Shot ();
	}

	private void Shot() {
		lock (_locker) {
			if (Input.GetKey (KeyCode.X)) {
				if (!isShoting) {
					isShoting = true;
					anim.SetBool ("Shooting", true);
					/*float forceX = speed;

					Vector3 temp = theTransform.localScale;
					temp.x = 3f;
					theTransform.localScale = temp;

					myBody.AddForce (new Vector2 (forceX, 0));*/

					float forceX = speed * 15;
					clone = Instantiate (theTransform, theTransform.position, theTransform.rotation) as Transform;
					clone.gameObject.tag = "Clone";

					Vector3 temp = clone.localScale;
					temp.x = 3f;
					clone.localScale = temp;

					clone.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (forceX, 0));
					Invoke ("StopShot", 0.3f);
					Invoke ("Die", lifetime);
				}
			}
		}
	}

	public void StopShot() {
		try {
			anim.SetBool ("Shooting", false);
		} catch {}
	}

	public void Die() {
		try {
			Destroy (clone.gameObject);
			isShoting = false;
		} catch {}
		try {
			var clones = GameObject.FindGameObjectsWithTag ("Clone");
			foreach (var cl in clones) {
				Destroy(cl);
			}
		} catch {}
	}
}
