using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : Character {

	
    protected bool isRunning = false;
	float input_H;
	float input_V;
    public Vector3 vecToReticule;
    public Crosshair reticule;
    



    // Use this for initialization
    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRend = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
        m_AudioSrc = GetComponent<AudioSource>();
        isFriendly = true;
        reticule = GameObject.FindGameObjectWithTag("Crosshair").GetComponent<Crosshair>();
    }


    // Update is called once per frame
    new void Update () {
        vecToReticule = gameObject.transform.position - reticule.transform.position;
        if (!isDead)
        {
            base.Update();
            if (!isDead)
                Move();
            Animation();
        }
        LookAtMouse();
    }

	void Animation(){
		m_Animator.SetFloat ("HorizSpeed", input_H);
		m_Animator.SetFloat ("VertSpeed", input_V);
        m_Animator.SetBool("IsRunning", isRunning);
        m_Animator.SetBool("isDead", isDead);
    }
    void Move()
    {
        input_H = CrossPlatformInputManager.GetAxis("Horizontal");
        input_V = CrossPlatformInputManager.GetAxis("Vertical");
        if (input_H != 0.0f)
        {
            m_RigidBody.AddForce(Vector2.right * input_H * speed * Time.deltaTime * 500.0f);
            isRunning = true;
        }
        if (input_V != 0.0f)
        {
            m_RigidBody.AddForce(Vector2.up * input_V * speed * Time.deltaTime * 500.0f);
            isRunning = true;
        }
        if (input_V == 0.0f && input_H == 0.0f)
            isRunning = false;
    }
    void LookAtMouse()
    {
        float angToReticule =  Vector3.SignedAngle(Vector3.left, vecToReticule, Vector3.back);
        //Debug.Log(angToReticule);
        if (angToReticule >= 90.0f || angToReticule <= -90.0f)
        {
            //transform.eulerAngles = new Vector3(-180.0f, 0.0f, Vector3.SignedAngle(Vector3.left, vecToReticule, Vector3.back));
            Vector3 temp = gameObject.transform.eulerAngles;
            temp.y = 180;
            gameObject.transform.eulerAngles = temp;
        }
        else
        {
            //transform.eulerAngles = new Vector3(0.0f, 0.0f, Vector3.SignedAngle(Vector3.left, vecToReticule, Vector3.forward));
            Vector3 temp = gameObject.transform.eulerAngles;
            temp.y = 0;
            gameObject.transform.eulerAngles = temp;
        }
    }
}