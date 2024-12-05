using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AEnemy : AEntity
{
    public float damage;
    protected Rigidbody2D rb;
    private bool isDead = false;
    private bool canTakeDamage = true;
    public override void TakeDamage(float damage)
    {
        if (isDead || !canTakeDamage) return;
        health -= damage;
        canTakeDamage = false;
        StartCoroutine(Cooldown());
        //CreateFloatingText(damage);
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

    public IEnumerator Cooldown()
    {

        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
    }
}
