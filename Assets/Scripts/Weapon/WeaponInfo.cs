using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent (typeof (SpriteRenderer))]
public class WeaponInfo : MonoBehaviour {
    public string weaponName;
    public float timeBetweenAttack;
    public LayerMask attackMask;
    public float attackRange;
    public int damage;
}