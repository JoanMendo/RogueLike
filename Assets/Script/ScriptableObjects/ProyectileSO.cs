using System.Collections;
using System.Collections.Generic;

using UnityEditor.Animations;
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
    public AnimatorController animatorController;
    public Sprite proyectileSprite;
}
