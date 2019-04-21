using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float maxMovementSpeed = 1.0f;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public GameObject Sprite;

    public static bool dead = false;

    private Rigidbody2D rb2d;
    private bool facingRight = true;
    public bool grounded;
    private Animator anim;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = Sprite.GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (dead)
            return;

        if (grounded) {
            if (anim.GetBool("Jumping"))
                anim.SetBool("Jumping", false);

            if (anim.GetBool("Sneezing"))
                anim.SetBool("Sneezing", false);
        }

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (grounded && !PlayerSneeze.sneezing)
            rb2d.velocity = new Vector2(move * maxMovementSpeed, rb2d.velocity.y);
        else
            rb2d.AddForce(new Vector2(move, 0));

        if (PlayerSneeze.sneezing && !grounded)
            PlayerSneeze.sneezing = false;

        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
            Flip();
    }

    void Update() {
        if (dead) {
            if (!facingRight) {
                facingRight = true;
                Vector3 localScale = Sprite.transform.localScale;
                localScale.x *= -1;
                Sprite.transform.localScale = localScale;
            }

            return;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            anim.SetBool("Jumping", true);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Spike") {
            dead = true;
            anim.Play("Death", -1, 0);
        } else if (other.gameObject.tag == "Pill") {
            SceneManager.LoadScene("Level 2", LoadSceneMode.Single);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;

        if (!grounded && collision.gameObject.transform.position.y <= groundCheck.transform.position.y) {
            grounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;

        if (grounded && collision.gameObject.transform.position.y <= groundCheck.transform.position.y) {
            grounded = false;
        }
    }

    private void Flip() {
        facingRight = !facingRight;
        Vector3 localScale = Sprite.transform.localScale;
        localScale.x *= -1;
        Sprite.transform.localScale = localScale;
    }
}