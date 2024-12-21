using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Weapons/Proyectile")]
public class ProyectileSO : ScriptableObject
{
    public float Damage;
    public string targetTag;
    public float attackSpeed;
    public Vector3 Scale;
    public float proyectileSpeed;
    public GameObject deathParticle;
    public RuntimeAnimatorController animatorController;
    public Sprite proyectileSprite;
}
