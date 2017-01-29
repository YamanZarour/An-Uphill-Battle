using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject SphereObject;
	public static int cameraMode = 1;
	public float cameraDistance = 20;
	public float cameraHeight = 20;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			cameraMode --;
			if (cameraMode == 0)
				cameraMode = 4;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			cameraMode ++;
			if (cameraMode == 5)
				cameraMode = 1;
		}
		if (Input.GetKey(KeyCode.DownArrow) && cameraDistance < 51 && cameraHeight < 51)
		{
			cameraDistance ++;
			cameraHeight ++;
		}
		if (Input.GetKey(KeyCode.UpArrow) && cameraDistance > 10 && cameraHeight > 10)
		{
			cameraDistance --;
			cameraHeight --;
		}
		if (cameraMode == 1) //behind
			transform.position = new Vector3 (SphereObject.transform.position.x, SphereObject.transform.position.y + cameraHeight, SphereObject.transform.position.z - cameraDistance);
		if (cameraMode == 2) //left
			transform.position = new Vector3 (SphereObject.transform.position.x - cameraDistance, SphereObject.transform.position.y + cameraHeight, SphereObject.transform.position.z);
		if (cameraMode == 3) //front
			transform.position = new Vector3 (SphereObject.transform.position.x, SphereObject.transform.position.y + cameraHeight, SphereObject.transform.position.z + cameraDistance);
		if (cameraMode == 4) //right
			transform.position = new Vector3 (SphereObject.transform.position.x + cameraDistance, SphereObject.transform.position.y + cameraHeight, SphereObject.transform.position.z);
		transform.LookAt (SphereObject.transform);
	}
}
