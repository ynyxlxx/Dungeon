using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour {
    public float downSpeed;
    public float damage;
    public Transform leftDetection;
    public Transform rightDetection;
    public LayerMask playerCheckMask;
    public LayerMask groundCheckMask;
    public ParticleSystem brokenEffect;

    private bool isTriggered = false;

    private float percent = 0;
    private float interpolation = 0;
    private Vector2 centerPos;
    private RaycastHit2D groundCheck;
    private float timer = 0;

    private void Start () {
        centerPos = new Vector2 (transform.position.x, transform.position.y - 1);
    }

    private void Update () {
        if (isTriggered == false) {
            RaycastHit2D leftCheck = Physics2D.Raycast (leftDetection.position, Vector2.down, playerCheckMask);
            RaycastHit2D rightCheck = Physics2D.Raycast (rightDetection.position, Vector2.down, playerCheckMask);

            if (leftCheck.collider != null || rightCheck.collider != null) {
                if (leftCheck.collider.CompareTag ("Player") || rightCheck.collider.CompareTag ("Player")) {
                    isTriggered = true;
                    groundCheck = Physics2D.Raycast (centerPos, Vector2.down, groundCheckMask);
                }
            }
        }

        if (isTriggered) {
            timer += Time.deltaTime;
            percent += Time.deltaTime * downSpeed;
            interpolation = -Mathf.Pow (percent, 2) * 8f;
            transform.position = Vector2.Lerp (transform.position, groundCheck.point + Vector2.down, percent);
        }

        if (timer >= 2f) {
            Destroy (this.gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag ("Player")) {
            other.GetComponent<Player> ().TakeHit (damage, other.transform.position, Vector2.down);

            ParticleSystem particle = Instantiate (other.GetComponent<Player> ().playerHitPatricle, other.transform.position, Quaternion.FromToRotation (Vector3.forward, -transform.up));
            Destroy (particle, 2f);

            ParticleSystem broken = Instantiate (brokenEffect, transform.position, Quaternion.identity);
            Destroy (broken, 2f);

            Destroy (this.gameObject);
        }
    }
}