using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public Image weaponSlot;

    private void Start () {
        weaponSlot.sprite = PlayerStats.Instance.GetCurrentWeaponA ().GetComponent<WeaponInfo> ().weaponSprite;
    }

}