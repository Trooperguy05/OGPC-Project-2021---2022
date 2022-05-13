using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool disableOnInteract = false;
    public Dialogue dialogue;

    // when player collides with a dialogue trigger, enter dialogue
    void OnTriggerEnter2D(Collider2D col) {
        GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>().StartDialogue(dialogue);
        if (disableOnInteract) {
            gameObject.SetActive(false);
        }
    }
}
