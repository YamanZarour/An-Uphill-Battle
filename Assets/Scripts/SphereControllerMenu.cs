using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereControllerMenu : MonoBehaviour {
	public bool canDash = true;
	public bool canMove = true;
	public float accelerationRate = 0.6f;
	public float decelerationRate = 0.04f;
	public float jumpHeight = 20;
	public bool canJump = true;
	public Rigidbody rb;
	// Use this for initialization

	void Start () 
	{
		Physics.gravity = new Vector3 (0, -20, 0);
		transform.position = new Vector3 (0, 6, 0);
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Start") {
			SceneManager.LoadScene (1);
		}
	}


	public void Movement(KeyCode[] keycodes){
		if (canDash) {
			if (Input.GetKeyDown (keycodes [0])) {
				StartCoroutine (Dash (keycodes, 0));
			}
			if (Input.GetKeyDown (keycodes [1])) {
				StartCoroutine (Dash (keycodes, 1));
			}
			if (Input.GetKeyDown (keycodes [2])) {
				StartCoroutine (Dash (keycodes, 2));
			}
			if (Input.GetKeyDown (keycodes [3])) {
				StartCoroutine (Dash (keycodes, 3));
			}
		}
		if (Input.GetKey (keycodes[0])) {
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z + accelerationRate);
		}
		if (Input.GetKey (keycodes[1])) {
			rb.velocity = new Vector3 (rb.velocity.x - accelerationRate, rb.velocity.y, rb.velocity.z);
		}
		if (Input.GetKey (keycodes[2])) {
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z - accelerationRate);
		}
		if (Input.GetKey (keycodes[3])) {
			rb.velocity = new Vector3 (rb.velocity.x + accelerationRate, rb.velocity.y, rb.velocity.z);
		}
	}

	public IEnumerator Dash(KeyCode[] input, int direction){ // It is possible to "Super Dash" (9 times normal speed) but the window is very tight (easier with 2 buttons pressed at the same time).
		yield return new WaitForEndOfFrame ();
		canDash = false;
		var i = 0;
		while (i < 30) {
			if (Input.GetKeyDown (input [direction])) {
				accelerationRate *= 3;
				break;
			}
			yield return new WaitForFixedUpdate ();
			i++;
		}
		yield return new WaitForSeconds (0.5f);
		accelerationRate = 0.8f;
		yield return new WaitForSeconds (0.4f);
		canDash = true;
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Ground") {
			canJump = true;
			canMove = true;
			jumpHeight = 20;
			decelerationRate = 0.06f;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if (canMove) {
			KeyCode[][] movement = new KeyCode[][] {
				new KeyCode[4] {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D},
				new KeyCode[4] {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W},
				new KeyCode[4] {KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A},
				new KeyCode[4] {KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S}
			};
			Movement (movement [CameraController.cameraMode-1]);
		}
		if (Input.GetKeyDown (KeyCode.Space) && canJump) 
		{
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y + jumpHeight, rb.velocity.z);
			canJump = false;
		}
		rb.velocity = new Vector3 (rb.velocity.x * (1 - decelerationRate), rb.velocity.y, rb.velocity.z * (1 - decelerationRate));
	}
}
