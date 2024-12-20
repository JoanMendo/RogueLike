using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    public float Damage;
    public Vector2 Direction;
    public float Speed;
    public float AttackSpeed;
    public ScriptableObject weaponSO;

    public abstract void SetWeapon(ScriptableObject scriptable);
}


