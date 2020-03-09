using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
    public string weaponName;
    public float price;

    private bool notEnoughGold = false;

    private void OnTriggerStay2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            if (notEnoughGold == false) {
                TextDisplayManager.Instance.DisplayTheText (hint);
            }

            if (Input.GetKeyDown (KeyCode.E)) {
                float currentGold = PlayerStats.Instance.GetMoney ();
                if (currentGold >= price) {
                    PlayerStats.Instance.SetMoney (currentGold - price);
                    GameObject weapon = WeaponContainer.weaponDict[weaponName];
                    PlayerStats.Instance.SetCurrentWeaponA (weapon);
                    other.GetComponent<PlayerAttack> ().SetCurrentWeapon (weapon);
                } else {
                    TextDisplayManager.Instance.DisplayTheText ("Not enough Gold");
                    notEnoughGold = true;
                }
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            TextDisplayManager.Instance.HideTheText ();
            notEnoughGold = false;
        }
    }
}