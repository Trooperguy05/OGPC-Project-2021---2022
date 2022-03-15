using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDialogueManager : MonoBehaviour
{
    // all story dialogue triggers
    public GameObject[] triggers;

    // Start is called before the first frame update
    void Start()
    {
        loadDialogueTriggers();
    }

    // function that saves the dialogue triggers \\
    public void saveDialogueTriggers() {
        Debug.Log("Saving story dialogue");
        SaveSystem.saveStoryDialogue(this);
    }
    
    // function that loads the dialogue triggers \\
    public async void loadDialogueTriggers() {
        Debug.Log("Loading story dialogue");
        StoryDialogueData data = SaveSystem.loadStoryDialogue();
        if (data != null) {
            for (int i = 0; i < data.triggers.Length; i++) {
                triggers[i].SetActive(data.triggers[i]);
            }
        }
    }
}
