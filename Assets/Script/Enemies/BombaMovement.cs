using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaMovement : AEnemy
{
    private GameObject player;
    private float inputX;
    private float inputY;
    public float speed = 5.0f;
    private Rigidbody2D rb;
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
        if (Vector2.Distance(transform.position, player.transform.position) < 8f)
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

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


    }
}
