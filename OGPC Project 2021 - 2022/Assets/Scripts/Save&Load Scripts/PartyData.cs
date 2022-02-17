using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartyData
{
    // party stats \\
    // character 1, Astar
    public string char1Name;
    public int char1HP;
    public int char1HPMax;
    public int char1Mana;
    public int char1ManaMax;
    public int char1Dexterity;

    // character 2, Gor
    public string char2Name;
    public int char2HP;
    public int char2HPMax;
    public int char2Mana;
    public int char2ManaMax;
    public int char2Dexterity;

    // character 3, Gadriel
    public string char3Name;
    public int char3HP;
    public int char3HPMax;
    public int char3Mana;
    public int char3ManaMax;
    public int char3Dexterity;

    // character 4, Lyra
    public string char4Name;
    public int char4HP;
    public int char4HPMax;
    public int char4Mana;
    public int char4ManaMax;
    public int char4Dexterity;

    // party order
    public int char1Order;
    public int char2Order;
    public int char3Order;
    public int char4Order;
    public string[] partyOrder = new string[4];

    // constructor \\
    public PartyData(PartyStats partyStats) {
        // data for Astar
        char1Name = partyStats.char1Name;
        char1HP = partyStats.char1HP;
        char1HPMax = partyStats.char1HPMax;
        char1Mana = partyStats.char1Mana;
        char1ManaMax = partyStats.char1ManaMax;
        char1Dexterity = partyStats.char1Dexterity;
        // data for Gor
        char2Name = partyStats.char2Name;
        char2HP = partyStats.char2HP;
        char2HPMax = partyStats.char2HPMax;
        char2Mana = partyStats.char2Mana;
        char2ManaMax = partyStats.char2ManaMax;
        char2Dexterity = partyStats.char2Dexterity;
        // data for Gadriel
        char3Name = partyStats.char3Name;
        char3HP = partyStats.char3HP;
        char3HPMax = partyStats.char3HPMax;
        char3Mana = partyStats.char3Mana;
        char3ManaMax = partyStats.char3ManaMax;
        char3Dexterity = partyStats.char3Dexterity;
        // data for Lyra
        char4Name = partyStats.char4Name;
        char4HP = partyStats.char4HP;
        char4HPMax = partyStats.char4HPMax;
        char4Mana = partyStats.char4Mana;
        char4ManaMax = partyStats.char4ManaMax;
        char4Dexterity = partyStats.char4Dexterity;

        // data for party order
        char1Order = PartyManager.character1Order;
        char2Order = PartyManager.character2Order;
        char3Order = PartyManager.character3Order;
        char4Order = PartyManager.character4Order;
        partyOrder[0] = PartyManager.partyOrder[0];
        partyOrder[1] = PartyManager.partyOrder[1];
        partyOrder[2] = PartyManager.partyOrder[2];
        partyOrder[3] = PartyManager.partyOrder[3];
    }
}
