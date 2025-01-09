using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AEntity : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float speed;
    public bool isDead = false;
    private bool canTakeDamage = true;
    public UnityEvent onTakeDamage;
    public virtual void TakeDamage(float damage)
    {
        if (isDead || !canTakeDamage) return;
        
        health = Mathf.Clamp(health -= damage, -10, maxHealth);
        onTakeDamage.Invoke();
        canTakeDamage = false;
        StartCoroutine(damageCooldown());
        if (health <= 0)
        {
            Die();
        }
    }

    public IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
    }

    public virtual void Die()
    {
        if (!isDead)
        {
            Debug.Log("Dead");
            isDead = true;
            Destroy(gameObject);
        }
    }

}
