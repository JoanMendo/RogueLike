using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaMovement : AEnemy
{
    public float explosionDistance;
    public GameObject explosion;
    private GameObject player;
    private float inputX;
    private float inputY;
    private Vector2 direction;
    private Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < explosionDistance)
        {
            Explosion();

        }
        else if (Vector2.Distance(transform.position, player.transform.position) < 11f)
        {
            direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
           
        }
        else
        {
            direction = Vector2.zero;
        }
       
        inputX = direction.x;
        inputY = direction.y;
        
        rb.velocity = direction * speed;

        anim.SetFloat("DirectionX", inputX);
        anim.SetFloat("DirectionY", inputY);

    }

    public void Explosion()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        if (player != null)
        {
            player.GetComponent<AEntity>().TakeDamage(damage);
            Debug.Log(player.GetComponent<AEntity>().health);
        }
        health = 0;
        TakeDamage(0);
    }
}
