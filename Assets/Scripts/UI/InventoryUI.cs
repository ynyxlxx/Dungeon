using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public Image weaponSlot;

    private void Start () {
        ChangeTheWeaponSprite ();
        PlayerStats.OnWeaponChanged += ChangeTheWeaponSprite;
    }

    public void ChangeTheWeaponSprite () {
        WeaponInfo currentWeapon = PlayerStats.Instance.GetCurrentWeaponA ().GetComponent<WeaponInfo> ();
        weaponSlot.sprite = currentWeapon.weaponSprite;
    }
}