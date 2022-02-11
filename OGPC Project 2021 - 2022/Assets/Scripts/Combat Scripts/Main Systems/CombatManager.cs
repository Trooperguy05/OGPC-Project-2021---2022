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
    public string[] initiativeNames = new string[8];
    public int[] initiativeCount = new int[8];
    public List<GameObject> enemiesInCombat = new List<GameObject>();
    public GameObject[] gameObjectsInCombat = new GameObject[8];
    public int initiativeIndex = 0;
    public int roundNum = 1;
    public bool combatStarted = false;

    // enemies \\
    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    // enemycreator objects
    public EnemyCreator e1;
    public EnemyCreator e2;
    public EnemyCreator e3;
    public EnemyCreator e4;
    // enemy field slots
    private int enemySlotsLeft = 4;
    // specified enemies
    private int specifiedEnemy;
    [Header("Other Scripts")]
    // script for enemy formation
    public EnemyFormation eF;
    // script for turn indicator
    public turnIndicator tI;
    // script for healthbar manager
    public HealthbarManager hM;

    // action scripts to monitor who's done what \\
    private PlayerActions playerActions;

    // load the party stats when the player enters combat
    void Start() {
        Debug.Log("Loading Party Stats");
        FindObjectOfType<PartyStats>().LoadData();

        // load specified enemy
        getSpecifiedEnemy();

        // grabbing the enemy objects
        e1 = enemy1.GetComponent<CombatEnemy>().eOb;
        e2 = enemy2.GetComponent<CombatEnemy>().eOb;
        e3 = enemy3.GetComponent<CombatEnemy>().eOb;
        e4 = enemy4.GetComponent<CombatEnemy>().eOb;

        // grabbing the action scripts
        playerActions = GameObject.Find("Action Manager").GetComponent<PlayerActions>();

        // grabbing the enemy formation script
        eF = GetComponent<EnemyFormation>();

        // grabbing the turn indicator script
        tI = GetComponent<turnIndicator>();
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
            Debug.Log(initiativeNames[initiativeIndex]);

            eF.organizeField();
            tI.updateIndicator();
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            initiativeIndex++;
            if (initiativeIndex <= 7 && initiativeNames[initiativeIndex] != "") {
                Debug.Log(initiativeNames[initiativeIndex]);
            }
            if (initiativeIndex <= 7) {
                tI.updateIndicator();
            }
        }

        ///   Turn-Based Combat   \\\
        // if it is one of the player characters' turn
        /*
        if (initiativeNames[initiativeIndex] == "Raza") {
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
            }
        }
        */
        /*
        if (initiativeNames[initiativeIndex] == "Dorne") {
            // if the player is done, pass the turn
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
            }
        }
        */

        // if initiativeNames[initiativeIndex] = "", continue to next person
        /*
        if (combatStarted) {
            if (initiativeNames[initiativeIndex] == "") {
                initiativeIndex++;
            }
        }
        */

        // if initiativeIndex is greater than 7, reset
        if (initiativeIndex > 7) {
            initiativeIndex = 0;
            roundNum++;
            Debug.Log("Round: " + roundNum);
            Debug.Log(initiativeNames[initiativeIndex]);
            tI.updateIndicator();
        }
    }

    // method that "rolls" for combat initiative \\
    void startCombat() {
        // grabbing party stats
        PartyStats stats = FindObjectOfType<PartyStats>();

        // (temporary) reset the arrays
        for (int h = 0; h < 8; h++) {
            initiativeNames[h] = "";
            initiativeCount[h] = 0;
        }
        e1 = null;
        e2 = null;
        e3 = null;
        e4 = null;
        enemySlotsLeft = 4;

        // create enemies based on size and space left \\
        int i = 0;
        while (enemySlotsLeft > 0) {
            if (e1 == null) {
                e1 = new EnemyCreator();
                if (enemySlotsLeft >= e1.size) {
                    enemySlotsLeft -= e1.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e1.dexterity;   
                    i++;
                    hM.enemy1Slider.maxValue = e1.healthMax;
                    hM.enemy1Slider.value = hM.enemy1Slider.maxValue;     
                }
                else {
                    e1 = null;
                }
            }
            else if (e2 == null) {
                e2 = new EnemyCreator();
                if (enemySlotsLeft >= e2.size) {
                    enemySlotsLeft -= e2.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e2.dexterity;
                    i++;
                    hM.enemy2Slider.maxValue = e2.healthMax;
                    hM.enemy2Slider.value = hM.enemy2Slider.maxValue; 
                }
                else {
                    e2 = null;
                }
            }
            else if (e3 == null) {
                e3 = new EnemyCreator();
                if (enemySlotsLeft >= e3.size) {
                    enemySlotsLeft -= e3.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e3.dexterity;
                    i++;
                    hM.enemy3Slider.maxValue = e3.healthMax;
                    hM.enemy3Slider.value = hM.enemy3Slider.maxValue;  
                }
                else {
                    e3 = null;
                }
            }
            else if (e4 == null) {
                e4 = new EnemyCreator();
                if (enemySlotsLeft >= e4.size) {
                    enemySlotsLeft -= e4.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e4.dexterity;
                    i++;
                    hM.enemy4Slider.maxValue = e4.healthMax;
                    hM.enemy4Slider.value = hM.enemy4Slider.maxValue;     
                }
                else {
                    e4 = null;
                }
            }
            Debug.Log("Slots left: " + enemySlotsLeft);

            if (enemySlotsLeft <= 0) {
                break;
            }
        }

        // player character initiatives
        initiativeCount[0] = Random.Range(1, 20) + stats.char1Dexterity; //Razza
        initiativeCount[1] = Random.Range(1, 20) + stats.char2Dexterity; //Dorne
        initiativeCount[2] = Random.Range(1, 20) + stats.char3Dexterity; //Smithson
        initiativeCount[3] = Random.Range(1, 20) + stats.char4Dexterity; //Zor

        string str = "";
        for (int j = 0; j < initiativeCount.Length; j++) {
            str += initiativeCount[j] + ", ";
        }
        Debug.Log(str);

        combatStarted = true;
    }

    // sorts the initiative from highest to lowest \\
    public void sortInitiative(int[] arrIn) {
        // reset the initiativenames if it isn't empty
        for (int i = 0; i < initiativeNames.Length; i++) {
            initiativeNames[i] = "";
        }
        for (int i = 0; i < gameObjectsInCombat.Length; i++) {
            gameObjectsInCombat[i] = null;
        }
        // variables used in algorithm
        int highestNumIndex = -1;
        int highestNum = -20;
        int numRep = 8;
        int[] tempArr = new int[8];

        // setting up the temporary array for initiative sorting
        for (int u = 0; u < arrIn.Length; u++) {
            tempArr[u] = arrIn[u];
        }

        // get the highest initiative roll
        for (int j = 0; j < numRep; j++) {
            for (int i = 0; i < numRep; i++) {
                // checking the highest
                if (highestNum < tempArr[i]) {
                    highestNum = tempArr[i];
                    highestNumIndex = i;
                }
                // checking for duplicates
                int person1 = 0; // the original highest
                int person2 = 0; // the equal
                if (highestNum == tempArr[i]) {
                    do {
                        person1 = Random.Range(1, 20);
                        person2 = Random.Range(1, 20);

                        if (person1 < person2) {
                            highestNum = tempArr[i];
                            highestNumIndex = i;
                        }
                    } while (person1 == person2);
                }
            }
            tempArr[highestNumIndex] = -21;

            // if one of those initiatives is from the player characters
            if (highestNumIndex == 0 && !initiativeNames.Contains("Raza")) {
                initiativeNames[j] = "Raza";
                gameObjectsInCombat[j] = GameObject.Find("Raza");
            }
            else if (highestNumIndex == 1 && !initiativeNames.Contains("Dorne")) {
                initiativeNames[j] = "Dorne";
                gameObjectsInCombat[j] = GameObject.Find("Dorne");
            }
            else if (highestNumIndex == 2 && !initiativeNames.Contains("Smithson")) {
                initiativeNames[j] = "Smithson";
                gameObjectsInCombat[j] = GameObject.Find("Smithson");
            }
            else if (highestNumIndex == 3 && !initiativeNames.Contains("Zor")) {
                initiativeNames[j] = "Zor";
                gameObjectsInCombat[j] = GameObject.Find("Zor");
            }
            // if one of those inititaives is from the enemies
            if (highestNumIndex == 4 && e1 != null) {
                initiativeNames[j] = e1.name;
                gameObjectsInCombat[j] = enemy1;
                enemiesInCombat.Add(enemy1);
            }
            else if (highestNumIndex == 5 && e2 != null) {
                initiativeNames[j] = e2.name;
                gameObjectsInCombat[j] = enemy2;
                enemiesInCombat.Add(enemy2);
            }
            else if (highestNumIndex == 6 && e3 != null) {
                initiativeNames[j] = e3.name;
                gameObjectsInCombat[j] = enemy3;
                enemiesInCombat.Add(enemy3);
            }
            else if (highestNumIndex == 7 && e4 != null) {
                initiativeNames[j] = e4.name;
                gameObjectsInCombat[j] = enemy4;
                enemiesInCombat.Add(enemy4);
            }

            // reset variables
            highestNum = -20;
            highestNumIndex = -1;
        }

        // if the enemiesincombat doesn't have the enemy gameobject, disable it
        if (!enemiesInCombat.Contains(enemy1)) {
            enemy1.SetActive(false);
            hM.enemy1Healthbar.SetActive(false);
        }
        if (!enemiesInCombat.Contains(enemy2)) {
            enemy2.SetActive(false);
            hM.enemy2Healthbar.SetActive(false);
        }
        if (!enemiesInCombat.Contains(enemy3)) {
            enemy3.SetActive(false);
            hM.enemy3Healthbar.SetActive(false);
        }
        if (!enemiesInCombat.Contains(enemy4)) {
            enemy4.SetActive(false);
            hM.enemy4Healthbar.SetActive(false);
        }
    }

    // method to get the specified enemy from the overworld enemy \\
    private void getSpecifiedEnemy() {
        Debug.Log("Loading Specified Enemy");
        SpecifiedEnemyData data = SaveSystem.LoadSpecifiedEnemy();
        if (data != null) {
            specifiedEnemy = data.specifiedEnemy;
        }
    }
}
