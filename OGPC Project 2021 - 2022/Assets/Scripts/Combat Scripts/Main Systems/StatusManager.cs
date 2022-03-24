using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////Status (usually damage over time) manager\\\\\\

public class StatusManager : MonoBehaviour

/// Variables
{
    public List<string> nameList = new List<string>();
    public List<string> effectList = new List<string>();
    public List<int> effectDurations = new List<int>();

    private PartyStats pM;

    void Start(){
        pM = GameObject.Find("Party Manager").GetComponent<PartyStats>();
    }

    /// Adds statuses to lists
    public void statusAdd(string target, string status, int duration){
        nameList.Add(target);
        effectList.Add(status);
        effectDurations.Add(duration);
    }

    /// Loops through every active status
    public void statusUpdate(){
        // apply the effect
        for(int i = 0; i < nameList.Count; i++){
            statusApply(i);
        }
        // remove effect if it has no duration left
        for (int i = 0; i < nameList.Count; i++) {
            if (effectDurations[i] == 0) {
                nameList.RemoveAt(i);
                effectList.RemoveAt(i);
                effectDurations.RemoveAt(i);
            }
        }
    }

    /// Activates effect of status while lowering the timer
    public void statusApply(int index){
        int dmg = 0;
        if (effectList[index] == "bleed"){
            dmg = 5;
        }
        else if (effectList[index] == "poison"){
            dmg = 3;
        }

        if (nameList[index] == "Raza" && effectDurations[index] != 0){
            effectDurations[index] -= 1;
            pM.char1HP -= dmg;
        }
        if (nameList[index] == "Zor" && effectDurations[index] != 0){
            effectDurations[index] -= 1;
            pM.char4HP -= dmg;
        }
        if (nameList[index] == "Smithson" && effectDurations[index] != 0){
            effectDurations[index] -= 1;
            pM.char3HP -= dmg;
        }
        if (nameList[index] == "Dorne" && effectDurations[index] != 0){
            effectDurations[index] -= 1;
            pM.char2HP -= dmg;
        }
    }
}
