using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour{

    public float MaxHealth = 100.0f;
    public float Health = 100.0f;
    public float speed = 100.0f;
    public bool isFriendly = false;
    public bool isDead = false;

    public Rigidbody2D m_RigidBody;
    public SpriteRenderer m_SpriteRend;
    public BoxCollider2D m_Collider;
    public Animator m_Animator;
    public AudioSource m_AudioSrc;
    public AudioClip deathSound;

    public GameObject bodyPrefab;

    public void Update()
    {
        if (Health <= 0.0f)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
        m_Animator.SetBool("isDead", isDead);
        m_SpriteRend.sortingOrder = -1;
        m_Collider.enabled = false;
        m_AudioSrc.clip = deathSound;
        m_AudioSrc.Play();
    }
    public void TakeDamage(float dmgValue)
    {
        Health -= dmgValue;
        m_Animator.Play("TakeDamage");
    }
 
}
