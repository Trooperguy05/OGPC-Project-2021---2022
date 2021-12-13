using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class PlayerActions : MonoBehaviour
{
    // character specific abilities
    public bool Deadeye = false;
    public GameObject Mark;
    public bool Gamble;

    // targeting system variable
    private TargetingSystem ts;

    // combat manager
    private CombatManager cm;
    // party stats manager
    private PartyStats pS;

    // variable that tells the combatmanager if the player is done
    // with a character's turn
    public bool charDone = false;

    // caching
    void Start() {
        // create a new targeting system
        ts = new TargetingSystem();
        // get the combat manager script
        cm = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        // get the party stats manager
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
    }

    /////     Help Methods (makes life easier)     \\\\\
    // get the enemy creator object based on name \\
    EnemyCreator getEnemy(string name) {
        if (name == "Enemy1") {
            return cm.e1;
        }
        else if (name == "Enemy2") {
            return cm.e2;
        }
        else if (name == "Enemy3") {
            return cm.e3;
        }
        else if (name == "Enemy4") {
            return cm.e4;
        }
        return null;
    }
    //////  Character: Raza's Actions   \\\\\
    /// Action Wrappers \\\
    /// Actions \\\
    //  basic shot  \\
    public void razaFire(){
        // define base damage
        int dmg = 30;
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        // act on target
        // increases damage on marked targets
        if (target == Mark){
            dmg += 20;
        }
        // multiplies damage by 2.5 and guarantees hit if deadeye is active
        if (Deadeye){
            dmg = dmg * 2 + dmg / 2;
        }
        else{
            // Checks if trick shot is active
            if (Gamble){
                dmg *= 2;
                int chance = Random.Range(1,2);
                if (chance == 2){
                    enemy.health -= dmg;
                    Debug.Log(enemy.name + " health: " + enemy.health);
                }
                dmg = 30;
                Gamble = false;
                Deadeye = false;

            }
            else{
                // rolls normal attack chance
                int chanceToHit = Random.Range(1, 100);
                if (chanceToHit <= 90 || Deadeye) {
                    enemy.health -= dmg;
                    Debug.Log(enemy.name + " health: " + enemy.health);
                    dmg = 30;
                    Deadeye = false;
                }
            }
        }
    }
    // deadeye \\
    public void razaAim(){
        if (!Deadeye) {
            Deadeye = true;
        }
    }
    //  mark   \\
    public void razaMark(){
        GameObject target = ts.target;
        Mark = target;
    }
    // trick shot \\
    public void razaGamble(){
        if (!Gamble) {
            Gamble = true;
        }
    }
    
    /////   Character: Dorne's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_dorneStrike() {
        StartCoroutine(ts.waitForClick(dorneStrike));
    }
    public void execute_dorneSpeedUp() {
        dorneSpeedUp();
    }
    /// Actions \\\
    // basic strike action \\
    public void dorneStrike() {
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        // act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90) {
            enemy.health -= 40;
            Debug.Log(enemy.name + " health: " + enemy.health);
        }
    }
    //  Arcane Counter  \\
    public void dorneMage(){
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        // act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90) {
            enemy.health -= 25;
            Debug.Log(enemy.name + " health: " + enemy.health);
            // reduces enemy mana
            enemy.mana -= 35;
            if (enemy.mana < 0) {
                enemy.mana = 0;
            } 
        }
    }
    //  Cavalier Charge \\
    public void dorneCharge(){
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        int chanceToHit = Random.Range(1, 100);
        // act on target
        if (chanceToHit <= 90) {
            //randomize damage on charge
            enemy.health -= Random.Range(1, 70);
            enemy.health -= Random.Range(1, 70);
            enemy.health -= Random.Range(1, 70);
            Debug.Log(enemy.name + " health: " + enemy.health);
            //subtract Random.Range(1, 30) to Dorne
            pS.char2HP -= Random.Range(1,30);
        } 
    }
    //  Tighten Harness \\
    // Will increase Dorne's dex/speed once by 2 when initiative is reprogrammed
    public void dorneSpeedUp() {
        // increases dorne's speed in initiative
        cm.initiativeCount[1] = cm.initiativeCount[1] + 2;
        // then resort the initiative order
        cm.sortInitiative(cm.initiativeCount);
    }
    
    /////   Character: Smithson's Actions   \\\\\
    /// Action Wrappers \\\
    //  Bony Grasp  \\
    public void smithsonGrab(){
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        // act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90) {
            if (enemy.health < enemy.healthMax / 2){
                enemy.health -= 35;
            }
            else{
                enemy.health -= 20;
                Debug.Log(enemy.name + " health: " + enemy.health);
            }
        }
    }

    //  Siphon Life \\
    // Deals 50 damage, costs 15 mana, heals Smithson for 20, heals for 30 if enemy dies

}
