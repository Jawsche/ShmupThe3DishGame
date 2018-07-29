using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Teleport : MonoBehaviour {

    public float TPRate = 1.0f;
    GameObject reticule;
    bool isPlayer = false;
    float nextUse;

	// Use this for initialization
	void Start () {
		reticule = GameObject.FindGameObjectWithTag("Crosshair");

        if (gameObject.tag == "Player")
            isPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
            if (CrossPlatformInputManager.GetButtonDown("Fire3"))
                if (Time.time > nextUse)
                {
                    nextUse = Time.time + TPRate;
                    DoTeleport(reticule.transform.position);
                }
    }

    public void DoTeleport(Vector3 target)
    {
        gameObject.transform.position = target;
    }
}
