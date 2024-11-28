using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : AEntity
{
    public float damage;
    protected Rigidbody2D rb;
    private bool isDead = false;
    public override void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        CreateFloatingText(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GameManager.instance.enemyList.Remove(gameObject);
            GameManager.instance.checkList();
            Destroy(gameObject);
        }
    }
}
