using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyStats : MonoBehaviour
{
    // character stats \\
    [Header("Character 1")]
    public string char1Name = "Raza";
    public int char1HP = 85;
    public int char1HPMax = 85;
    public int char1Mana = 0;
    public int char1ManaMax = 0;
    public int char1Dexterity = 2;
    public int char1VMeter = 0;

    [Header("Character 2")]
    public string char2Name = "Dorne";
    public int char2HP = 100;
    public int char2HPMax = 100;
    public int char2Mana = 0;
    public int char2ManaMax = 0;
    public int char2Dexterity = 1;
    public int char2VMeter = 0;

    [Header("Character 3")]
    public string char3Name = "Smithson";
    public int char3HP = 75;
    public int char3HPMax = 75;
    public int char3Mana = 100;
    public int char3ManaMax = 100;
    public int char3Dexterity = 0;
    public int char3VMeter = 0;

    [Header("Character 4")]
    public string char4Name = "Zor";
    public int char4HP = 100;
    public int char4HPMax = 100;
    public int char4Mana = 0;
    public int char4ManaMax = 0;
    public int char4Dexterity = 1;
    public int char4VMeter = 0;

    // player animator
    public Animator playerAnimator;

    // methods to save and load the party stats \\
    public void SaveData() {
        Debug.Log("Saving Party Data");
        SaveSystem.SavePartyStats(this);
    }

    public void LoadData() {
        Debug.Log("Loading Party Data");
        PartyData data = SaveSystem.LoadPartyStats();
        if (data != null) {
            // data for Astar
            char1Name = data.char1Name;
            char1HP = data.char1HP;
            char1HPMax = data.char1HPMax;
            char1Mana = data.char1Mana;
            char1ManaMax = data.char1ManaMax;
            char1Dexterity = data.char1Dexterity;
            char1VMeter = data.char1VMeter;
            // data for Gor
            char2Name = data.char2Name;
            char2HP = data.char2HP;
            char2HPMax = data.char2HPMax;
            char2Mana = data.char2Mana;
            char2ManaMax = data.char2ManaMax;
            char2Dexterity = data.char2Dexterity;
            char2VMeter = data.char2VMeter;
            // data for Gadriel
            char3Name = data.char3Name;
            char3HP = data.char3HP;
            char3HPMax = data.char3HPMax;
            char3Mana = data.char3Mana;
            char3ManaMax = data.char3ManaMax;
            char3Dexterity = data.char3Dexterity;
            char3VMeter = data.char3VMeter;
            // data for Lyra
            char4Name = data.char4Name;
            char4HP = data.char4HP;
            char4HPMax = data.char4HPMax;
            char4Mana = data.char4Mana;
            char4ManaMax = data.char4ManaMax;
            char4Dexterity = data.char4Dexterity;
            char4VMeter = data.char4VMeter;

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
            if (SceneManager.GetActiveScene().name == "Template Project" || SceneManager.GetActiveScene().name == "OverworldScene") {
                PartyManager pM = FindObjectOfType<PartyManager>();
                pM.updatePlayerSprite();
                pM.updatePartyWheel();
                if (PartyManager.partyOrder[0] == "Raza") {
                    playerAnimator.SetBool("razaLeader", true);
                }
                else if (PartyManager.partyOrder[0] == "Dorne") {
                    playerAnimator.SetBool("dorneLeader", true);
                }
                else if (PartyManager.partyOrder[0] == "Smithson") {
                    playerAnimator.SetBool("smithsonLeader", true);
                }
                else if (PartyManager.partyOrder[0] == "Zor") {
                    playerAnimator.SetBool("zorLeader", true);
                }
                FindObjectOfType<PlayerMovement>().updateIdleSprite();
            }  
        }
        else {
            playerAnimator.SetBool("razaLeader", true);
        }
    }
}
