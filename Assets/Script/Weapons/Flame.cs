using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : AWeapon
{
    public float Speed;
    public float lifeTime = 2f;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
       lifeTime *= Random.Range(0.8f, 1.2f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * 2, ForceMode2D.Impulse);


    }

    void Update()
    {
        rb.velocity = Direction * Speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
        }   
    }
}
