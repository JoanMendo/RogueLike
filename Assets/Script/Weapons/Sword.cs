using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AWeapon
{
    public float knockbackForce = 10;
    private Rigidbody2D rb;
    public void Start()
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.position += (Vector3)Direction * 1.5f; //Para que spawnee a una minima distania del origen, en este caso el jugador
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * 2, ForceMode2D.Impulse); 

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
            collision.gameObject.GetComponent<AEnemy>().Knockback(Direction, 10);
        }
    }
}
