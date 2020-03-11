using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : Item {
    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            AudioManager.Instance.Play ("heal");
            other.GetComponent<Player> ().IncrementTheHealth ();
            Debug.Log ("current health: " + other.GetComponent<Player> ().health);
            Destroy (this.gameObject);
        }
    }
}