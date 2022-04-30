using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWell : MonoBehaviour
{
    // interactable necessary variables
    private Animator notificationAnimator;
    private interactable interactable;

    // interact dialogue
    public Dialogue pickupNotification;
    public Dialogue noMoneyNotification;
    
    // player variables
    private GameObject player;
    private Inventory playerInventory;
    private PartyStats pS;

    // caching
    void Start()
    {
        notificationAnimator = GameObject.Find("Interact Notice").GetComponent<Animator>();
        interactable = new interactable(2);
        player = GameObject.Find("OverworldPlayerCharacter");
        playerInventory = player.GetComponent<Inventory>();
        pS = GameObject.Find("Party and Player Manager").GetComponent<PartyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is within range to interact
        float distance = interactable.checkDistance(gameObject, player);
        if (distance <= 5) {
            if (distance <= interactable.interactableRange) {
                interactable.changeInteract(true);
            }
            else {
                interactable.changeInteract(false);
            }
            Debug.Log(interactable.isInteractable());
            notificationAnimator.SetBool("open", interactable.isInteractable());
        }

        // player interacts with the magic well
        if (interactable.isInteractable()) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                // find the coin in the player's inventory
                int coinIndex = playerInventory.findItem(new Item(Item.ItemType.Coin, 1, true, false));

                // if the player has coin and at least 50
                if (coinIndex != -1 && playerInventory.inventory[coinIndex].quantity >= 50) {
                    playerInventory.removeItem(coinIndex, 50);
                    pS.char1HP = pS.char1HPMax;
                    pS.char2HP = pS.char2HPMax;
                    pS.char3HP = pS.char3HPMax;
                    pS.char3Mana = pS.char3ManaMax;
                    pS.char4HP = pS.char4HPMax;
                    GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(pickupNotification);
                }
                // if the player has less than 50 coins
                else {
                    GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(noMoneyNotification);
                }
            }
        }
    }
}
