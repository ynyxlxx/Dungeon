using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponContainer : MonoBehaviour {
    public GameObject[] weaponPrefabs;
    public static Dictionary<string, GameObject> weaponDict = new Dictionary<string, GameObject> ();

    private void Awake () {
        weaponDict.Clear ();
        foreach (GameObject weapon in weaponPrefabs) {
            WeaponInfo info = weapon.GetComponent<WeaponInfo> ();
            weaponDict.Add (info.weaponName, weapon);
        }
    }
}