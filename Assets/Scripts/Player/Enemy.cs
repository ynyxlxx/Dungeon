using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity {
    public enum EnemyState { patrolling, attacking, findThePlayer }

    public float patrolSpeed;
    public float groundDetectionDistance;
    public float wallDetectionDistance;
    public float viewRange;
    public float attackSpeed;
    public float attackInterval;

    private bool movingRight = true;

    public Transform groundDetectionPoint;
    public Transform wallDetectionPoint;

    public LayerMask obstacleMask;
    public LayerMask targetMask;

    private EnemyState currentState;

    private Vector3 playerPos;

    private SpriteRenderer enemySprite;
    private float attackTimer;

    public static event System.Action OnDeathStatic;

    private void Awake () {
        enemySprite = GetComponentInChildren<SpriteRenderer> ();
    }

    protected override void Start () {
        base.Start ();

        currentState = EnemyState.patrolling;

        StartCoroutine (Patrolling ());
        StartCoroutine (FindingThePlayer ());

    }

    private IEnumerator Patrolling () {
        while (true) {
            if (currentState == EnemyState.patrolling) {
                transform.Translate (patrolSpeed * Time.deltaTime * Vector2.right);

                RaycastHit2D groundCheck = Physics2D.Raycast (groundDetectionPoint.position, Vector2.down, groundDetectionDistance, obstacleMask);
                RaycastHit2D wallCheck = Physics2D.Raycast (wallDetectionPoint.position, transform.right, wallDetectionDistance, obstacleMask);

                if (groundCheck.collider == false) {
                    if (movingRight == true) {
                        transform.eulerAngles = new Vector3 (0, -180, 0);
                        movingRight = false;
                    } else {
                        transform.eulerAngles = new Vector3 (0, 0, 0);
                        movingRight = true;
                    }
                }

                if (wallCheck.collider == true) {
                    if (movingRight == true) {
                        transform.eulerAngles = new Vector3 (0, -180, 0);
                        movingRight = false;
                    } else {
                        transform.eulerAngles = new Vector3 (0, 0, 0);
                        movingRight = true;
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator FindingThePlayer () {
        while (true) {
            attackTimer += Time.deltaTime;
            if (currentState == EnemyState.patrolling) {
                //Debug.DrawLine (transform.position, new Vector3 (transform.position.x + viewRange, transform.position.y, transform.position.z), Color.red);

                RaycastHit2D hitInfo = Physics2D.Raycast (transform.position, transform.right, viewRange, targetMask);
                if (hitInfo.collider != null && hitInfo.collider.tag == "Player") {
                    playerPos = hitInfo.collider.transform.position;
                    if (attackTimer > attackInterval) {
                        PrepareForAttack ();
                        attackTimer = 0;
                    }
                }
            }
            yield return null;
        }
    }

    private void PrepareForAttack () {
        enemySprite.color = Color.red;
        currentState = EnemyState.attacking;
        StartCoroutine ("AttackThePlayer");
    }

    private IEnumerator AttackThePlayer () {
        float percent = 0;
        Vector2 oldPos = transform.position;
        yield return new WaitForSeconds (0.5f);
        while (percent < 1) {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = Mathf.Pow (percent, 2) * 4f;
            Vector2 targetPos = new Vector2 (playerPos.x, transform.position.y);
            transform.position = Vector2.Lerp (oldPos, targetPos, interpolation);
            enemySprite.color = Color.white;
            yield return null;
        }
        currentState = EnemyState.patrolling;
    }

    public override void TakeHit (float damage, Vector3 hitPoint, Vector3 hitDirection) {

        if (damage >= health) {
            if (OnDeathStatic != null) {
                OnDeathStatic ();
            }

        }
        base.TakeHit (damage, hitPoint, hitDirection);
    }
}