using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereController : MonoBehaviour {
	public int time1;
	public int time2;
	public int time3;
	public int time4;
	public int time5;
	public TextMesh finalLevel1;
	public TextMesh timeLevel1;
	public TextMesh finalLevel2;
	public TextMesh timeLevel2;
	public TextMesh finalLevel3;
	public TextMesh timeLevel3;
	public TextMesh finalLevel4;
	public TextMesh timeLevel4;
	public TextMesh finalLevel5;
	public TextMesh timeLevel5;
	public TextMesh finalTotal;
	public TextMesh finalRating;
	public TextMesh timeTotal;
	public TextMesh progressLevel1;
	public TextMesh progressLevel2;
	public TextMesh progressLevel3;
	public TextMesh progressLevel4;
	public TextMesh progressLevel5;
	public GameObject level5Collectible1;
	public GameObject level5Collectible2;
	public int level5CollectibleProgress;
	public GameObject level4Collectible1;
	public GameObject level4Collectible2;
	public GameObject level4Collectible3;
	public bool onIce = false;
	public bool onLadder = false;
	public GameObject IceSphere;
	public GameObject IceSphere2;
	public int level3CollectibleProgress;
	public GameObject level3Collectible1;
	public GameObject level3Collectible2;
	public GameObject level3Collectible3;
	public GameObject level3Collectible4;
	public static int currentLevel;
	public bool canDash = true;
	public bool canMove = true;
	public GameObject GoalPad1;
	public GameObject GoalPad2;
	public GameObject GoalPad3;
	public GameObject GoalPad4;
	public GameObject GoalPad5;
	public static float amountCollectedTotal;
	public static int[] amountCollected = new int[5]{0,0,0,0,0};
	public float accelerationRate = 0.6f;
	public float decelerationRate = 0.04f;
	public float jumpHeight = 20;
	public bool canJump = true;
	public Rigidbody rb;
	// Use this for initialization

	public IEnumerator Timer(){
		while (!(currentLevel == 6)) {
			yield return new WaitForSecondsRealtime (1f);
			switch (currentLevel) {
			case 1:
				time1++;
				break;
			case 2:
				time2++;
				break;
			case 3:
				time3++;
				break;
			case 4:
				time4++;
				break;
			case 5:
				time5++;
				break;
			default:
				break;
			}
		}
	}
	public IEnumerator Updater() {
		timeLevel1.text = "Your time is " + time1.ToString () + ".";
		timeLevel2.text = "Your time is " + time2.ToString () + ".";
		timeLevel3.text = "Your time is " + time3.ToString () + ".";
		timeLevel4.text = "Your time is " + time4.ToString () + ".";
		timeLevel5.text = "Your time is " + time5.ToString () + ".";
		timeTotal.text = "Your time is " + (time1 + time2 + time3 + time4 + time5).ToString () + ".";
		finalLevel1.text = "You got " + amountCollected [0].ToString () + " out of 7 collectibles.";
		finalLevel2.text = "You got " + amountCollected [1].ToString () + " out of 7 collectibles.";
		finalLevel3.text = "You got " + amountCollected [2].ToString () + " out of 7 collectibles.";
		finalLevel4.text = "You got " + amountCollected [3].ToString () + " out of 11 collectibles.";
		finalLevel5.text = "You got " + amountCollected [4].ToString () + " out of 12 collectibles.";
		finalTotal.text = "You got " + (amountCollectedTotal).ToString () + "out of 44.";
		float percentage = amountCollectedTotal / 0.44f;
		finalRating.text = "You got " + (Mathf.Round(percentage)).ToString () + " percent of completion.";
		yield return new WaitForFixedUpdate ();
	}

	void Start () 
	{
		transform.position = new Vector3 (0, 6, 0);
		StartCoroutine (Timer ());
		currentLevel = 1;
		amountCollectedTotal = 0;
		amountCollected = new int[5]{0,0,0,0,0};
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
			canDash = true;
			onLadder = false;
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
		if (coll.gameObject.tag == "IceLadder") {
			canJump = false;
			decelerationRate = 0;
			accelerationRate = 5;
			onIce = true;
			canDash = false;
			onLadder = true;
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
		if (onIce) {
			if (onLadder) {
				accelerationRate = 0.08f;
			} else {
				accelerationRate = 0.2f;
			}
		} else {
			accelerationRate = 0.8f;
		}
		yield return new WaitForSeconds (0.4f);
		canDash = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneManager.LoadScene (0);
		}
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			transform.position = new Vector3 (transform.position.x, 6, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			transform.position = new Vector3 (transform.position.x, 206, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			transform.position = new Vector3 (transform.position.x, 406, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			transform.position = new Vector3 (transform.position.x, 706, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			transform.position = new Vector3 (transform.position.x, 1006, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Alpha6)) {
			transform.position = new Vector3 (transform.position.x, 1506, transform.position.z);
		}

		StartCoroutine(Updater());
		if (amountCollected [0] <= 4) {
			progressLevel1.text ="You have " + (4 - amountCollected [0]).ToString () + " collectibles left.";
		} else {
			progressLevel1.text ="You have 0 collectibles left.";
		}
		if (amountCollected [1] <= 5) {
			progressLevel2.text ="You have " + (5 - amountCollected [1]).ToString () + " collectibles left.";
		} else {
			progressLevel2.text ="You have 0 collectibles left.";
		}
		if (amountCollected [2] <= 5) {
			progressLevel3.text ="You have " + (5 - amountCollected [2]).ToString () + " collectibles left.";
		} else {
			progressLevel3.text ="You have 0 collectibles left.";
		}
		if (amountCollected [3] <= 6) {
			progressLevel4.text ="You have " + (6 - amountCollected [3]).ToString () + " collectibles left.";
		} else {
			progressLevel4.text ="You have 0 collectibles left.";
		}
		if (amountCollected [4] <= 10) {
			progressLevel5.text ="You have " + (10 - amountCollected [4]).ToString () + " collectibles left.";
		} else {
			progressLevel5.text ="You have 0 collectibles left.";
		}
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
		if (1000 <= transform.position.y && transform.position.y <= 1500){
			currentLevel = 5;
		}
		if (1500 <= transform.position.y){
			currentLevel = 6;
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
		if (transform.position.y > 900) {
			limit = 100;
		}
		for (var i = 0; i < limit; i++) {
			rb.velocity = new Vector3 (0, 0, 0);
			transform.position = new Vector3 (transform.position.x, transform.position.y + (jump / 300 * 5), transform.position.z);
			yield return new WaitForEndOfFrame();
		}
		if (transform.position.y > 900) {
			canMove = true;
			canJump = true;
			canDash = true;
			jumpHeight = 20;
		}
	}
}
