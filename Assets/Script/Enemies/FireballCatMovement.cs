using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCatMovement : AEnemy
{
    public float attackDistance;
    public GameObject Projectile;
    public ProyectileSO proyectileSO;
    public bool onCooldown = false;
    private GameObject player;
    private Vector2 direction;
    private Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        if (Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        {
            anim.SetFloat("DirectionX", direction.x);
            anim.SetFloat("DirectionY", direction.y);

            if (!onCooldown)
            {
                onCooldown = true;
                Attack();
            }
        }
        else 
        { 
            anim.SetFloat("DirectionX", 0);
            anim.SetFloat("DirectionY", 0);
        }
        
    }
    public void Attack()
    {
        Projectile.GetComponent<AWeapon>().weaponSO = proyectileSO;
        GameObject projectile = Instantiate(Projectile, transform.position + (Vector3)direction, Quaternion.identity);
        projectile.GetComponent<Proyectile>().Direction = direction;
        projectile.GetComponent<AWeapon>().SetWeapon(proyectileSO); //Setea el arma
        StartCoroutine(Cooldown(projectile.GetComponent<AWeapon>().AttackSpeed));

    }

    public IEnumerator Cooldown(float attackSpeed)
    {
        yield return new WaitForSeconds(attackSpeed);
        onCooldown = false;
    }


}
