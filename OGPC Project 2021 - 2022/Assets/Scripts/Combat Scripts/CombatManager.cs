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
    private string[] initiativeNames = new string[8];
    private float[] initiativeCount = new float[8];
    public object[] enemiesInCombat = new object[4];
    public static int initiativeIndex = 0;

    // enemies \\
    private EnemyCreator enemy1;
    private EnemyCreator enemy2;
    private EnemyCreator enemy3 ;
    private EnemyCreator enemy4;

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
            sortInitiative(initiativeCount);
            string str = "";
            for (int i = 0; i < initiativeNames.Length; i++) {
                str += initiativeNames[i] + ", ";
            }
            Debug.Log(str);
            Debug.Log(" ");
        }
    }

    // method that "rolls" for combat initiative \\
    void startCombat() {
        // grabbing party stats
        PartyStats stats = FindObjectOfType<PartyStats>();

        // creating the enemy objects
        enemy1 = new EnemyCreator();
        enemy2 = new EnemyCreator();
        enemy3 = new EnemyCreator();
        enemy4 = new EnemyCreator();
        enemiesInCombat[0] = enemy1;
        enemiesInCombat[1] = enemy2;
        enemiesInCombat[2] = enemy3;
        enemiesInCombat[3] = enemy4;

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
        initiativeCount[4] = Random.Range(1, 20) + enemy1.dexterity;
        initiativeCount[5] = Random.Range(1, 20) + enemy2.dexterity;
        initiativeCount[6] = Random.Range(1, 20) + enemy3.dexterity;
        initiativeCount[7] = Random.Range(1, 20) + enemy4.dexterity;

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
                initiativeNames[j] = enemy1.name;
            }
            else if (highestNumIndex == 5) {
                initiativeNames[j] = enemy2.name;
            }
            else if (highestNumIndex == 6) {
                initiativeNames[j] = enemy3.name;
            }
            else if (highestNumIndex == 7) {
                initiativeNames[j] = enemy4.name;
            }

            // reset variables
            highestNum = -20.0f;
            highestNumIndex = -1;
        }
    }
}
