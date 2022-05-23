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
    private bool updatedStartingSprite = false;
    public OverworldOST walkin;
    public enum direction {
        up,
        right,
        left,
        down
    }
    public direction playerDirection = direction.down;
    // player animator
    private Animator animator;
    // player sound
    public AudioSource AS;
    public bool playing = false;

    /// Caching Variables \\\
    void Awake() {
        pM = GameObject.Find("Party and Player Manager").GetComponent<PartyManager>();
        animator = GetComponent<Animator>();
    }
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // check for player input
    void Update() {
        if (playerAbleMove) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if ((movement.x != 0 || movement.y != 0)) {
                if (!playing && playerAbleMove) {
                    AS.Play();
                    playing = true;
                }
            }
            else {
                AS.Stop();
                playing = false;
            }
        }
        if (!playerAbleMove)
        {
            AS.Stop();
            playing = false;
        }

        // change sprite based on movement \\
        // horizontal movement
        if (movement.x > 0.5f || movement.x < -0.5f) {
            animator.speed = 1f;
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            // right
            if (movement.x > 0.5f) {
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
            }
            // left
            else if (movement.x < -0.5f) {
                animator.SetBool("Right", false);
                animator.SetBool("Left", true);
            }
        }
        // vertical movement
        else if (movement.y > 0.5f || movement.y < -0.5f) {
            animator.speed = 1f;
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
            // right
            if (movement.y > 0.5f) {
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
            }
            // left
            else if (movement.y < -0.5f) {
                animator.SetBool("Up", false);
                animator.SetBool("Down", true);
            }
        }
        // if the player is idle \\
        else {
            if (!PartyManager.partyTabOpen && loadingScreenManager.loadingDone) {
                animator.speed = 0f;
                AnimatorClipInfo[] currentAnimInfo = animator.GetCurrentAnimatorClipInfo(0);
                if (currentAnimInfo.Length > 0) animator.Play(currentAnimInfo[0].clip.name, 0, 0);
            }
        }

        // update the starting sprite to the last save's \\
        if (!updatedStartingSprite) {
            animator.speed = 1f;
            pM.updatePlayerSprite();
            updateIdleSprite();
            updatedStartingSprite = true;
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
        else {
            movement.x = 0f;
            movement.y = 0f;
        }
    }

    // update the idle sprite when lead member changes \\
    public void updateIdleSprite() {
        animator.speed = 1f;
        // set the leader based on who is heading the party \\
        if (pM.leader == PartyManager.PartyLead.Raza) {
            animator.SetBool("razaLeader", true);
            animator.SetBool("dorneLeader", false);
            animator.SetBool("smithsonLeader", false);
            animator.SetBool("zorLeader", false);
        }
        else if (pM.leader == PartyManager.PartyLead.Dorne) {
            animator.SetBool("razaLeader", false);
            animator.SetBool("dorneLeader", true);
            animator.SetBool("smithsonLeader", false);
            animator.SetBool("zorLeader", false);
        }
        else if (pM.leader == PartyManager.PartyLead.Smithson) {
            animator.SetBool("razaLeader", false);
            animator.SetBool("dorneLeader", false);
            animator.SetBool("smithsonLeader", true);
            animator.SetBool("zorLeader", false);
        }
        else if (pM.leader == PartyManager.PartyLead.Zor) {
            animator.SetBool("razaLeader", false);
            animator.SetBool("dorneLeader", false);
            animator.SetBool("smithsonLeader", false);
            animator.SetBool("zorLeader", true);
        }
        // update the direction \\
        // if player is directed down
        if (playerDirection == direction.down) {
            animator.SetBool("Down", true);
            animator.SetBool("Up", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        // if player is directed up
        else if (playerDirection == direction.up) {
            animator.SetBool("Down", false);
            animator.SetBool("Up", true);
            animator.SetBool("Right", false);
            animator.SetBool("Left", false);
        }
        // if player is directed right
        else if (playerDirection == direction.right) {
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }
        // if player is direction left
        else if (playerDirection == direction.left) {
            animator.SetBool("Down", false);
            animator.SetBool("Up", false);
            animator.SetBool("Right", false);
            animator.SetBool("Left", true);
        }
        animator.speed = 0f;
    }

    // method to check if the player animator is playing \\
    private bool isPlaying(string stateName) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
            return true;
        }
        else {
            return false;
        }
    }
}