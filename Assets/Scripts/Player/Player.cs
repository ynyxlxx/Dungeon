using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {

    private bool isInvincible = false;
    private float invincibleDuration = 1f;
    private float invincibleTimer = 0f;

    public float beenHitForce;

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;

    private void Awake () {
        rb = GetComponent<Rigidbody2D> ();
        playerSprite = GetComponent<SpriteRenderer> ();
    }

    protected override void Start () {
        base.Start ();

    }

    private void Update () {

    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        if (isInvincible == false) {
            base.TakeHit (damage, hitPoint, hitDirection);
            HitAnime (hitDirection);

            isInvincible = true;
            StartCoroutine (ResetTheTimer ());
        }
        Debug.Log (health);

    }

    private void HitAnime (Vector2 hitDirection) {
        rb.AddForce (hitDirection * beenHitForce, ForceMode2D.Impulse);
    }

    private IEnumerator ResetTheTimer () {
        while (true) {
            if (invincibleTimer < invincibleDuration && isInvincible == true) {
                invincibleTimer += Time.deltaTime;
            } else {
                isInvincible = false;
                invincibleTimer = 0f;
                break;
            }
            yield return null;
        }
        yield return null;
    }
}