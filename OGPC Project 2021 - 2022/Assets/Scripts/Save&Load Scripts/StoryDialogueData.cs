using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryDialogueData
{
    public bool[] triggers;

    public StoryDialogueData(StoryDialogueManager sDM) {
        triggers = new bool[sDM.triggers.Length];
        for (int i = 0; i < sDM.triggers.Length; i++) {
            triggers[i] = sDM.triggers[i].activeSelf;
        } 
    }
}
