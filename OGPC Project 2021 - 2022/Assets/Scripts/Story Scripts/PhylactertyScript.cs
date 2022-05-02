using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylactertyScript : MonoBehaviour
{
    // check which phylactery this object is
    [Header("Which phylactery?")]
    public bool isDesertPhy;
    public bool isForestPhy;
    public bool isSwampPhy;

    // phylacterty animator
    private Animator animator;
    // interact notice
    private Animator interactNoticeAnimator;
    // interactable object
    private interactable interact;
    // player progress script
    private PlayerProgress pP;
    // player object
    private GameObject player;
    // after destruction dialogue
    public Dialogue dialogue;

    // caching
    void Start() {
        animator = GetComponent<Animator>();
        pP = GameObject.Find("Party and Player Manager").GetComponent<PlayerProgress>();
        player = GameObject.Find("OverworldPlayerCharacter");
        interactNoticeAnimator = GameObject.Find("Interact Notice").GetComponent<Animator>();
        interact = new interactable(2);

        // disable this object if the player has already destroyed it (in a previous save)
        if (isDesertPhy && pP.destroyedDesertPhylactery) {
            gameObject.SetActive(false);
        }
        else if (isSwampPhy && pP.destroyedSwampPhylactery) {
            gameObject.SetActive(false);
        }
        else if (isForestPhy && pP.destroyedForestPhylactery) {
            gameObject.SetActive(false);
        }
    }

    // if the phylactery is interactable, the player can destroy it \\
    void Update() {
        // check if player is within interact range
        float distance = interact.checkDistance(gameObject, player);
        if (distance <= 5) {
            if (distance <= interact.interactableRange) {
                interact.changeInteract(true);
            }
            else {
                interact.changeInteract(false);
            }  
            interactNoticeAnimator.SetBool("open", interact.isInteractable());      
        }

        // allow the player to destroy the phylactery
        if (interact.isInteractable()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                animator.SetTrigger("destroy");
                if (isDesertPhy) {
                    pP.destroyedDesertPhylactery = true;
                }
                else if (isSwampPhy) {
                    pP.destroyedSwampPhylactery = true;
                }
                else if (isForestPhy) {
                    pP.destroyedForestPhylactery = true;
                }
                StartCoroutine(wait());
            }
        }
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
        // start the after destruction dialogue
        GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(dialogue);
        interactNoticeAnimator.SetBool("open", false);
        // destroy the gameobject
        Destroy(gameObject);
    }
}
