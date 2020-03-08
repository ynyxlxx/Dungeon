using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    private float attackTimer;
    public Transform weaponHolder;
    public Transform attackPos;
    public GameObject weaponPrefabs;

    private WeaponInfo playerWeapon;

    public Animator animator;
    public ParticleSystem hitEffect;

    private void Awake () {
        playerWeapon = weaponPrefabs.GetComponent<WeaponInfo> ();
    }

    private void Start () {
        GameObject weaponInstance = Instantiate (weaponPrefabs, weaponHolder.position, Quaternion.FromToRotation (weaponPrefabs.transform.up, weaponHolder.transform.right));
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
                        Vector2 hitDir = enemy.transform.position - attackPos.position;
                        ParticleSystem particle = Instantiate (hitEffect, enemy.transform.position, Quaternion.FromToRotation (Vector3.forward, hitDir));
                        Destroy (particle.gameObject, 2f);
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

    public void SetCurrentWeapon (GameObject newWeapon) {
        for (int i = 0; i < weaponHolder.childCount; i++) {
            Destroy (weaponHolder.GetChild (i).gameObject);
        }

        if (GetComponent<PlayerController> ().isFacingRight) {
            GameObject weaponInstance = Instantiate (newWeapon, weaponHolder.position, Quaternion.FromToRotation (newWeapon.transform.up, weaponHolder.transform.right));
            weaponInstance.transform.parent = weaponHolder;
        } else {
            GameObject weaponInstance = Instantiate (newWeapon, weaponHolder.position, Quaternion.FromToRotation (newWeapon.transform.up, -weaponHolder.transform.right));
            weaponInstance.transform.parent = weaponHolder;
        }

        playerWeapon = newWeapon.GetComponent<WeaponInfo> ();
    }

}