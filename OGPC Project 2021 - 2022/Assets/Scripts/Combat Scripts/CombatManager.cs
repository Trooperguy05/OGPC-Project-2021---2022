using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Random=UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    // arrays for tracking initiative \\
    private string[] initiativeNames = new string[8];
    private int[] initiativeCount = new int[8];

    // load the party stats when the player enters combat
    void Start() {
        Debug.Log("Loading Party Stats");
        FindObjectOfType<PartyStats>().LoadData();
    }

    void Update() {
        // return to the overworld scene from the combat scene
        // will remove later
        if (Input.GetKeyDown(KeyCode.Space)) {
            FindObjectOfType<PartyStats>().SaveData();

            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            startCombat();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            startCombat();
            sortThing(initiativeCount);
            string str = "";
            for (int i = 0; i < initiativeNames.Length; i++) {
                str += initiativeNames[i] + ", ";
            }
            Debug.Log(str);
        }
    }

    // method that "rolls" for combat initiative \\
    void startCombat() {
        // grabbing party stats
        PartyStats stats = FindObjectOfType<PartyStats>();

        /*
        for (int i = 0; i < initiativeCount.Length; i++) {
            int num = Random.Range(1, 20);
            initiativeCount[i] = num;
        }
        */
        initiativeCount[0] = Random.Range(1, 20) + stats.char1Dexterity; //Razza
        initiativeCount[1] = Random.Range(1, 20) + stats.char2Dexterity; //Dorne
        initiativeCount[2] = Random.Range(1, 20) + stats.char3Dexterity; //Smithson
        initiativeCount[3] = Random.Range(1, 20) + stats.char4Dexterity; //Zor
    }

    // sorts the initiative \\
    void sortThing(int[] arrIn) {
        int highestNumIndex = -1;
        int highestNum = -20;
        int numRep = 4;
        for (int j = 0; j < numRep; j++) {
            // get the highest initiative roll
            for (int i = 0; i < arrIn.Length; i++) {
                if (highestNum < arrIn[i]) {
                    highestNum = arrIn[i];
                    arrIn[i] = -21;
                    highestNumIndex = i;
                }
            }
            // if one of those initiatives is from the player characters
            if (highestNumIndex == 0) {
                initiativeNames[j] = "Razza";
            }
            else if (highestNumIndex == 1) {
                initiativeNames[j] = "Dorne";
            }
            else if (highestNumIndex == 2) {
                initiativeNames[j] = "Smithson";
            }
            else if (highestNumIndex == 3) {
                initiativeNames[j] = "Zor";
            }
            // if one of those inititaives is from the enemies

            // reset variables
            highestNum = -20;
            highestNumIndex = -1;
        }
    }
}
