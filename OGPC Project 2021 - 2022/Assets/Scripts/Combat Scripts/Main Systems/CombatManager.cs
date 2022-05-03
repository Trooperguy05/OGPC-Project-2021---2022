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
    private bool tookChoice = false;

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
    // combat enemy scripts
    public CombatEnemy e1combat;
    public CombatEnemy e2combat;
    public CombatEnemy e3combat;
    public CombatEnemy e4combat;
    // enemy field slots
    private int enemySlotsLeft = 4;
    // specified enemies
    private int specifiedEnemy;
    [Header("Other Scripts")]
    // party stats script
    public PartyStats pS;
    public PlayerProgress pP;
    // script for enemy formation
    public EnemyFormation eF;
    // script for turn indicator
    public turnIndicator tI;
    // script for healthbar manager
    public HealthbarManager hM;
    public ManabarManager mM;
    // action scripts to monitor who's done what \\
    public PlayerActions playerActions;
    public EnemyActions enemyActions;
    // status manager
    public StatusManager sM;

    // load the party stats when the player enters combat
    void Start() {
        FindObjectOfType<PartyStats>().LoadData();
        pP.loadPlayerData();

        // load specified enemy
        getSpecifiedEnemy();

        // grabbing the enemy objects
        e1combat = enemy1.GetComponent<CombatEnemy>();
        e2combat = enemy2.GetComponent<CombatEnemy>();
        e3combat = enemy3.GetComponent<CombatEnemy>();
        e4combat = enemy4.GetComponent<CombatEnemy>();
        e1 = e1combat.eOb;
        e2 = e2combat.eOb;
        e3 = e3combat.eOb;
        e4 = e4combat.eOb;

        // grabbing the action scripts
        playerActions = GameObject.Find("Action Manager").GetComponent<PlayerActions>();

        // grabbing the enemy formation script
        eF = GetComponent<EnemyFormation>();

        // grabbing the turn indicator script
        tI = GetComponent<turnIndicator>();
    }

    void Update() {
        if (!combatStarted) {
            // update healthbar and manabar
            hM.updateHealthbarValues();
            mM.smithsonManabarSlider.value = pS.char3Mana;
            // start combat
            startCombat();
            sortInitiative(initiativeCount);
            eF.organizeField();
            tI.updateIndicator();
        }

        ///   Turn-Based Combat   \\\
        // if it is one of the player characters' turn \\
        if (initiativeNames[initiativeIndex] == "Raza") {
            if (pS.char1HP <= 0 || roundNum == playerActions.razaStunRound) {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        else if (initiativeNames[initiativeIndex] == "Dorne") {
            if (pS.char2HP <= 0 || roundNum == playerActions.dorneStunRound) {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        else if (initiativeNames[initiativeIndex] == "Smithson") {
            if (pS.char3HP <= 0 || roundNum == playerActions.smithsonStunRound) {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        else if (initiativeNames[initiativeIndex] == "Zor") {
            if (pS.char4HP <= 0 || roundNum == playerActions.zorStunRound) {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (playerActions.charDone) {
                playerActions.charDone = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if it is one of the enemy's turns \\
        // if the enemy is a scorpion
        if (initiativeNames[initiativeIndex] == "Scorpion") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                int choice = Random.Range(1, 3);
                if (!tookChoice) {
                    if (choice == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.scorpSting());

                    }
                    else {
                        tookChoice = true;
                        StartCoroutine(enemyActions.scorpPinch());
                    }      
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();     
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is a mummy
        else if (initiativeNames[initiativeIndex] == "Mummy") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    tookChoice = true;
                    StartCoroutine(enemyActions.mummyWalkin());
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();  
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is a crocodile
        else if (initiativeNames[initiativeIndex] == "Crocodile") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    int choice = Random.Range(1, 3);
                    if (choice == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.crocBite());
                    }
                    else if (choice == 2) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.crocSpin());
                    }
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is the snake
        else if (initiativeNames[initiativeIndex] == "Snake") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    int choice = Random.Range(1, 3);
                    if (choice == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.snakeConstrict());
                    }
                    else if (choice == 2) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.snakeBite());
                    }
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is a slime
        else if (initiativeNames[initiativeIndex] == "Slime") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    tookChoice = true;
                    StartCoroutine(enemyActions.slimeEat());
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is a spider
        else if (initiativeNames[initiativeIndex] == "Spider") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    int chance = Random.Range(1, 3);
                    if (chance == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.spiderBite());
                    }
                    else if (chance == 2) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.spiderWeb());
                    }
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is the worm (miniboss)
        else if (initiativeNames[initiativeIndex] == "Worm") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    int choice = Random.Range(1, 3);
                    if (choice == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.sandwormHole());
                    }
                    else if (choice == 2) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.sandwormBite());
                    }
                }  
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }
        // if enemy is a man trap (miniboss)
        else if (initiativeNames[initiativeIndex] == "Man Trap") {
            if (gameObjectsInCombat[initiativeIndex].GetComponent<CombatEnemy>().eOb.health > 0) {
                if (!tookChoice) {
                    int choice = Random.Range(1, 3);
                    if (choice == 1) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.trapSnap());
                    }
                    else if (choice == 2) {
                        tookChoice = true;
                        StartCoroutine(enemyActions.trapClamp());
                    }
                }
            }
            else {
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
            if (enemyActions.enemyDone) {
                enemyActions.enemyDone = false;
                tookChoice = false;
                initiativeIndex++;
                newRound();
                tI.updateIndicator();
            }
        }

        // if initiativeNames[initiativeIndex] = "", continue to next person
        if (combatStarted) {
            if (initiativeNames[initiativeIndex] == "") {
                initiativeIndex++;
                newRound();
            }
        }
    }

    // method that continues combat to the next round \\
    public void newRound() {
        // check exit conditions
        checkExitConditions();
        /// checking if it's the end of a round
        if (initiativeIndex > 7) {
            initiativeIndex = 0;
            roundNum++;
            Debug.Log("Round: " + roundNum);
            playerActions.updatePCVariables();
            tI.updateIndicator();
            // regen some mana for smithson
            StartCoroutine(mM.regenMana(mM.smithsonManabarSlider, 15, 0.01f));
            // update statuses
            sM.statusUpdate();
        }
    }

    // method that checks for the exit conditions for combat
    // player wins or loses
    public void checkExitConditions() {
        /// checking win/lose conditions
        // if player loses
        if (pS.char1HP <= 0 && pS.char2HP <= 0 && pS.char3HP <= 0 && pS.char4HP <= 0) {
            // save
            SaveSystem.SavePartyStats(pS);
            CombatReport cR = GetComponent<CombatReport>();
            cR.wonLastCombat = true;
            SaveSystem.saveCombatReport(cR);
            // return to the overworld
            SceneLoader.changeScene = true;
        }
        // if player wins
        int numCheck = 0;
        if (e1 != null) {
            if (e1combat.eOb.health <= 0) {
                numCheck++;
            }
        }
        if (e2 != null) {
            if (e2combat.eOb.health <= 0) {
                numCheck++;
            }
        }
        if (e3 != null) {
            if (e3combat.eOb.health <= 0) {
                numCheck++;
            }
        }
        if (e4 != null) {
            if (e4combat.eOb.health <= 0) {
                numCheck++;
            }
        }
        if (numCheck == enemiesInCombat.Count) {
            // save
            SaveSystem.SavePartyStats(pS);
            CombatReport cR = GetComponent<CombatReport>();
            cR.wonLastCombat = true;
            SaveSystem.saveCombatReport(cR);
            // return to overworld
            SceneManager.LoadScene(1);
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

        // if there is a specified enemy \\
        if (specifiedEnemy != 0) {
            e1 = new EnemyCreator(specifiedEnemy);
            enemySlotsLeft -= e1.size;
            initiativeCount[4] = Random.Range(1, 21) + e1.dexterity;
            hM.enemy1Slider.maxValue = e1.healthMax;
            hM.enemy1Slider.value = hM.enemy1Slider.maxValue;
            e1combat.eOb = e1;
            e1combat.updateSprite();
        }

        // create enemies based on size and space left \\
        int i = 0;
        while (enemySlotsLeft > 0) {
            if (e1 == null) {
                // different enemies based on player biome
                if (pP.playerBiome == PlayerProgress.Biome.desert) {
                    int chance = Random.Range(1, 3);
                    e1 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.swamp) {
                    int chance = Random.Range(3, 5);
                    e1 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.forest) {
                    int chance = Random.Range(5, 7);
                    e1 = new EnemyCreator(chance);
                }
                // create the enemy
                if (enemySlotsLeft >= e1.size) {
                    enemySlotsLeft -= e1.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e1.dexterity;   
                    i++;
                    hM.enemy1Slider.maxValue = e1.healthMax;
                    hM.enemy1Slider.value = hM.enemy1Slider.maxValue;
                    e1combat.eOb = e1;
                    e1combat.updateSprite(); 
                }
                else {
                    e1 = null;
                }
            }
            else if (e2 == null) {
                // different enemies based on player biome
                if (pP.playerBiome == PlayerProgress.Biome.desert) {
                    int chance = Random.Range(1, 3);
                    e2 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.swamp) {
                    int chance = Random.Range(3, 5);
                    e2 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.forest) {
                    int chance = Random.Range(5, 7);
                    e2 = new EnemyCreator(chance);
                }
                //e2 = new EnemyCreator();
                // create the new enemy
                if (enemySlotsLeft >= e2.size) {
                    enemySlotsLeft -= e2.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e2.dexterity;
                    i++;
                    hM.enemy2Slider.maxValue = e2.healthMax;
                    hM.enemy2Slider.value = hM.enemy2Slider.maxValue;
                    e2combat.eOb = e2;
                    e2combat.updateSprite();
                }
                else {
                    e2 = null;
                }
            }
            else if (e3 == null) {
                // different enemies based on player biome
                if (pP.playerBiome == PlayerProgress.Biome.desert) {
                    int chance = Random.Range(1, 3);
                    e3 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.swamp) {
                    int chance = Random.Range(3, 5);
                    e3 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.forest) {
                    int chance = Random.Range(5, 7);
                    e3 = new EnemyCreator(chance);
                }
                //e3 = new EnemyCreator();
                // create new enemy
                if (enemySlotsLeft >= e3.size) {
                    enemySlotsLeft -= e3.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e3.dexterity;
                    i++;
                    hM.enemy3Slider.maxValue = e3.healthMax;
                    hM.enemy3Slider.value = hM.enemy3Slider.maxValue;
                    e3combat.eOb = e3;
                    e3combat.updateSprite();
                }
                else {
                    e3 = null;
                }
            }
            else if (e4 == null) {
                // different enemies based on player biome
                if (pP.playerBiome == PlayerProgress.Biome.desert) {
                    int chance = Random.Range(1, 3);
                    e4 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.swamp) {
                    int chance = Random.Range(3, 5);
                    e4 = new EnemyCreator(chance);
                }
                else if (pP.playerBiome == PlayerProgress.Biome.forest) {
                    int chance = Random.Range(5, 7);
                    e4 = new EnemyCreator(chance);
                }
                //e4 = new EnemyCreator();
                // create new enemy
                if (enemySlotsLeft >= e4.size) {
                    enemySlotsLeft -= e4.size;
                    initiativeCount[4+i] = Random.Range(1, 20) + e4.dexterity;
                    i++;
                    hM.enemy4Slider.maxValue = e4.healthMax;
                    hM.enemy4Slider.value = hM.enemy4Slider.maxValue;
                    e4combat.eOb = e4;
                    e4combat.updateSprite();  
                }
                else {
                    e4 = null;
                }
            }

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
        for (int i = 0; i < enemiesInCombat.Count; i++) {
            enemiesInCombat.RemoveAt(0);
            i--;
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
