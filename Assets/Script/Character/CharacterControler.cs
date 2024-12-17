using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : AEntity
{
    private float inputX;
    private float inputY;
    private Rigidbody2D rb;
    private Vector2 direction;
    public GameObject head;
    public GameObject body;
    public Sprite headForward;
    public Sprite headBackward;
    public Sprite headSideways;
    private Controls movementControls;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(inputX, inputY);
        rb.velocity = direction.normalized * speed;

        if (inputX == 1 && inputY==0)
        {
            head.GetComponent<SpriteRenderer>().sprite = headSideways;
            
        }
        else if (inputX == -1&&inputY == 0)
        {
            head.GetComponent<SpriteRenderer>().sprite = headSideways;
            
        }
        else if  ( inputY == 1)
        {
            head.GetComponent<SpriteRenderer>().sprite = headBackward;
            
        }
        else
        {
            head.GetComponent<SpriteRenderer>().sprite = headForward;
            
        }

    }
}
