using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetMoveX : MonoBehaviour {

	private float speed = 3f;
	public bool moveLeft;
	private bool isGrounded = false;
	private int numTrigger = 0;
	private float jumpForce = 500f;
	private Rigidbody2D myBody;
	private Animator anim;

	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update () {
		Move ();
	}

	private void Move() {
		Vector3 temp = transform.position;
		Vector3 tempScale = transform.localScale;
		if (moveLeft) {
			temp.x -= speed * Time.deltaTime;
			tempScale.x = -Mathf.Abs (tempScale.x);
		} else {
			temp.x += speed * Time.deltaTime;
			tempScale.x = Mathf.Abs (tempScale.x);
		}
		transform.position = temp;
		transform.localScale = tempScale;
	}

	void ChangeDirection() {
		//yield return new WaitForSeconds (3f);
		moveLeft = !moveLeft;
		//StartCoroutine (ChangeDirection());
	}

	void OnTriggerEnter2D(Collider2D target) {
		numTrigger++;
		if (target.tag == "Ground") {
			isGrounded = true;
			anim.SetBool ("Ground", true);
		}
		ChangeDirection();
	}

	void OnTriggerExit2D(Collider2D target) {
		numTrigger--;
		if (numTrigger == 0) {
			isGrounded = false;
			anim.SetBool ("Ground", false);
		}
	}
} // class
