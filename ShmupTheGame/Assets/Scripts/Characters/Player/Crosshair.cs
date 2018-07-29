using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Crosshair : MonoBehaviour {

	Camera MainCam;
	Vector3 mouse;
	Vector3 mousePos;
	//GameObject Player;

	// Use this for initialization
	void Start () {
		MainCam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		//Player = GameObject.FindGameObjectWithTag ("Player");
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		mouse = Input.mousePosition;
		mousePos = MainCam.ScreenToWorldPoint (mouse);
		transform.position = mousePos;
		Vector3 temp = transform.position;
		temp.z = 0.0f;
		transform.position = temp;
	}
		
}
