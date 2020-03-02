using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    public float damage = 1f;

    private void OnCollisionEnter2D (Collision2D other) {

        if (other.collider.tag == "Player") {
            other.gameObject.GetComponent<Player> ().TakeHit (damage, other.transform.position, transform.up);
        }
    }
}