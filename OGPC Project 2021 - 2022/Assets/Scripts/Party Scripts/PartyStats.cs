using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyStats : MonoBehaviour
{
    // character stats \\
    [Header("Character 1")]
    public string char1Name = "Raza";
    public int char1HP = 85;
    public int char1Mana = 45;
    public int char1Dexterity = 2;

    [Header("Character 2")]
    public string char2Name = "Dorne";
    public int char2HP = 100;
    public int char2Mana = 0;
    public int char2Dexterity = 1;

    [Header("Character 3")]
    public string char3Name = "Smithson";
    public int char3HP = 75;
    public int char3Mana = 100;
    public int char3Dexterity = 0;

    [Header("Character 4")]
    public string char4Name = "Zor";
    public int char4HP = 100;
    public int char4Mana = 35;
    public int char4Dexterity = 1;

    // methods to save and load the party stats \\
    public void SaveData() {
        Debug.Log("Saving Party Data");
        SaveSystem.SavePartyStats(this);
    }

    public void LoadData() {
        Debug.Log("Loading Party Data");
        PartyData data = SaveSystem.LoadPartyStats();

        // data for Astar
        char1Name = data.char1Name;
        char1HP = data.char1HP;
        char1Mana = data.char1Mana;
        char1Dexterity = data.char1Dexterity;
        // data for Gor
        char2Name = data.char2Name;
        char2HP = data.char2HP;
        char2Mana = data.char2Mana;
        char2Dexterity = data.char2Dexterity;
        // data for Gadriel
        char3Name = data.char3Name;
        char3HP = data.char3HP;
        char3Mana = data.char3Mana;
        char3Dexterity = data.char3Dexterity;
        // data for Lyra
        char4Name = data.char4Name;
        char4HP = data.char4HP;
        char4Mana = data.char4Mana;
        char4Dexterity = data.char4Dexterity;

        // data for party order
        PartyManager.character1Order = data.char1Order;
        PartyManager.character2Order = data.char2Order;
        PartyManager.character3Order = data.char3Order;
        PartyManager.character4Order = data.char4Order;
        PartyManager.partyOrder[0] = data.partyOrder[0];
        PartyManager.partyOrder[1] = data.partyOrder[1];
        PartyManager.partyOrder[2] = data.partyOrder[2];
        PartyManager.partyOrder[3] = data.partyOrder[3];
        // update the visuals based on the loaded data
        FindObjectOfType<PartyManager>().updatePlayerSprite();
        FindObjectOfType<PartyManager>().updatePartyWheel();
    }
}
