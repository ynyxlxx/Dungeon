using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour {
    public Transform playerPrefab;

    private void Start () {
        Instantiate (playerPrefab, transform.position, Quaternion.identity);
    }
}