using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AEnemy : AEntity
{
    public float damage;
    protected Rigidbody2D rb;
    private bool isDead = false;


    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public override void Die()
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
