using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variables
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public static bool playerAbleMove = true;

    // caching variables
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // check for player input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // move the player
    void FixedUpdate() {
        if (playerAbleMove) {
            // only allowing the player to move one direction at a time
            if (movement.x > 0.5f || movement.x < -0.5f) {
                rb.MovePosition(rb.position + new Vector2(movement.x, 0f) * speed * Time.fixedDeltaTime);
            }
            else if (movement.y > 0.5f || movement.y < -0.5f) {
                rb.MovePosition(rb.position + new Vector2(0f, movement.y) * speed * Time.fixedDeltaTime);
            }
        }
    }
}