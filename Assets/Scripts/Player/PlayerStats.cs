using System;
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

    public static event Action OnWeaponChanged;

    public bool _firstInitial = true;

    [SerializeField]
    private float money;

    private void Awake () {
        if (_playerStats != null && _playerStats != this) {
            Destroy (this.gameObject);
        } else {
            _playerStats = this;
        }

        money = 50f;
    }

    private void Start () {
        if (_firstInitial) {
            weaponInitialize ();
        }

        DontDestroyOnLoad (this);
    }

    private void weaponInitialize () {
        if (weaponPrefabA == null) {
            weaponPrefabA = WeaponContainer.weaponDict["Sword"];
            _firstInitial = false;
            //print (weaponPrefabA);
        }
    }

    public GameObject GetCurrentWeaponA () {
        print ("get the current weapon " + weaponPrefabA);
        return weaponPrefabA;
    }

    public void SetCurrentWeaponA (GameObject newWeapon) {
        weaponPrefabA = newWeapon;
        if (OnWeaponChanged != null) {
            OnWeaponChanged ();
        }
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