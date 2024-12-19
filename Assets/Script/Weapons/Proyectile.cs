using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Proyectile : AWeapon
{
    
    public string TargetTag;
    private GameObject deathParticle;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject Shadow;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        //Se hace esto para que la sombra quede bien posicionada, independientemente de la rotación del proyectil
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
        GameObject shadow = Instantiate(Shadow, new Vector3(transform.position.x, transform.position.y - 1, transform.position.x), transform.rotation, transform);
        shadow.transform.localScale = new Vector3(2f, 1f + Mathf.Abs(Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)), 1);
        SetWeapon((ProyectileSO)weaponSO);
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
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Collider" )
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetWeapon(ProyectileSO proyectile)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = proyectile.proyectileSprite;
        gameObject.transform.localScale = proyectile.Scale;
        animator.runtimeAnimatorController = proyectile.animatorController;
        Damage = proyectile.Damage;
        Speed = proyectile.proyectileSpeed;
        deathParticle = proyectile.deathParticle;
    }
}
