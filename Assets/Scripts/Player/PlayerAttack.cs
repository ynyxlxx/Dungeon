﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private float attackTimer;
    public Transform weaponHolder;
    public Transform attackPos;
    public GameObject weaponPrefabs;

    private WeaponInfo playerWeapon;

    public Animator animator;

    private void Awake () {
        playerWeapon = weaponPrefabs.GetComponent<WeaponInfo> ();
    }

    private void Start () {
        GameObject weaponInstance = Instantiate (weaponPrefabs, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.parent = weaponHolder;
    }

    private void Update () {
        if (attackTimer <= 0) {
            if (Input.GetMouseButtonDown (0)) {
                animator.SetTrigger ("attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll (attackPos.position, playerWeapon.attackRange);

                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    Enemy enemy = enemiesToDamage[i].GetComponent<Enemy> ();
                    if (enemy != null) {
                        enemy.TakeHit (playerWeapon.damage, enemy.transform.position, this.transform.right);
                        Debug.Log ($"hit the {enemy}, current health {enemy.health}");
                    }
                }
                attackTimer = playerWeapon.timeBetweenAttack;
            }
        } else {
            attackTimer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos () {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (attackPos.position, playerWeapon.attackRange);
    }

}