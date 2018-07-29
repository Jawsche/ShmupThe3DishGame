using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour {

	public float time = 20.0f;
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, time);
	}
}
