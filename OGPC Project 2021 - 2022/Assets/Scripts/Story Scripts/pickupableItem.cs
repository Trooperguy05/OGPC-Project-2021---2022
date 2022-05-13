using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupableItem : MonoBehaviour
{
    // interact object
    private interactable interact;
    // player inventory
    private GameObject player;
    private Inventory playerInven;
    // item
    public Item item;
    // item notification
    private Animator interactNoticeAnimator;
    public Dialogue pickupNotification;

    // caching
    void Start()
    {
        interact = new interactable(2);
        player = GameObject.Find("OverworldPlayerCharacter");
        playerInven = player.GetComponent<Inventory>();
        interactNoticeAnimator = GameObject.Find("Interact Notice").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        // allow player to pick up the item
        if (interact.isInteractable()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                playerInven.addItem(item);
                GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(pickupNotification);
                interactNoticeAnimator.SetBool("open", false);
                gameObject.SetActive(false);               
            }
        }
    }
}
