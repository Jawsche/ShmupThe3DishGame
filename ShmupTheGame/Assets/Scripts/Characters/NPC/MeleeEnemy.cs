using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Character {

    

    public GameObject Player;
    public PlayerControl playerCtrl;
    public float sightRange = 10.0f;
    public float atkRange = 1.0f;
    
    public AudioClip biteSound;

    public Vector3 vecToPlayer;

    public ItemUse m_iUse;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerCtrl = Player.GetComponent<PlayerControl>();

        m_RigidBody = GetComponent<Rigidbody2D>();
        m_SpriteRend = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Animator = GetComponent<Animator>();
        m_AudioSrc = GetComponent<AudioSource>();
        m_iUse = GetComponent<ItemUse>();
    }
	
	// Update is called once per frame
	new void Update () {

        vecToPlayer = gameObject.transform.position - Player.transform.position;
        LookAtPlayer();
        if (!isDead)
        {
            base.Update();

            //SightRange Debug Lines
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + sightRange, transform.position.y, transform.position.z), Color.red);
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + sightRange, transform.position.z), Color.green);

            //If the player is within sight range...
            if (Player != null)
            {
                if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= sightRange && !isFriendly)
                {
                    Pursue();
                }
            }
        }
    }

    //State for pursuing target (Player)
    void Pursue()
    {
        
        if (Vector2.Distance(gameObject.transform.position, Player.transform.position) <= atkRange)
        {
            m_iUse.UseItem();
        }
        else
        {
            Move(Player.transform.position);
        }
    }
   

    //Moves gameobject towards a target location
    void Move(Vector3 targetLocation)
    {
            Vector3 vecToTarget;
            vecToTarget = transform.position - targetLocation;
            vecToTarget.Normalize();

            m_RigidBody.AddForce(Vector2.left * vecToTarget.x * speed * Time.deltaTime * 500.0f);
            m_RigidBody.AddForce(Vector2.down * vecToTarget.y * speed * Time.deltaTime * 500.0f);
            Animation(vecToTarget);
        
    }

    void Animation(Vector3 Input)
    {
        m_Animator.SetFloat("HorizSpeed", Input.x * -1.0f);
        m_Animator.SetFloat("VertSpeed", Input.y * -1.0f);
    }

    void LookAtPlayer()
    {
        float angToPlayer = Vector3.SignedAngle(Vector3.left, vecToPlayer, Vector3.back);
        if (angToPlayer >= 90.0f || angToPlayer <= -90.0f)
        {
            Vector3 temp = gameObject.transform.eulerAngles;
            temp.y = 180;
            gameObject.transform.eulerAngles = temp;
        }
        else
        {
            Vector3 temp = gameObject.transform.eulerAngles;
            temp.y = 0;
            gameObject.transform.eulerAngles = temp;
        }
    }
}