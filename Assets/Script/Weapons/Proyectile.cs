using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proyectile : AWeapon
{

    public float Speed;
    public GameObject deathParticle;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Direction * Speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" )
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Collider" )
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
