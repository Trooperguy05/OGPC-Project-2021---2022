using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // sentence queue of the dialogue \\
    private Queue<string> sentences;

    // initialize the queue \\
    void Start() {
        sentences = new Queue<string>();
    }

    // method that starts the dialogue \\
    public void StartDialogue(Dialogue dialogue) {
        Debug.Log("Starting conversation with " + dialogue.name);

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
        Debug.Log(sentence);

    }

    // method that ends the dialogue \\
    public void EndDialogue() {
        Debug.Log("End of conversation");
    }
}
