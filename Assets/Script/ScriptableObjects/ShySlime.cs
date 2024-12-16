using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShySlime", menuName = "ScriptableObjects/Enemies/Slime")]
public class ShySlime : ScriptableObject
{
    public float health = 100f;
    public float damage = 20f;
    public float jumpForce = 1000f;
    public float minDistanceFromPlayer = 7f;
    public Color color;
}
