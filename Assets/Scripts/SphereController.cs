using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
	public GameObject level5Collectible1;
	public GameObject level5Collectible2;
	public int level5CollectibleProgress;
	public GameObject level4Collectible1;
	public GameObject level4Collectible2;
	public GameObject level4Collectible3;
	public bool onIce = false;
	public GameObject IceSphere;
	public GameObject IceSphere2;
	public int level3CollectibleProgress;
	public GameObject level3Collectible1;
	public GameObject level3Collectible2;
	public GameObject level3Collectible3;
	public GameObject level3Collectible4;
	public static int currentLevel;
	public bool canMove = true;
	public GameObject GoalPad1;
	public GameObject GoalPad2;
	public GameObject GoalPad3;
	public GameObject GoalPad4;
	public GameObject GoalPad5;
	public static int amountCollectedTotal;
	public static int[] amountCollected = new int[5]{0,0,0,0,0};
	public float accelerationRate = 0.6f;
	public float decelerationRate = 0.04f;
	public float jumpHeight = 20;
	public bool canJump = true;
	public Rigidbody rb;
	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3 (0, 6, 0);
	}


	void OnCollisionExit (Collision coll)
	{
		if (coll.gameObject.tag == "Ice") {
			if (transform.position.y <= 200 || transform.position.y >= 250) {
				decelerationRate = 0.06f;
			}
			accelerationRate = 0.8f;
			onIce = false;
		}
		if (coll.gameObject.tag == "IceLadder") {
			accelerationRate = 0.8f;
		}
			
	}
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject == IceSphere) {
			StartCoroutine (IceSphereFall (IceSphere));
		}
		if (coll.gameObject == IceSphere2) {
			StartCoroutine (IceSphereFall (IceSphere2));
		}
		if (coll.gameObject.tag == "Ground") {
			canJump = true;
			canMove = true;
			jumpHeight = 20;
			decelerationRate = 0.06f;
		}
		if (coll.gameObject.tag == "Ice") {
			canJump = true;
			canMove = true;
			jumpHeight = 20;
			decelerationRate = 0.005f;
			accelerationRate = 0.2f;
			onIce = true;
		}
		if (coll.gameObject.tag == "IceSlide") {
			canJump = false;
			decelerationRate = 0.005f;
			accelerationRate = 0.08f;
			onIce = true;
		}
		if (coll.gameObject.tag == "IceLadder") {
			canJump = false;
			decelerationRate = 0;
			accelerationRate = 5;
			onIce = true;
		}
		if (coll.gameObject.tag == "Water") {
			canJump = true;
			canMove = true;
			decelerationRate = 0.08f;
			jumpHeight = 30;
		}
		if (coll.gameObject.tag == "Goal Pad") {
			if (jumpHeight == -300) {
				rb.velocity = new Vector3 (0, 0, 0);
			}
			canJump = true;
			jumpHeight = 300;
			canMove = true;
		}
		if (coll.gameObject.tag == "Reverse Goal Pad") {
			if (jumpHeight == 300) {
				rb.velocity = new Vector3 (0, 0, 0);
			}
			canJump = true;
			jumpHeight = -300;
			canMove = true;
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Collectible") 
		{
			amountCollected [currentLevel - 1]++;
			amountCollectedTotal++;
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "StarCheck") {
			if (currentLevel == 3) {
				level3CollectibleProgress++;
			}
			if (currentLevel == 5) {
				level5CollectibleProgress++;
			}
			Destroy (coll.gameObject);
			if (level5CollectibleProgress == 2) {
				level5Collectible1.SetActive (true);
			}
			if (level3CollectibleProgress == 3) {
				level3Collectible1.SetActive (true);
			}
			if (level3CollectibleProgress == 6) {
				level3Collectible2.SetActive (true);
			}
			if (level3CollectibleProgress == 9) {
				level3Collectible3.SetActive (true);
			}
			if (level3CollectibleProgress == 8) {
				level3Collectible4.SetActive (true);
			}
		}
		if (coll.gameObject.tag == "TurnInvisible") {
			coll.gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "TurnInvisible") {
			coll.gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
	}

	public void Movement(KeyCode[] keycodes){
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

	public IEnumerator Dash(KeyCode Direction){
		var i = 0;
		while (i < 10) {
			if (Input.GetKeyDown (Direction)) {
				break;
			}
			if (Input.GetKeyDown (Direction[1])) {
				break;
			}
			if (Input.GetKeyDown (Direction[2])) {
				break;
			}
			if (Input.GetKeyDown (Direction[3])) {
				break;
			}
			yield return new WaitForEndOfFrame ();
			i++;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.Log (amountCollected [4]);
		if ((transform.position.y >= 200 && transform.position.y <= 250) || (transform.position.y >= 1000 && transform.position.y <= 1050 && transform.position.x >= 0 && transform.position.z <= 0)) {
			decelerationRate = 0.08f;
			Physics.gravity = new Vector3 (0, -5, 0);
		} else {
			if (!onIce) {
				decelerationRate = 0.04f;
			}
			Physics.gravity = new Vector3 (0, -20, 0);
		}
		if (0 <= transform.position.y && transform.position.y <= 200){
			currentLevel = 1;
		}
		if (200 <= transform.position.y && transform.position.y <= 400) {
			currentLevel = 2;
		}
		if (400 <= transform.position.y && transform.position.y <= 700){
			currentLevel = 3;
		}
		if (700 <= transform.position.y && transform.position.y <= 1000){
			currentLevel = 4;
		}
		if (1000 <= transform.position.y && transform.position.y <= 1300){
			currentLevel = 5;
		}
		if (amountCollected[0] >= 4) {
			GoalPad1.SetActive (true);
		}
		if (amountCollected[1] >= 5) {
			GoalPad2.SetActive (true);
		}
		if (amountCollected[2] >= 5) {
			GoalPad3.SetActive (true);
		}
		if (amountCollected[3] >= 6) {
			GoalPad4.SetActive (true);
		}
		if (amountCollected[4] >= 10) {
			GoalPad5.SetActive (true);
		}
		if (canMove) {
			KeyCode[][] movement = new KeyCode[][] {
				new KeyCode[4] {KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D},
				new KeyCode[4] {KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W},
				new KeyCode[4] {KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.A},
				new KeyCode[4] {KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S}
			};
			Movement (movement [CameraController.cameraMode - 1]);
		}
		if (Input.GetKeyDown (KeyCode.Space) && canJump) 
		{
			if (Mathf.Abs (jumpHeight) == 300) {
				canMove = false;
				rb.velocity = new Vector3 (0, 0, 0);
				StartCoroutine(LevelSwitch(jumpHeight));
			} else {
				rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y + jumpHeight, rb.velocity.z);
			}
			canJump = false;
		}
		rb.velocity = new Vector3 (rb.velocity.x * (1 - decelerationRate), rb.velocity.y, rb.velocity.z * (1 - decelerationRate));
	}

	public IEnumerator IceSphereFall(GameObject iceSphere){
		for (var i = 0; i < 160; i++) {
			iceSphere.transform.position = new Vector3 (iceSphere.transform.position.x, iceSphere.transform.position.y - 1, iceSphere.transform.position.z);
			yield return new WaitForEndOfFrame();
		}
		Destroy (iceSphere);
		if (iceSphere == IceSphere) {
			level4Collectible1.SetActive (true);
			level4Collectible2.SetActive (true);
			level4Collectible3.SetActive (true);
			for (var i = 0; i < 40; i++) {
				level4Collectible1.transform.position = new Vector3 (level4Collectible1.transform.position.x - 2, level4Collectible1.transform.position.y, level4Collectible1.transform.position.z);
				level4Collectible2.transform.position = new Vector3 (level4Collectible2.transform.position.x, level4Collectible2.transform.position.y, level4Collectible2.transform.position.z - 2);
				level4Collectible3.transform.position = new Vector3 (level4Collectible3.transform.position.x + 1, level4Collectible3.transform.position.y, level4Collectible3.transform.position.z + 1);
				yield return new WaitForEndOfFrame ();
			}
		}
		if (iceSphere == IceSphere2) {
			level5Collectible2.SetActive (true);
			for (var i = 0; i < 40; i++) {
				level5Collectible2.transform.position = new Vector3 (level5Collectible2.transform.position.x - 2, level5Collectible2.transform.position.y, level5Collectible2.transform.position.z);
				yield return new WaitForEndOfFrame ();
			}
		}
	}

	public IEnumerator LevelSwitch(float jump) {
		var limit = 40;
		if (transform.position.y < 300) {
			limit = 40;
		}
		if (transform.position.y > 300) {
			limit = 60;
		}
		for (var i = 0; i < limit; i++) {
			rb.velocity = new Vector3 (0, 0, 0);
			transform.position = new Vector3 (transform.position.x, transform.position.y + (jump / 300 * 5), transform.position.z);
			yield return new WaitForEndOfFrame();
		}
	}
}
