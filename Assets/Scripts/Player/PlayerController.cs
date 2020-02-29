using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public int extraJumpTimes;
    private int extraJumpCounter;

    private bool isClimbing;
    public Transform climbCheck;
    private float climbCheckRadius = 0.1f;

    private void Start () {
        rb = GetComponent<Rigidbody2D> ();
        extraJumpCounter = extraJumpTimes;
    }

    private void Update () {
        if (isGrounded) {
            extraJumpCounter = extraJumpTimes;
        }

        if (isGrounded == false && isClimbing == true) {
            extraJumpCounter = extraJumpTimes;
        }

        if (Input.GetKeyDown (KeyCode.Space) && extraJumpCounter > 0) {
            rb.velocity = Vector2.up * jumpForce;
            extraJumpCounter--;
        } else if (Input.GetKeyDown (KeyCode.Space) && extraJumpCounter == 0 && isGrounded) {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate () {

        isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, whatIsGround);
        isClimbing = Physics2D.OverlapCircle (climbCheck.position, climbCheckRadius, whatIsGround);

        moveInput = Input.GetAxisRaw ("Horizontal");
        rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0) {
            Flip ();
        } else if (facingRight == true && moveInput < 0) {
            Flip ();
        }
    }

    private void OnTriggerStay2D (Collider2D other) {
        if (other.tag == "End Point") {
            if (Input.GetKeyDown (KeyCode.E))
                GameManager.instance.EnterNextLevel ();
        }
    }

    private void Flip () {
        facingRight = !facingRight;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}