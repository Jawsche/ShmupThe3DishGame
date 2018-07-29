using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float Damage;
    public float projSpeed;
    public float concForce;
    public Vector3 vecToReticule;
    public GameObject hitEffect;


    Rigidbody2D m_rigidBody;
    

    private void Start()
    {
        m_rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_rigidBody.AddForce(vecToReticule.normalized * projSpeed * Time.deltaTime * -50.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Character>().isDead==false)
                collision.gameObject.GetComponent<Character>().TakeDamage(Damage);

        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(vecToReticule.normalized * concForce * -500.0f);

        Instantiate(hitEffect, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
