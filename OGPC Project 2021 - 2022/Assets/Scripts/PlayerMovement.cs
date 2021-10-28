using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variables
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

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
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}