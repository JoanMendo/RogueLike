using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemy : AEntity
{
    public float damage;

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
}
