using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public float damage;
    public float flySpeed;
    public float checkDistance;
    public LayerMask checkMask;

    private bool movingRight = true;
    private float angle;

    private void Update () {
        transform.Translate (flySpeed * Time.deltaTime * Vector2.right);

        RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.right, checkDistance, checkMask);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag ("Ground")) {
            TurnAround ();
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<Player> ().TakeHit (damage, other.transform.position, transform.right);
            ParticleSystem particle = Instantiate (other.GetComponent<Player> ().playerHitPatricle, other.transform.position, Quaternion.FromToRotation (Vector3.forward, transform.right));
            Destroy (particle.gameObject, 2f);
        }
    }

    private void TurnAround () {
        if (movingRight == true) {
            transform.eulerAngles = new Vector3 (0, -180, 0);
            movingRight = false;
        } else {
            transform.eulerAngles = new Vector3 (0, 0, 0);
            movingRight = true;
        }
    }

}