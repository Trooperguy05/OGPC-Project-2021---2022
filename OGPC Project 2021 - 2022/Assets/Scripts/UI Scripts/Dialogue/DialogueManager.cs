using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // sentence queue of the dialogue \\
    private Queue<string> sentences;
    private Queue<string> names;

    // dialogue box game object and text fields \\
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public static bool InDialogue = false;
    // its animator
    private Animator dialogueBoxAnimator;

    // text typing speed
    public float typeSpeed = 0.1f;

    // initialize the queue \\
    void Start() {
        sentences = new Queue<string>();
        names = new Queue<string>();

        // grabbing the dialogue box animator
        dialogueBoxAnimator = dialogueBox.GetComponent<Animator>();
    }

    // method that starts the dialogue \\
    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);
        dialogueBoxAnimator.SetBool("dialogueOn", true);
        PlayerMovement.playerAbleMove = false;
        InDialogue = true;

        // if there are multiple people in the conversation
        if (dialogue.names.Length > 0) {
            names.Clear();
            foreach(string name in dialogue.names) {
                names.Enqueue(name);
            }
        }
        else {
            dialogueName.text = dialogue.name;
        }

        // sentences
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
        string name = "";
        if (names.Count > 0) {
            name = names.Dequeue();
        }
        StopAllCoroutines();
        StartCoroutine(typeText(name, sentence));
    }

    // method that types the sentence in keyboard-like fashion \\
    IEnumerator typeText(string name, string sentence) {
        dialogueText.text = "";
        if (name != "") {
            dialogueName.text = name;
        }
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
        InDialogue = false;
    }
}
