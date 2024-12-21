using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "ScriptableObjects/Weapons/Sword")]
public class SwordSO : ScriptableObject
{
    public float Damage;
    public float attackSpeed;
    public Vector3 Scale;
    public float Speed;
    public float knockbackForce;
    public RuntimeAnimatorController animatorController;
    public Sprite swordSprite;
}