using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    public float damage = 1f;

    private void OnCollisionEnter2D (Collision2D other) {

        if (other.collider.tag == "Player") {
            other.gameObject.GetComponent<Player> ().TakeHit (damage, other.transform.position, transform.up);
            ParticleSystem particle = Instantiate (other.collider.GetComponent<Player> ().playerHitPatricle, other.transform.position, Quaternion.FromToRotation (Vector3.forward, transform.up));
            Destroy (particle.gameObject, 2f);
        }
    }
}