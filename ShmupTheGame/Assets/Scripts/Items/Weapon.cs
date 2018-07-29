using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public AudioSource AudioSrc;
    public float Damage = 10.0f;
    public float atkRate = 0.2f;
    public float nextAtk = 0.0f;
    public float concQuote = 1.0f;
    public float Range = 1000.0f;
    public float spread = 0.0f;
    public bool penetrates = false;
    public bool explodes = false;
    public bool playerHeld = true;
    public Crosshair reticule;
    public GameObject thePlayer;
    public Vector3 vecTovecToTrg;
    public GameObject lookTarget;
    public CameraShake camShake;
    public float camShakeAmt = 0.1f;
    public float camShakeLength = 0.1f;
}
