using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AWeapon
{
    private Animator animator;
    private Rigidbody2D rb;
    public void Start()
    {
        animator = GetComponent<Animator>();
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.position += (Vector3)Direction * 1.5f;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * 2, ForceMode2D.Impulse);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
            collision.gameObject.GetComponent<AEnemy>().Knockback(Direction, 5);



        }
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationLength);
    }
}
