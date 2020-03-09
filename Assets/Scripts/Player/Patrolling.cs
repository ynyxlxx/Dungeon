using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour {
    public float patrolSpeed = 5f;
    public float groundDetectionDistance = 1f;
    public Transform groundDetectionPoint;

    private bool timeToTurnAround = false;
    private bool movingRight = true;

    private void Start () {
        StartCoroutine (StartPatrol ());
    }

    private IEnumerator StartPatrol () {
        while (true) {
            if (timeToTurnAround == false) {
                transform.Translate (patrolSpeed * Time.deltaTime * Vector2.right);
            }
            RaycastHit2D groundCheck = Physics2D.Raycast (groundDetectionPoint.position, Vector2.down, groundDetectionDistance);

            if (groundCheck.collider == false) {
                yield return StartCoroutine (TurnAround ());
            }
            yield return null;
        }
    }

    private IEnumerator TurnAround () {
        timeToTurnAround = true;
        yield return new WaitForSeconds (0.5f);
        if (movingRight == true) {
            transform.eulerAngles = new Vector3 (0, -180, 0);
            movingRight = false;
        } else {
            transform.eulerAngles = new Vector3 (0, 0, 0);
            movingRight = true;
        }
        yield return new WaitForSeconds (0.5f);
        timeToTurnAround = false;
    }
}