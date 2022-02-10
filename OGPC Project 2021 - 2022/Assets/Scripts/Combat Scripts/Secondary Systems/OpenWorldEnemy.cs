using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldEnemy : MonoBehaviour
{
    // making so a specified enemy has to be in the combat
    public int specifiedEnemy = -1; // -1 if none, positive if there is
    
    // enemy animator
    private Animator animator;

    // the movement types the enemy can make \\
    [Header("Movement Types")]
    public bool moveCircle = false;
    public bool moveLeftRight = false;
    public bool moveUpDown = false;
    public int moveTick = 1;

    // moving every x seconds \\
    [Header("Timed Movement")]
    public float timeBetweenMove = 2f;
    public float startTime;
    public float currentTime;
    void Start() {
        startTime = Time.time;
        animator = GetComponent<Animator>();
    }
    void Update() {
        currentTime = Time.time - startTime;

        // move every few seconds
        if (currentTime >= timeBetweenMove) {
            // moveCircle \\
            if (moveCircle) {
                if (moveTick == 1) {
                    transform.Translate(new Vector2(1f, 0f));
                    moveTick++;
                    animator.SetBool("right", true);
                }
                else if (moveTick == 2) {
                    transform.Translate(new Vector2(0f, -1f));
                    moveTick++;
                }
                else if (moveTick == 3) {
                    transform.Translate(new Vector2(0f, -1f));
                    moveTick++;
                }
                else if (moveTick == 4) {
                    transform.Translate(new Vector2(-1f, 0f));
                    animator.SetBool("right", false);
                    moveTick++;
                }
                else if (moveTick == 5) {
                    transform.Translate(new Vector2(-1f, 0f));
                    moveTick++;
                }
                else if (moveTick == 6) {
                    transform.Translate(new Vector2(0f, 1f));
                    moveTick++;
                }
                else if (moveTick == 7) {
                    transform.Translate(new Vector2(0f, 1f));
                    moveTick++;
                }
                else if (moveTick == 8) {
                    transform.Translate(new Vector2(1f, 0f));
                    animator.SetBool("right", true);
                    moveTick = 1;
                }
            }
            // move leftright \\
            else if (moveLeftRight) {
                if (moveTick == 1) {
                    transform.Translate(new Vector2(1f, 0f));
                    animator.SetBool("right", true);
                    moveTick++;
                }
                else if (moveTick == 2) {
                    transform.Translate(new Vector2(1f, 0f));
                    moveTick++;
                }
                else if (moveTick == 3) {
                    transform.Translate(new Vector2(-1f, 0f));
                    animator.SetBool("right", false);
                    moveTick++;
                }
                else if (moveTick == 4) {
                    transform.Translate(new Vector2(-1f, 0f));
                    moveTick++;
                }
                else if (moveTick == 5) {
                    transform.Translate(new Vector2(-1f, 0f));
                    moveTick++;
                }
                else if (moveTick == 6) {
                    transform.Translate(new Vector2(-1f, 0f));
                    moveTick++;
                }
                else if (moveTick == 7) {
                    transform.Translate(new Vector2(1f, 0f));
                    animator.SetBool("right", true);
                    moveTick++;
                }
                else if (moveTick == 8) {
                    transform.Translate(new Vector2(1f, 0f));
                    moveTick = 1;
                }
            }
            // move updown \\
            else if (moveUpDown) {
                if (moveTick == 1) {
                    transform.Translate(new Vector2(0f, 1f));
                    animator.SetBool("right", true);
                    moveTick++;
                }
                else if (moveTick == 2) {
                    transform.Translate(new Vector2(0f, 1f));
                    moveTick++;
                }
                else if (moveTick == 3) {
                    transform.Translate(new Vector2(0f, -1f));
                    animator.SetBool("right", false);
                    moveTick++;
                }
                else if (moveTick == 4) {
                    transform.Translate(new Vector2(0f, -1f));
                    moveTick++;
                }
                else if (moveTick == 5) {
                    transform.Translate(new Vector2(0f, -1f));
                    moveTick++;
                }
                else if (moveTick == 6) {
                    transform.Translate(new Vector2(0f, -1f));
                    moveTick++;
                }
                else if (moveTick == 7) {
                    transform.Translate(new Vector2(0f, 1f));
                    animator.SetBool("right", true);
                    moveTick++;
                }
                else if (moveTick == 8) {
                    transform.Translate(new Vector2(0f, 1f));
                    moveTick = 1;
                }
            }

            // reset
            startTime = Time.time;
        }
    }

    // when an enemy interacts with the player
    // initiate combat with the player
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Entering Combat");
        gameObject.SetActive(false); // deactivate itself
        FindObjectOfType<PartyStats>().SaveData(); // save party stats
        FindObjectOfType<PlayerProgress>().savePlayerData(); // save player progress
        FindObjectOfType<OpenWorldEnemyManager>().saveActiveEnemies(); // save active enemies
        SaveSystem.SaveSpecifiedEnemy(this); // save the specified enemy
        SceneManager.LoadScene(2); // load the combat scene
    }
}
