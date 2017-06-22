using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

	private float speed = 30f, maxVelocity = 4f;

	private Rigidbody2D myBody;
	private Animator anim;

	private bool isGrounded = false;
	private int numTrigger = 0;
	private float jumpForce = 500f;
	[SerializeField]
	private bool isShooting = false;

	void Awake () {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void FixedUpdate () {
		PlayerWalkKeyboard ();
		Jump ();
		Shot ();
	}

	private void PlayerWalkKeyboard() {
		if (!isShooting) {
			float forceX = 0f;
			float vel = Mathf.Abs (myBody.velocity.x);

			float h = Input.GetAxisRaw ("Horizontal");

			if (h > 0) {
				if (vel < maxVelocity) {
					forceX = speed;
					AmmoMovement.speed = Math.Abs (AmmoMovement.speed);
				}

				Vector3 temp = transform.localScale;
				temp.x = 3f;
				transform.localScale = temp;

				anim.SetBool ("Walk", true);
			} else if (h < 0) {
				if (vel < maxVelocity) {
					forceX = -speed;
					AmmoMovement.speed = -Math.Abs (AmmoMovement.speed);
				}

				Vector3 temp = transform.localScale;
				temp.x = -3f;
				transform.localScale = temp;

				anim.SetBool ("Walk", true);
			} else {
				anim.SetBool ("Walk", false);
			}

			myBody.AddForce (new Vector2 (forceX, 0));
		}
	}

	private void Jump() {
		if (Input.GetKey(KeyCode.Space)) {
			if (isGrounded) {
				myBody.AddForce (new Vector2 (0, jumpForce));
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target) {
		if ((target.tag == "Player") || (target.tag == "Clone")) {
			return;
		}
		numTrigger++;
		if (target.tag == "Ground") {
			isGrounded = true;
			anim.SetBool ("Ground", true);
		}
	}

	void OnTriggerExit2D(Collider2D target) {
		if ((target.tag == "Player") || (target.tag == "Clone")) {
			return;
		}
		numTrigger--;
		if (numTrigger == 0) {
			isGrounded = false;
			anim.SetBool ("Ground", false);
		}
	}

	private void Shot() {
		if (Input.GetKey(KeyCode.X)) {
			if (!isShooting) {
				isShooting = true;
				//anim.SetBool ("Shooting", true);

				Invoke ("StopShot", 0.3f);
			}
		}
	}

	public void StopShot() {
		try {
			//anim.SetBool ("Shooting", false);
			isShooting = false;
		} catch {}
	}
} // class
