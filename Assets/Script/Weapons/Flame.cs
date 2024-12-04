using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : AWeapon
{
    bool onCooldown = false;


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !onCooldown)
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
            StartCoroutine(Cooldown());
        }
    }

    public IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(0.1f);
        onCooldown = false;
    }
}


