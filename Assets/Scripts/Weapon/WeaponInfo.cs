using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent (typeof (SpriteRenderer))]
public class WeaponInfo : MonoBehaviour {
    public string weaponName;
    public float timeBetweenAttack;
    public LayerMask attackMask = 1 << 10; //player Layer
    public float attackRange;
    public int damage;
    public Sprite weaponSprite;

    // public WeaponInfo (string _weaponName, float _timeBetweenAttack, float _attackRange, int _damage) {
    //     weaponName = _weaponName;
    //     timeBetweenAttack = _timeBetweenAttack;
    //     attackRange = _attackRange;
    //     damage = _damage;
    // }

    private void Start () {
        GetComponent<SpriteRenderer> ().sprite = weaponSprite;
    }
}