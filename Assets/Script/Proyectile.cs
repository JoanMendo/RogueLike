using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed;
    public float Damage;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            collision.gameObject.GetComponent<SlimeMovement>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Collider")
        {
            Destroy(gameObject);
        }
    }
}
