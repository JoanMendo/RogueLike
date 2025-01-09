using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AEnemy : AEntity
{
    public float damage;
    protected Rigidbody2D rb;
    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public override void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GameManager.instance.coins += Random.Range(1, 3);
            GameManager.instance.enemyList.Remove(gameObject);
            Destroy(gameObject);
            GameManager.instance.checkList();
        }
    }
}
