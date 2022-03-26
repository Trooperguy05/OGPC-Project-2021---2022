using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylactertyScript : MonoBehaviour
{
    // phylacterty animator
    private Animator animator;
    public bool interactable = false;
    // player progress cript
    private PlayerProgress pP;

    // caching
    void Start() {
        animator = GetComponent<Animator>();
        pP = GameObject.Find("Party and Player Manager").GetComponent<PlayerProgress>();
    }

    // if the phylactery is interactable, the player can destroy it \\
    void Update() {
        if (interactable) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                animator.SetTrigger("destroy");
                pP.destroyedDesertPhylactery = true;
                StartCoroutine(wait());
            }
        }
    }

    // if the player touches the phylactery, it become interactable \\
    void OnTriggerEnter2D(Collider2D col) {
        interactable = true;
    }
    void OnTriggerExit2D(Collider2D col) {
        interactable = false;
    }

    // method that waits for the destroy animation to destroy the gameobject \\
    public IEnumerator wait() {
        // wait for animation to finish
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("phylacteryDestroy")) {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("phylacteryDestroy")) {
            yield return null;
        }
        animator.SetBool("destroyed", true);
        // destroy the gameobject
        Destroy(gameObject);
    }
}
