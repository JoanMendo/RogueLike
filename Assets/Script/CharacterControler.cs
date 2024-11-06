using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    private float inputX;
    private float inputY;
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator anim;
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        direction = new Vector2(inputX, inputY);
        rb.velocity = direction.normalized * speed;


    }
}
