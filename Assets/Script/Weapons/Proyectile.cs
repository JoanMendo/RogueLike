using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proyectile : AWeapon
{

    public float Speed;
    public GameObject deathParticle;
    private Rigidbody2D rb;
    public GameObject Shadow;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
        GameObject shadow = Instantiate(Shadow, new Vector3(transform.position.x, transform.position.y -1, transform.position.x), transform.rotation, transform);
        //Make the y bigger if the rotation is either 90 or 270, and smaller if it is 0 or 180. The min is 1, and for each degree between the lower and the max the width gets a bit bigger.
        shadow.transform.localScale = new Vector3(2f, 1f + Mathf.Abs(Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), 1);

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
