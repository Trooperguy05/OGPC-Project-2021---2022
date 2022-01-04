using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // sentence queue of the dialogue \\
    private Queue<string> sentences;

    // dialogue box game object and text fields \\
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    // its animator
    private Animator dialogueBoxAnimator;

    // text typing speed
    public float typeSpeed = 0.1f;

    // initialize the queue \\
    void Start() {
        sentences = new Queue<string>();

        // grabbing the dialogue box animator
        dialogueBoxAnimator = dialogueBox.GetComponent<Animator>();
    }

    // method that starts the dialogue \\
    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
        dialogueBoxAnimator.SetBool("dialogueOn", true);
        PlayerMovement.playerAbleMove = false;
        dialogueName.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // method that displays the next sentence in a dialogue \\
    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeText(sentence));
    }

    // method that types the sentence in keyboard-like fashion \\
    IEnumerator typeText(string sentence) {
        dialogueText.text = "";
        for (int i = 0; i < sentence.Length; i++) {
            dialogueText.text += sentence[i];
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // method that ends the dialogue \\
    public void EndDialogue() {
        Debug.Log("End of conversation");
        dialogueBoxAnimator.SetBool("dialogueOn", false);
        PlayerMovement.playerAbleMove = true;
    }
}
