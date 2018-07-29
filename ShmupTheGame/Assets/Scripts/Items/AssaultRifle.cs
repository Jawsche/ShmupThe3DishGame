using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AssaultRifle : Weapon
{

    public Projectile bulletPrefab = new Projectile();
    public GameObject emitPoint;
    Rigidbody2D bulletPrefabRB;
    public float ProjectileSpeed = 400.0f;
    public bool playerUsed = false;

    // Use this for initialization
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();



        AudioSrc = GetComponent<AudioSource>();
        camShake = Camera.main.GetComponent<CameraShake>();
        bulletPrefabRB = bulletPrefab.GetComponent<Rigidbody2D>();

        if (transform.root.gameObject == thePlayer)
            playerUsed = true;

        if (playerUsed)
            lookTarget = reticule.gameObject;
        else
            lookTarget = thePlayer;
        
    }

    // Update is called once per frame
    void Update()
    {

        vecTovecToTrg = emitPoint.transform.position - lookTarget.transform.position;
        LookAtMouse();

        if (transform.root.gameObject.GetComponent<Character>().isDead)
            gameObject.SetActive(false);

    }

    void LookAtMouse()
    {
        //Point Gun @ Reticule
        float angToReticule = Vector3.SignedAngle(Vector3.left, vecTovecToTrg, Vector3.back);
        if (angToReticule >= 90.0f || angToReticule <= -90.0f)
        {
            transform.eulerAngles = new Vector3(-180.0f, 0.0f, angToReticule);

        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -angToReticule);

        }
    }

    void makeProjectile(Vector3 source, GameObject gun)
    {
        //Pass projectile the Weapon stats it needs
        bulletPrefab.Damage = Damage;
        bulletPrefab.projSpeed = ProjectileSpeed;
        bulletPrefab.concForce = concQuote;
        bulletPrefab.vecToReticule = vecTovecToTrg;

        Rigidbody2D RB;
        RB = Instantiate(bulletPrefabRB, source, emitPoint.transform.rotation) as Rigidbody2D;
    }

    public void Fire()
    {
        if (Time.time > nextAtk)
        {
            makeProjectile(emitPoint.transform.position, gameObject);
            nextAtk = Time.time + atkRate;
            camShake.Shake(camShakeAmt, camShakeLength);
            AudioSrc.Play();
        }
    }
}
