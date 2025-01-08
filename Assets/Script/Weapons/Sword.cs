using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : AWeapon
{
    private float knockbackForce;
    private Rigidbody2D rb;
    private Animator animator;
    public void Awake()
    {      
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    public void OnEnable()
    {
        StartCoroutine(disableOnAnimationEnd());
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AEnemy>().TakeDamage(Damage);
            collision.gameObject.GetComponent<AEnemy>().Knockback(Direction, knockbackForce);
        }
    }

    public override void SetWeapon(ScriptableObject swordSO)
    {
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.position += (Vector3)Direction * 1.5f; //Para que spawnee a una minima distania del origen, en este caso el jugador


        SwordSO sword = (SwordSO)swordSO;
        gameObject.GetComponent<SpriteRenderer>().sprite = sword.swordSprite;
        gameObject.transform.localScale = sword.Scale;
        Speed = sword.Speed;
        Damage = sword.Damage;
        AttackSpeed = sword.attackSpeed;
        knockbackForce = sword.knockbackForce;
        animator.runtimeAnimatorController = sword.animatorController;
        rb.AddForce(Direction * Speed, ForceMode2D.Force);

    }

    public IEnumerator disableOnAnimationEnd() 
    {

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
