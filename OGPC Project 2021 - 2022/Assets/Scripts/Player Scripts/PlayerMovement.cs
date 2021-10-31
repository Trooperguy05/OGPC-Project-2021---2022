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
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}