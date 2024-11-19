using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : AEnemy
{
    private Transform Player;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float jumpForce = 1000f;
    public float minDistanceFromPlayer = 6f;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SlimeMovementCR());
       
    }

    // Update is called once per frame
    void Update()
    {
        direction = Player.position - transform.position;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    public IEnumerator SlimeMovementCR()
    {
        float randomWait = Random.Range(1.5f, 3f);
        yield return new WaitForSeconds(randomWait);
        if (Vector3.Distance(transform.position, Player.position) < minDistanceFromPlayer)
        {
           
            rb.AddForce(direction.normalized * jumpForce * 2.3f, ForceMode2D.Force);
        }
        else
        {
            float RandomX = Random.Range(-1f, 1f);
            float RandomY = Random.Range(-1f, 1f);
            rb.AddForce(new Vector2(RandomX, RandomY) * jumpForce * 2.3f, ForceMode2D.Force);
        }

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        yield return new WaitForSeconds(0.8f);
        
        yield return SlimeMovementCR();
    }

    
}
