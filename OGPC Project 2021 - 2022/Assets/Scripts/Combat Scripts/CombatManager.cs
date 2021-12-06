using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Random=UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    // arrays for tracking initiative \\
    [Header("Initiative")]
    private string[] initiativeNames = new string[8];
    private float[] initiativeCount = new float[8];
    public object[] enemiesInCombat = new object[4];
    public int initiativeIndex = 0;

    // enemies \\
    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3 ;
    public GameObject enemy4;
    // enemycreator objects
    public EnemyCreator e1;
    public EnemyCreator e2;
    public EnemyCreator e3;
    public EnemyCreator e4;

    // action scripts to monitor who's done what \\
    private PlayerActions playerActions;

    // load the party stats when the player enters combat
    void Start() {
        Debug.Log("Loading Party Stats");
        FindObjectOfType<PartyStats>().LoadData();

        // grabbing the enemy objects
        e1 = enemy1.GetComponent<CombatEnemy>().eOb;
        e2 = enemy2.GetComponent<CombatEnemy>().eOb;
        e3 = enemy3.GetComponent<CombatEnemy>().eOb;
        e4 = enemy4.GetComponent<CombatEnemy>().eOb;

        // grabbing the action scripts
        playerActions = GameObject.Find("Action Manager").GetComponent<PlayerActions>();
    }

    void Update() {
        // prints turn order
        // will remove later
        if (Input.GetKeyDown(KeyCode.I)) {
            startCombat();
            sortInitiative(initiativeCount);
            string str = "";
            for (int i = 0; i < initiativeNames.Length; i++) {
                str += initiativeNames[i] + ", ";
            }
            Debug.Log(str);
            Debug.Log(" ");
        }

        ///   Turn-Based Combat   \\\
        // if it is one of the player characters' turn
        if (initiativeNames[initiativeIndex] == "Dorne") {
            // if the player is done, pass the turn
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
            }
        }

        // if initiativeIndex is greater than 7, reset
        if (initiativeIndex > 7) {
            initiativeIndex = 0;
        }
    }

    // method that "rolls" for combat initiative \\
    void startCombat() {
        // grabbing party stats
        PartyStats stats = FindObjectOfType<PartyStats>();

        // creating new objects
        e1 = new EnemyCreator();
        e2 = new EnemyCreator();
        e3 = new EnemyCreator();
        e4 = new EnemyCreator();
        // creating the enemy objects
        enemiesInCombat[0] = e1;
        enemiesInCombat[1] = e2;
        enemiesInCombat[2] = e3;
        enemiesInCombat[3] = e4;

        // (temporary) reset the arrays
        for (int i = 0; i < 8; i++) {
            initiativeNames[i] = "";
            initiativeCount[i] = 0f;
        }

        // player character initiatives
        initiativeCount[0] = Random.Range(1, 20) + stats.char1Dexterity; //Razza
        initiativeCount[1] = Random.Range(1, 20) + stats.char2Dexterity; //Dorne
        initiativeCount[2] = Random.Range(1, 20) + stats.char3Dexterity; //Smithson
        initiativeCount[3] = Random.Range(1, 20) + stats.char4Dexterity; //Zor
        // enemy initiatives
        initiativeCount[4] = Random.Range(1, 20) + e1.dexterity;
        initiativeCount[5] = Random.Range(1, 20) + e2.dexterity;
        initiativeCount[6] = Random.Range(1, 20) + e3.dexterity;
        initiativeCount[7] = Random.Range(1, 20) + e4.dexterity;

        string str = "";
        for (int i = 0; i < initiativeCount.Length; i++) {
            str += initiativeCount[i] + ", ";
        }
        Debug.Log(str);
    }

    // sorts the initiative \\
    void sortInitiative(float[] arrIn) {
        int highestNumIndex = -1;
        float highestNum = -20.0f;
        int numRep = 8;
        // get the highest initiative roll
        for (int j = 0; j < numRep; j++) {
            for (int i = 0; i < numRep; i++) {
                // checking the highest
                if (highestNum < arrIn[i]) {
                    highestNum = arrIn[i];
                    highestNumIndex = i;
                }
                // checking for duplicates
                int person1 = 0; // the original highest
                int person2 = 0; // the equal
                if (highestNum == arrIn[i]) {
                    do {
                        person1 = Random.Range(1, 20);
                        person2 = Random.Range(1, 20);

                        if (person1 < person2) {
                            highestNum = arrIn[i];
                            highestNumIndex = i;
                        }
                    } while (person1 == person2);
                }
            }
            arrIn[highestNumIndex] = -21.0f;

            // if one of those initiatives is from the player characters
            if (highestNumIndex == 0 && !initiativeNames.Contains("Raza")) {
                initiativeNames[j] = "Raza";
            }
            else if (highestNumIndex == 1 && !initiativeNames.Contains("Dorne")) {
                initiativeNames[j] = "Dorne";
            }
            else if (highestNumIndex == 2 && !initiativeNames.Contains("Smithson")) {
                initiativeNames[j] = "Smithson";
            }
            else if (highestNumIndex == 3 && !initiativeNames.Contains("Zor")) {
                initiativeNames[j] = "Zor";
            }
            // if one of those inititaives is from the enemies
            if (highestNumIndex == 4) {
                initiativeNames[j] = e1.name;
            }
            else if (highestNumIndex == 5) {
                initiativeNames[j] = e2.name;
            }
            else if (highestNumIndex == 6) {
                initiativeNames[j] = e3.name;
            }
            else if (highestNumIndex == 7) {
                initiativeNames[j] = e4.name;
            }

            // reset variables
            highestNum = -20.0f;
            highestNumIndex = -1;
        }
    }
}
