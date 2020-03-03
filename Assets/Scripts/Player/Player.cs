using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : LivingEntity {

    private bool isInvincible = false;
    private float invincibleDuration = 1f;
    private float invincibleTimer = 0f;

    public float maxHealth = 10f;
    public float beenHitForce;

    private Rigidbody2D rb;
    private SpriteRenderer playerSprite;
    private PlayerController playerController;

    private void Awake () {
        rb = GetComponent<Rigidbody2D> ();
        playerSprite = GetComponent<SpriteRenderer> ();
        playerController = GetComponent<PlayerController> ();
    }

    protected override void Start () {
        base.Start ();

    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {
        if (isInvincible == false) {
            base.TakeHit (damage, hitPoint, hitDirection);
            this.tag = "Untagged";
            HitAnime (hitDirection);

            isInvincible = true;
            StartCoroutine (ResetTheTimer ());
        }
        Debug.Log (health);
    }

    private void HitAnime (Vector2 hitDirection) {
        rb.AddForce (Vector2.up * beenHitForce, ForceMode2D.Impulse);
        CameraShake.ShakeOnce (0.5f, 0.5f);
        StartCoroutine (ChangeTheColor ());
    }

    private IEnumerator ChangeTheColor () {
        playerSprite.color = new Vector4 (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.6f);
        yield return new WaitForSeconds (1f);
        playerSprite.color = new Vector4 (playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
        this.tag = "Player";
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