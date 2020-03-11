using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item {

    public float coinValue;

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            AudioManager.Instance.Play ("coin");
            float currentMoney = PlayerStats.Instance.GetMoney ();
            PlayerStats.Instance.SetMoney (coinValue + currentMoney);
            Debug.Log ("current money: " + PlayerStats.Instance.GetMoney ());
            Destroy (this.gameObject);
        }
    }
}