using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerJump : MonoBehaviour {

	private float speed = 3f;
	public bool jumpLeft;
	private bool isGrounded = false;
	private int numTrigger = 0;
	private float jumpForce = 500f;
	private Rigidbody2D myBody;
	private Animator anim;

	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		StartCoroutine (ChangeDirection());
	}
	
	void Update () {
		Jump ();
	}

	private void Jump() {
		if (isGrounded) {
			if (jumpLeft) {
				myBody.AddForce (new Vector2 (-30, jumpForce));
			} else {
				myBody.AddForce (new Vector2 (30, jumpForce));
			}
		}
	
		/*Vector3 temp = transform.position;
		Vector3 tempScale = transform.localScale;
		if (jumpLeft) {
			temp.x -= speed * Time.deltaTime;
			tempScale.x = -Mathf.Abs (tempScale.x);
		} else {
			temp.x += speed * Time.deltaTime;
			tempScale.x = Mathf.Abs (tempScale.x);
		}
		transform.position = temp;
		transform.localScale = tempScale;*/
	}

	IEnumerator ChangeDirection() {
		yield return new WaitForSeconds (3f);
		jumpLeft = !jumpLeft;
		StartCoroutine (ChangeDirection());
	}

	void OnTriggerEnter2D(Collider2D target) {
		numTrigger++;
		if (target.tag == "Ground") {
			isGrounded = true;
			anim.SetBool ("Ground", true);
		}
	}

	void OnTriggerExit2D(Collider2D target) {
		numTrigger--;
		if (numTrigger == 0) {
			isGrounded = false;
			anim.SetBool ("Ground", false);
		}
	}
} // class
