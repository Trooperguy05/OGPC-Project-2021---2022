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
    public AudioClip Walk;

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
            if (movement.x != 0 || movement.y != 0){
                AS.PlayOneShot(Walk);
            }
            else{
                AS.Stop();
            }

        }

        // change sprite based on movement \\
        // horizontal movement
        if (movement.x > 0.5f || movement.x < -0.5f) {
            animator.speed = 1f;
            // if party leader is raza
            if (pM.leader == PartyManager.PartyLead.Raza) {
                // reset other animators
                animator.SetBool("razaUp", false);
                animator.SetBool("razaDown", false);
                if (movement.x < -0.5f) {
                    animator.SetBool("razaLeft", true);
                    animator.SetBool("razaRight", false);
                }
                else if (movement.x > 0.5f) {
                    animator.SetBool("razaRight", true);
                    animator.SetBool("razaLeft", false);
                }
            }
            // if party leader is dorne
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                // reset other animators
                animator.SetBool("dorneUp", false);
                animator.SetBool("dorneDown", false);
                if (movement.x < -0.5f) {
                    animator.SetBool("dorneLeft", true);
                    animator.SetBool("dorneRight", false);
                }
                else if (movement.x > 0.5f) {
                    animator.SetBool("dorneRight", true);
                    animator.SetBool("dorneLeft", false);
                }
            }
             // if party leader is smithson
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                // reset other animators
                animator.SetBool("smithsonUp", false);
                animator.SetBool("smithsonDown", false);
                if (movement.x < -0.5f) {
                    animator.SetBool("smithsonLeft", true);
                    animator.SetBool("smithsonRight", false);
                }
                else if (movement.x > 0.5f) {
                    animator.SetBool("smithsonRight", true);
                    animator.SetBool("smithsonLeft", false);
                }
            }
            // if party leader is zor
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                // reset other animators
                animator.SetBool("zorUp", false);
                animator.SetBool("zorDown", false);
                if (movement.x < -0.5f) {
                    animator.SetBool("zorLeft", true);
                    animator.SetBool("zorRight", false);
                }
                else if (movement.x > 0.5f) {
                    animator.SetBool("zorRight", true);
                    animator.SetBool("zorLeft", false);
                }
            }
        }
        // vertical movement
        else if (movement.y > 0.5f || movement.y < -0.5f) {
            animator.speed = 1f;
            // if party leader is raza
            if (pM.leader == PartyManager.PartyLead.Raza) {
                // reset other animators
                animator.SetBool("razaLeft", false);
                animator.SetBool("razaRight", false);
                if (movement.y < -0.5f) {
                    animator.SetBool("razaDown", true);
                    animator.SetBool("razaUp", false);
                }
                else if (movement.y > 0.5f) {
                    animator.SetBool("razaUp", true);
                    animator.SetBool("razaDown", false);
                }
            }
            // if party leader is dorne
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                // reset other animators
                animator.SetBool("dorneLeft", false);
                animator.SetBool("dorneRight", false);
                if (movement.y < -0.5f) {
                    animator.SetBool("dorneDown", true);
                    animator.SetBool("dorneUp", false);
                }
                else if (movement.y > 0.5f) {
                    animator.SetBool("dorneUp", true);
                    animator.SetBool("dorneDown", false);
                }
            }
            // if party leader is smithson
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                // reset other animators
                animator.SetBool("smithsonLeft", false);
                animator.SetBool("smithsonRight", false);
                if (movement.y < -0.5f) {
                    animator.SetBool("smithsonDown", true);
                    animator.SetBool("smithsonUp", false);
                }
                else if (movement.y > 0.5f) {
                    animator.SetBool("smithsonUp", true);
                    animator.SetBool("smithsonDown", false);
                }
            }
            // if party leader is zor
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                // reset other animators
                animator.SetBool("zorLeft", false);
                animator.SetBool("zorRight", false);
                if (movement.y < -0.5f) {
                    animator.SetBool("zorDown", true);
                    animator.SetBool("zorUp", false);
                }
                else if (movement.y > 0.5f) {
                    animator.SetBool("zorUp", true);
                    animator.SetBool("zorDown", false);
                }
            }
        }
        // if the player is idle \\
        else {
            if (!pM.partyTabOpen && loadingScreenManager.loadingDone) {
                // if party leader is raza
                if (pM.leader == PartyManager.PartyLead.Raza) {
                    if (playerDirection == direction.down) {
                        animator.SetBool("razaDown", false);
                    }
                    else if (playerDirection == direction.left) {
                        animator.SetBool("razaLeft", false);
                    }
                    else if (playerDirection == direction.up) {
                        animator.SetBool("razaUp", false);
                    }
                    else if (playerDirection == direction.right) {
                        animator.SetBool("razaRight", false);
                    }
                }
                // if party leader is dorne
                else if (pM.leader == PartyManager.PartyLead.Dorne) {
                    if (playerDirection == direction.down) {
                        animator.SetBool("dorneDown", false);
                    }
                    else if (playerDirection == direction.left) {
                        animator.SetBool("dorneLeft", false);
                    }
                    else if (playerDirection == direction.up) {
                        animator.SetBool("dorneUp", false);
                    }
                    else if (playerDirection == direction.right) {
                        animator.SetBool("dorneRight", false);
                    }
                }
                // if the party leader is smithson
                else if (pM.leader == PartyManager.PartyLead.Smithson) {
                    if (playerDirection == direction.down) {
                        animator.SetBool("smithsonDown", false);
                    }
                    else if (playerDirection == direction.left) {
                        animator.SetBool("smithsonLeft", false);
                    }
                    else if (playerDirection == direction.up) {
                        animator.SetBool("smithsonUp", false);
                    }
                    else if (playerDirection == direction.right) {
                        animator.SetBool("smithsonRight", false);
                    }
                }
                // if the party leader is zor
                else if (pM.leader == PartyManager.PartyLead.Zor) {
                    if (playerDirection == direction.down) {
                        animator.SetBool("zorDown", false);
                    }
                    else if (playerDirection == direction.left) {
                        animator.SetBool("zorLeft", false);
                    }
                    else if (playerDirection == direction.up) {
                        animator.SetBool("zorUp", false);
                    }
                    else if (playerDirection == direction.right) {
                        animator.SetBool("zorRight", false);
                    }
                }
                animator.speed = 0f;
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
        // if player is directed down
        if (playerDirection == direction.down) {
            if (pM.leader == PartyManager.PartyLead.Raza) {
                animator.SetBool("razaDown", true);
                animator.SetBool("dorneDown", false);
                animator.SetBool("smithsonDown", false);
                animator.SetBool("zorDown", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                animator.SetBool("razaDown", false);
                animator.SetBool("dorneDown", true);
                animator.SetBool("smithsonDown", false);
                animator.SetBool("zorDown", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                animator.SetBool("razaDown", false);
                animator.SetBool("dorneDown", false);
                animator.SetBool("smithsonDown", true);
                animator.SetBool("zorDown", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                animator.SetBool("razaDown", false);
                animator.SetBool("dorneDown", false);
                animator.SetBool("smithsonDown", false);
                animator.SetBool("zorDown", true);
            }
        }
        // if player is directed up
        else if (playerDirection == direction.up) {
            if (pM.leader == PartyManager.PartyLead.Raza) {
                animator.SetBool("razaUp", true);
                animator.SetBool("dorneUp", false);
                animator.SetBool("smithsonUp", false);
                animator.SetBool("zorUp", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                animator.SetBool("razaUp", false);
                animator.SetBool("dorneUp", true);
                animator.SetBool("smithsonUp", false);
                animator.SetBool("zorUp", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                animator.SetBool("razaUp", false);
                animator.SetBool("dorneUp", false);
                animator.SetBool("smithsonUp", true);
                animator.SetBool("zorUp", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                animator.SetBool("razaUp", false);
                animator.SetBool("dorneUp", false);
                animator.SetBool("smithsonUp", false);
                animator.SetBool("zorUp", true);
            }
        }
        // if player is directed right
        else if (playerDirection == direction.right) {
            if (pM.leader == PartyManager.PartyLead.Raza) {
                animator.SetBool("razaRight", true);
                animator.SetBool("dorneRight", false);
                animator.SetBool("smithsonRight", false);
                animator.SetBool("zorRight", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                animator.SetBool("razaRight", false);
                animator.SetBool("dorneRight", true);
                animator.SetBool("smithsonRight", false);
                animator.SetBool("zorRight", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                animator.SetBool("razaRight", false);
                animator.SetBool("dorneRight", false);
                animator.SetBool("smithsonRight", true);
                animator.SetBool("zorRight", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                animator.SetBool("razaRight", false);
                animator.SetBool("dorneRight", false);
                animator.SetBool("smithsonRight", false);
                animator.SetBool("zorRight", true);
            }
        }
        // if player is direction left
        else if (playerDirection == direction.left) {
            if (pM.leader == PartyManager.PartyLead.Raza) {
                animator.SetBool("razaLeft", true);
                animator.SetBool("dorneLeft", false);
                animator.SetBool("smithsonLeft", false);
                animator.SetBool("zorLeft", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Dorne) {
                animator.SetBool("razaLeft", false);
                animator.SetBool("dorneLeft", true);
                animator.SetBool("smithsonLeft", false);
                animator.SetBool("zorLeft", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Smithson) {
                animator.SetBool("razaLeft", false);
                animator.SetBool("dorneLeft", false);
                animator.SetBool("smithsonLeft", true);
                animator.SetBool("zorLeft", false);
            }
            else if (pM.leader == PartyManager.PartyLead.Zor) {
                animator.SetBool("razaLeft", false);
                animator.SetBool("dorneLeft", false);
                animator.SetBool("smithsonLeft", false);
                animator.SetBool("zorLeft", true);
            }
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