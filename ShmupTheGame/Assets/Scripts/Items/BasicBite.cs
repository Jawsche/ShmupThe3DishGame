using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBite : Weapon {

    public List<GameObject> targets;
    public bool playerUsed = false;

    // Use this for initialization
    void Start () {
        thePlayer = GameObject.FindGameObjectWithTag("Player"); 
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
        AudioSrc = GetComponent<AudioSource>();
        camShake = Camera.main.GetComponent<CameraShake>();
        if (transform.root.gameObject == thePlayer)
            playerUsed = true;
        if (playerUsed)
            lookTarget = reticule.gameObject;
        else
            lookTarget = thePlayer;
    }
	
	// Update is called once per frame
	void Update () {
        vecTovecToTrg = transform.position - lookTarget.transform.position;
        LookAtMouse();
        if (transform.root.gameObject.GetComponent<Character>().isDead)
            gameObject.SetActive(false);
    }

    public void Fire()
    {

        if (Time.time > nextAtk)
        {
            nextAtk = Time.time + atkRate;
            camShake.Shake(camShakeAmt, camShakeLength);
            AudioSrc.Play();

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets.Count > 0)
                {
                    if (targets[i].tag == "Player" || targets[i].tag == "Enemy")
                    {
                        if (targets[i].GetComponent<Character>().isDead == false)
                            targets[i].GetComponent<Character>().TakeDamage(Damage);

                    }
                    if (targets[i].GetComponent<Rigidbody2D>() != null)
                        targets[i].GetComponent<Rigidbody2D>().AddForce(vecTovecToTrg.normalized * concQuote * -500.0f);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            targets.Add(collision.gameObject);
      

        }

        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
        targets.Remove(collision.gameObject);

    }
    void LookAtMouse()//fix to target later
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
 
}
