using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour
{
    public float health;
    public float speed;


    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

}
