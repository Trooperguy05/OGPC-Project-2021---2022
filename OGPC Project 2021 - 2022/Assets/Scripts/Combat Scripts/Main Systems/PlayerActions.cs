using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class PlayerActions : MonoBehaviour
{
    // character specific abilities
    // Raza
    public bool Deadeye = false;
    public GameObject Mark;
    public int markTurn; // turn that player marked an enemy
    public bool Gamble;
    // Zor
    public bool Enraged = false;
    public int enragedTurn; // turn that player enraged
    public int ZorDamage = 60;
    public int ZorToHit = 80;

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

    // update the player character specific variables based on round number \\
    public void updatePCVariables() {
        // reset Raza's mark if the durability runs out
        if (Mark != null) {
            if (cm.roundNum == markTurn + 2) { // number of turns the mark lasts equals the turn the player
                Mark = null;                   // marked plus the amount of turns we want the mark to last
                markTurn = 0;
            }
        }
        // reset Zor's enraged if durability runs out
        if (Enraged) {
            if (cm.roundNum == enragedTurn + 2) { // number of turns the enrage lasts equals the turn the player
                Enraged = false;                  // enrages plus the amount of turns we want the enrage to last
                enragedTurn = 0;
            }
        }
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
    // Automatically triggers a critical hit and guarantees an attack hits
    // unless trick shot is active
    public void razaAim(){
        if (!Deadeye) {
            Deadeye = true;
        }
    }
    //  mark   \\
    // not finished
    public void razaMark(){
        GameObject target = ts.target;
        Mark = target;
        markTurn = cm.roundNum;
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
    // A basic attack\\
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
    // Lowers the mana of the target (though this applies to few enemies).
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
    // Deals a random amount of damage 3 times over to one enemy, while
    // doing a lower amount of random damage to Dorne.
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
    // Will increase Dorne's initiative by 2
    public void dorneSpeedUp() {
        // increases dorne's speed in initiative
        cm.initiativeCount[1] = cm.initiativeCount[1] + 2;
        // then resort the initiative order
        cm.sortInitiative(cm.initiativeCount);
    }
    
    /////   Character: Smithson's Actions   \\\\\
    /// Action Wrappers \\\
    // Actions \\
    //  Bony Grasp  \\
    // Basic attack dealing 35 damage, doing an extra 20 if that enemy is
    // below half of its maximum health.
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
    // Deals 50 damage, heals Smithson for 20 plus an additional 10 if the
    // enemy dies
    public void smithsonSteal(){
        //wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        //act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90 && pS.char3Mana >= 15){
            // deals damage and heals caster
            enemy.health -= 50;
            pS.char3HP += 20;
            pS.char3Mana -= 15;

            // if enemy is killed, heal user
            if (enemy.health <= 0){
                pS.char3HP += 10;
            }
        } 
        else {
            // spell wiff effect
        }
    }

   // Chill of the Grave \\
   // Targets all enemies, dealing 25 damage and reducing their initiative 
   // count by 1
    public void smithsonAOE(){
        // check if necessary mana remains
        if (pS.char3Mana >= 40){
            // subtract mana
            pS.char3Mana -= 40;
            // Targets all enenies, dealing damage and reducing their initiative order
            List<GameObject> targets = ts.targetList;
            if (cm.e1 != null){
                cm.e1.health -= 25;
                cm.initiativeCount[4] -= 1;
            }
            if (cm.e2 != null){
                cm.e2.health -= 25;
                cm.initiativeCount[5] -= 1;
            }
            if (cm.e3 != null){
                cm.e3.health -= 25;
                cm.initiativeCount[6] -= 1;
            }
            if (cm.e4 != null){
                cm.e4.health -= 25;
                cm.initiativeCount[7] -= 1;
            }
            cm.sortInitiative(cm.initiativeCount);
        }
    }

    // Clean Wounds \\
    // Basic healing spell, heals for 35.
    public void smithsonHeal(){
        // check for mana
        if (pS.char3Mana >= 10){
            pS.char3Mana -= 10;
                GameObject target = ts.target;
                if (target.name == "Raza"){
                    pS.char1HP += 35;
                }
                else if (target.name == "Dorne"){
                    pS.char2HP += 35;
                }
                else if (target.name == "Smithson"){
                    pS.char3HP += 35;
                }
                else if (target.name == "Zor"){
                    pS.char4HP += 35;
                }
            
        }
    }
    /////   Character: Zor's Actions   \\\\\
    /// Action Wrappers \\\
    // Actions \\
    // Cleave \\
    // Deals damage to a single enemy, increases in damage but decreases 
    //in accuracy per hit if rage is active
    public void zorCleave(){
        // target enemy
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        int hitChance = 90;
        int chanceToHit = Random.Range(1, 100);
        if (Enraged){
            hitChance = ZorToHit;
        } else {
            ZorDamage = 60;
            hitChance = 90;
        }
        //chance to hit
        if (chanceToHit <= hitChance){
            enemy.health -= ZorDamage;
            ZorDamage += 10;
            ZorToHit -= 5;
        }
    }

    // Tempestuous Fury \\
    // Induces rage, which allows exclusively for the use of cleave but increases damage with each use.
    public void zorAngy(){
        //can be toggled
        Enraged = !Enraged;
        if (Enraged) {
            enragedTurn = cm.roundNum;
        }
        else {
            enragedTurn = 0;
        }
    }

    // Barbaric Bolt \\
    // Targets two enemies, dealing 25 damage each
    public void zorZap(){
        if (!Enraged){
            //chance to hit
            int chanceToHit = Random.Range(1, 100);
            if (chanceToHit <= 90) {
                for(int i = 0; i < 2; i++){
                    //hits two enemies
                    getEnemy(ts.targetList[i].name).health -= 25;
                }
            }
        }
    }

    // Hurricane \\
    // General attack targeting all enemies, dealing 35 damage to all.
    public void zorAOE(){
        if (!Enraged){
            // Targets everyone
            List<GameObject> targets = ts.targetList;
            if (cm.e1 != null){
                cm.e1.health -= 35;
            }
            if (cm.e2 != null){
                cm.e2.health -= 35;
            }
            if (cm.e3 != null){
                cm.e3.health -= 35;
            }
            if (cm.e4 != null){
                cm.e4.health -= 35;
            }
        }
    }
}
