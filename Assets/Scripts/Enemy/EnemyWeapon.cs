using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    public float angle;
    public float angleIncrement;
    public float rangeOfAngel;
    public float damage = 1f;

    private bool clockwise;

    private void Start () {
        StartCoroutine (WavingTheWeapon ());
    }

    private void Update () {
        transform.localEulerAngles = Vector3.forward * angle;
    }

    private IEnumerator WavingTheWeapon () {
        while (true) {
            if (angle < 0) {
                angle = 0;
                clockwise = !clockwise;
            }
            if (angle > rangeOfAngel) {
                angle = rangeOfAngel;
                clockwise = !clockwise;
            }

            if (clockwise) {
                angle += angleIncrement * Time.deltaTime;
                yield return null;
            } else {
                angle -= angleIncrement * Time.deltaTime;
                yield return null;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<Player> ().TakeHit (damage, other.transform.position, transform.right);
            //Debug.Log (this + " hit the player");
        }
    }
}