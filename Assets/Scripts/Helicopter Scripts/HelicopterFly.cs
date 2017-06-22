using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFly : MonoBehaviour {

	private float speed = 3f;
	public bool flyLeft;

	void Start () {
		StartCoroutine (ChangeDirection());
	}
	
	void Update () {
		Fly ();
	}

	private void Fly() {
		Vector3 temp = transform.position;
		Vector3 tempScale = transform.localScale;
		if (flyLeft) {
			temp.x -= speed * Time.deltaTime;
			tempScale.x = -Mathf.Abs (tempScale.x);
		} else {
			temp.x += speed * Time.deltaTime;
			tempScale.x = Mathf.Abs (tempScale.x);
		}
		transform.position = temp;
		transform.localScale = tempScale;
	}

	IEnumerator ChangeDirection() {
		yield return new WaitForSeconds (3f);
		flyLeft = !flyLeft;
		StartCoroutine (ChangeDirection());
	}
} // class
