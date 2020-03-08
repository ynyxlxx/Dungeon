using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private static PlayerStats _playerStats;
    public static PlayerStats Instance {
        get {
            return _playerStats;
        }
    }

    private GameObject weaponPrefabA;
    private GameObject weaponPrefabB;

    private float money;

    private void Awake () {
        _playerStats = this;
        money = 0;
    }

    private void Start () {
        weaponInitialize ();
        DontDestroyOnLoad (this);
    }

    private void weaponInitialize () {
        if (weaponPrefabA == null) {
            weaponPrefabA = WeaponContainer.weaponDict["Sword"];
            //print (weaponPrefabA);
        }
    }

    public GameObject GetCurrentWeaponA () {
        print (weaponPrefabA);
        return weaponPrefabA;
    }

    public void SetCurrentWeaponA (GameObject newWeapon) {
        weaponPrefabA = newWeapon;
    }

    public GameObject GetCurrentWeaponB () {
        return weaponPrefabB;
    }

    public void SetCurrentWeaponB (GameObject newWeapon) {
        weaponPrefabB = newWeapon;
    }

    public void SetMoney (float _money) {
        money = _money;
    }

    public float GetMoney () {
        return money;
    }
}