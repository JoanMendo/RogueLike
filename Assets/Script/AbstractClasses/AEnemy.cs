using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : AEntity
{
    public float damage;
    protected Rigidbody2D rb;
    public override void TakeDamage(float damage)
    {
        health -= damage;
        CreateFloatingText(damage);
        if (health <= 0)
        {
            GameManager.instance.enemyList.Remove(gameObject);
            GameManager.instance.checkList();
            Destroy(gameObject);
        }
    }

    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
