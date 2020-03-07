using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
    public string weaponName;

    // private void OnCollisionEnter2D (Collision2D other) {
    //     if (other.collider.CompareTag ("Player")) {
    //         print ("player found");
    //         if (Input.GetKeyDown (KeyCode.Q)) {
    //             GameObject weapon = WeaponContainer.weaponDict[weaponName];
    //             PlayerStats.Instance.SetCurrentWeaponA (weapon);
    //             other.collider.GetComponent<PlayerAttack> ().SetCurrentWeapon (weapon);
    //         }
    //     }
    // }

    private void OnTriggerStay2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            print ("player found");
            if (Input.GetKeyDown (KeyCode.Q)) {
                GameObject weapon = WeaponContainer.weaponDict[weaponName];
                PlayerStats.Instance.SetCurrentWeaponA (weapon);
                other.GetComponent<PlayerAttack> ().SetCurrentWeapon (weapon);
            }
        }
    }
}