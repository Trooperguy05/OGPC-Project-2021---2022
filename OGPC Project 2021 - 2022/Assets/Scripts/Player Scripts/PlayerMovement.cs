using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement variables \\
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public static bool playerAbleMove = true;

    // direction variables \\
    private PartyManager pM;
    private SpriteRenderer spriteRenderer;
    public enum direction {
        up,
        right,
        left,
        down
    }
    public direction playerDirection = direction.down;

    // character variables \\
    [Header("Character Idle Sprites")]
    // player animator
    private Animator animator;
    // raza sprites
    // dorne sprites
    // smithson sprites
    [Header("Smithson Sprites")]
    public Sprite smithsonUp_idle;
    public Sprite smithsonDown_idle;
    public Sprite smithsonRight_idle;
    public Sprite smithsonLeft_idle;
    // zor sprites

    // caching variables
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        pM = GameObject.Find("Party and Player Manager").GetComponent<PartyManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // check for player input
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // change sprite based on movement \\
        if (movement.x > 0.5f || movement.x < -0.5f) {
            // reset other animators
            animator.SetBool("smithsonUp", false);
            animator.SetBool("smithsonDown", false);
            // if party leader is smithson
            if (pM.leader == PartyManager.PartyLead.Smithson) {
                if (movement.x < -0.5f) {
                    animator.SetBool("smithsonLeft", true);
                    animator.SetBool("smithsonRight", false);
                }
                else if (movement.x > 0.5f) {
                    animator.SetBool("smithsonRight", true);
                    animator.SetBool("smithsonLeft", false);
                }
            }
        }
        else if (movement.y > 0.5f || movement.y < -0.5f) {
            // reset other animators
            animator.SetBool("smithsonLeft", false);
            animator.SetBool("smithsonRight", false);
            // if party leader is smithson
            if (pM.leader == PartyManager.PartyLead.Smithson) {
                if (movement.y < -0.5f) {
                    animator.SetBool("smithsonDown", true);
                    animator.SetBool("smithsonUp", false);
                }
                else if (movement.y > 0.5f) {
                    animator.SetBool("smithsonUp", true);
                    animator.SetBool("smithsonDown", false);
                }
            }
        }
        // if the player is idle
        else {
            // if the party leader is smithson
            if (pM.leader == PartyManager.PartyLead.Smithson) {
                if (playerDirection == direction.down) {
                    animator.SetBool("smithsonDown", false);
                    spriteRenderer.sprite = smithsonDown_idle;
                }
                else if (playerDirection == direction.left) {
                    animator.SetBool("smithsonLeft", false);
                    spriteRenderer.sprite = smithsonLeft_idle;
                }
                else if (playerDirection == direction.up) {
                    animator.SetBool("smithsonUp", false);
                    spriteRenderer.sprite = smithsonUp_idle;
                }
                else if (playerDirection == direction.right) {
                    animator.SetBool("smithsonRight", false);
                    spriteRenderer.sprite = smithsonRight_idle;
                }
            }
        }
    }

    // move the player
    void FixedUpdate() {
        if (playerAbleMove) {
            // horizontal movement \\
            if (movement.x > 0.5f || movement.x < -0.5f) {
                // change direction based on the movement variable
                if (movement.x > 0.5f) {
                    playerDirection = direction.right;
                }
                else if (movement.x < -0.5f) {
                    playerDirection = direction.left;
                }
                // move the player
                rb.MovePosition(rb.position + new Vector2(movement.x, 0f) * speed * Time.fixedDeltaTime);
            }
            // vertical movement \\
            else if (movement.y > 0.5f || movement.y < -0.5f) {
                // change direction based on the movement variable
                if (movement.y > 0.5f) {
                    playerDirection = direction.up;
                }
                else if (movement.y < -0.5f) {
                    playerDirection = direction.down;
                }
                // move the player
                rb.MovePosition(rb.position + new Vector2(0f, movement.y) * speed * Time.fixedDeltaTime);
            }
        }
    }
}