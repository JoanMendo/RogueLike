using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proyectile : AWeapon
{
    
    private string TargetTag;
    private GameObject deathParticle;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject ShadowPrefab;
    private GameObject shadow;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        StartCoroutine(disableAfterTime(5));
        
    }

    public void OnDisable()
    {
        Destroy(shadow);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Direction * Speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TargetTag )
        {
            collision.gameObject.GetComponent<AEntity>().TakeDamage(Damage);
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Collider" )
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
    }

    public override void SetWeapon(ScriptableObject proyectileSO)
    {
        ProyectileSO proyectile = (ProyectileSO)proyectileSO;
        gameObject.GetComponent<SpriteRenderer>().sprite = proyectile.proyectileSprite;
        gameObject.transform.localScale = proyectile.Scale;
        animator.runtimeAnimatorController = proyectile.animatorController;
        Damage = proyectile.Damage;
        TargetTag = proyectile.targetTag;
        AttackSpeed = proyectile.attackSpeed;
        Speed = proyectile.proyectileSpeed;
        deathParticle = proyectile.deathParticle;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
        shadow = Instantiate(ShadowPrefab, new Vector3(transform.position.x, transform.position.y - 1, transform.position.x), transform.rotation, transform);
        shadow.transform.localScale = new Vector3(2f, 1f + Mathf.Abs(Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), 1);
    }

    public IEnumerator disableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
