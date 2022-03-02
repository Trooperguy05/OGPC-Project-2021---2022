using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class PlayerActions : MonoBehaviour
{
    // character specific abilities\
    [Header("Character Ability Bools")]
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

    // animators for the player characters \\
    [Header("PC Animators")]
    public Animator razaAnimator;
    public Animator dorneAnimator;
    public Animator smithsonAnimator;
    public Animator zorAnimator;

    // other scripts \\
    private TargetingSystem ts;
    private CombatManager cm;
    private PartyStats pS;
    private HealthbarManager hM;
    private ManabarManager mM;
    private EnemyActions eA;
    private BattleMenuManager bMM;

    // variable that tells the combatmanager if the player is done
    // with a character's turn
    public bool charDone = false;

    // caching the other scripts \\
    void Start() {
        ts = new TargetingSystem();
        cm = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
        hM = GameObject.Find("Healthbar Manager").GetComponent<HealthbarManager>();
        mM = GameObject.Find("Healthbar Manager").GetComponent<ManabarManager>();
        eA = GetComponent<EnemyActions>();
        bMM = GameObject.Find("Battle Menu").GetComponent<BattleMenuManager>();
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

    // method that, if a pc misses, pauses the turn before passing it \\
    public IEnumerator pauseOnMiss(float duration) {
        yield return new WaitForSeconds(duration);
        charDone = true;
    }

    // method that combines the three below methods: showDealtDamage, animPlaying, & hurtEnemy into one \\
    public IEnumerator updateGameField(Animator animator, string anim, GameObject target, int dmg) {
        StartCoroutine(animPlaying(animator, anim));
        yield return new WaitForSeconds(1f);
        hurtEnemy(target, dmg);
    }

    // shows damage through the enemy healthbars \\
    public void showDealtDamage(GameObject target, int amt) {
        if (target.name == "Enemy1") {
            StartCoroutine(hM.dealDamage(hM.enemy1Slider, amt, 0.01f));
        }
        if (target.name == "Enemy2") {
            StartCoroutine(hM.dealDamage(hM.enemy2Slider, amt, 0.01f));
        }
        if (target.name == "Enemy3") {
            StartCoroutine(hM.dealDamage(hM.enemy3Slider, amt, 0.01f));
        }
        if (target.name == "Enemy4") {
            StartCoroutine(hM.dealDamage(hM.enemy4Slider, amt, 0.01f));
        }
    }

    // method that checks if the animation is done playing before passing the turn \\
    public IEnumerator animPlaying(Animator animator, string anim) {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        charDone = true;
    }

    // method that plays the hurt animation of the enemy hit \\
    public void hurtEnemy(GameObject target, int dmg) {
        showDealtDamage(target, dmg);
        if (target.name == "Enemy1") {
            eA.e1Animator.SetTrigger("hurt");
        }
        if (target.name == "Enemy2") {
            eA.e2Animator.SetTrigger("hurt");
        }
        if (target.name == "Enemy3") {
            eA.e3Animator.SetTrigger("hurt");
        }
        if (target.name == "Enemy4") {
            eA.e4Animator.SetTrigger("hurt");
        }
    }

    //////  Character: Raza's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_razaFire() {
        StartCoroutine(ts.waitForClick(razaFire));
    }
    public void execute_razaAim() {
        razaAim();
    }
    public void execute_razaMark() {
        StartCoroutine(ts.waitForClick(razaMark));
    }
    public void execute_razaGamble() {
        razaGamble();
    }
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
                // hits the gamble shot
                if (chance == 2){
                    enemy.health -= dmg;
                    // play active animation
                    razaAnimator.SetTrigger("act");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_active", target, dmg));
                    // update battle menu with action text
                    StartCoroutine(bMM.typeActionText("raza used fire!", 0.01f));
                }
                // misses the gamble shot
                else {
                    StartCoroutine(bMM.typeActionText("raza missed"), 0.01f);
                    StartCoroutine(pauseOnMiss(1f));
                }
                // reset
                dmg = 30;
                Gamble = false;
                Deadeye = false;
            }
            else{
                // rolls normal attack chance
                int chanceToHit = Random.Range(1, 100);
                if (chanceToHit <= 90 || Deadeye) {
                    enemy.health -= dmg;
                    // play active animation
                    razaAnimator.SetTrigger("act");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_active", target, dmg));
                    // update battle menu with action text
                    StartCoroutine(bMM.typeActionText("raza used fire!", 0.01f));
                    // reset
                    dmg = 30;
                    Deadeye = false;
                }
                else {
                    // update battle menu with action text
                    StartCoroutine(bMM.typeActionText("raza missed!", 0.01f));
                    StartCoroutine(pauseOnMiss(1f));
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
        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));
    }
    //  mark   \\
    // not finished
    public void razaMark(){
        GameObject target = ts.target;
        Mark = target;
        markTurn = cm.roundNum;

        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));
    }
    // trick shot \\
    public void razaGamble(){
        if (!Gamble) {
            Gamble = true;
        }
        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));
    }
    
    /////   Character: Dorne's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_dorneStrike() {
        StartCoroutine(ts.waitForClick(dorneStrike));
    }
    public void execute_dorneMage() {
        StartCoroutine(ts.waitForClick(dorneMage));
    }
    public void execute_dorneCharge() {
        StartCoroutine(ts.waitForClick(dorneCharge));
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
            showDealtDamage(target, 40);
        }
        charDone = true;
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
        charDone = true;
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
        charDone = true;
    }
    //  Tighten Harness \\
    // Will increase Dorne's initiative by 2
    public void dorneSpeedUp() {
        // increases dorne's speed in initiative
        cm.initiativeCount[1] = cm.initiativeCount[1] + 2;
        // then resort the initiative order
        cm.sortInitiative(cm.initiativeCount);
        charDone = true;
    }
    
    /////   Character: Smithson's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_smithsonGrab() {
        StartCoroutine(ts.waitForClick(smithsonGrab));
    }
    public void execute_smithsonSteal() {
        StartCoroutine(ts.waitForClick(smithsonSteal));
    }
    public void execute_smithsonAOE() {
        smithsonAOE();
    }
    public void execute_smithsonHeal() {
        StartCoroutine(ts.waitForClick(smithsonHeal));
    }
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
        // hits
        if (chanceToHit <= 90) {
            if (enemy.health < enemy.healthMax / 2){
                enemy.health -= 35;
                smithsonAnimator.SetTrigger("act");
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", target, 35));
            }
            else{
                enemy.health -= 20;
                smithsonAnimator.SetTrigger("act");
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", target, 20));
            }
            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used bony grasp!", 0.01f));
        }
        // misses
        else {
            StartCoroutine(bMM.typeActionText("smithson missed", 0.01f));
            StartCoroutine(pauseOnMiss(1f));
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
                hM.giveHeal(hM.smithsonSlider, 10, 0.01f);
            }

            // make sure the player isn't overhealed
            if (pS.char3HP > pS.char3HPMax) {
                pS.char3HP = pS.char3HPMax;
            }

            // animations
            smithsonAnimator.SetTrigger("act");
            StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", target, 50));
            StartCoroutine(hM.giveHeal(hM.smithsonSlider, 20, 0.01f));
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 15, 0.01f));

            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used siphon life!", 0.01f));
        } 
        else {
            StartCoroutine(bMM.typeActionText("smithson's spell fizzled", 0.01f));
            StartCoroutine(pauseOnMiss(1f));
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
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 40, 0.01f));
            // Targets all enenies, dealing damage and reducing their initiative order
            List<GameObject> targets = ts.targetList;
            smithsonAnimator.SetTrigger("act");
            if (cm.e1 != null){
                cm.e1.health -= 25;
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", cm.enemy1, 25));
                cm.initiativeCount[4] -= 1;
            }
            if (cm.e2 != null){
                cm.e2.health -= 25;
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", cm.enemy2, 25));
                cm.initiativeCount[5] -= 1;
            }
            if (cm.e3 != null){
                cm.e3.health -= 25;
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", cm.enemy3, 25));
                cm.initiativeCount[6] -= 1;
            }
            if (cm.e4 != null){
                cm.e4.health -= 25;
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", cm.enemy4, 25));
                cm.initiativeCount[7] -= 1;
            }
            cm.sortInitiative(cm.initiativeCount);

            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used chill of the grave!", 0.01f));
        }
        // out of mana
        else {
            StartCoroutine(bMM.typeActionText("smithson's spell fizzled", 0.01f));
            StartCoroutine(pauseOnMiss(1f));
        }
    }

    // Clean Wounds \\
    // Basic healing spell, heals for 35.
    public void smithsonHeal(){
        // check for mana
        if (pS.char3Mana >= 10){
            pS.char3Mana -= 10;
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 10, 0.01f));
            GameObject target = ts.target;
            if (target.name == "Raza"){
                pS.char1HP += 35;
                StartCoroutine(hM.giveHeal(hM.razaSlider, 35, 0.01f));
                if (pS.char1HP > pS.char1HPMax) {
                    pS.char1HP = pS.char1HPMax;
                }
            }
            else if (target.name == "Dorne"){
                pS.char2HP += 35;
                StartCoroutine(hM.giveHeal(hM.dorneSlider, 35, 0.01f));
                if (pS.char2HP > pS.char2HPMax) {
                    pS.char2HP = pS.char2HPMax;
                }
            }
            else if (target.name == "Smithson"){
                pS.char3HP += 35;
                StartCoroutine(hM.giveHeal(hM.smithsonSlider, 35, 0.01f));
                if (pS.char3HP > pS.char3HPMax) {
                    pS.char3HP = pS.char3HPMax;
                }
            }
            else if (target.name == "Zor"){
                pS.char4HP += 35;
                StartCoroutine(hM.giveHeal(hM.zorSlider, 35, 0.01f));
                if (pS.char4HP > pS.char4HPMax) {
                    pS.char4HP = pS.char4HPMax;
                }
            }
            // animation
            smithsonAnimator.SetTrigger("act");
            StartCoroutine(animPlaying(smithsonAnimator, "smithsonCombat_active"));

            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used clean wounds!", 0.01f));     
        }
        // is out of mana
        else {
            StartCoroutine(bMM.typeActionText("smithson's spell fizzles", 0.01f));
            StartCoroutine(pauseOnMiss(1f));
        }
    }
    /////   Character: Zor's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_zorCleave() {
        StartCoroutine(ts.waitForClick(zorCleave));
    }
    public void execute_zorAngy() {
        zorAngy();
    }
    public void execute_zorZap() {
        StartCoroutine(ts.waitForClick(zorZap, 2));
    }
    public void execute_zorAOE() {
        zorAOE();
    }
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
            showDealtDamage(target, ZorDamage);
            ZorDamage += 10;
            ZorToHit -= 5;
        }
        charDone = true;
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
        charDone = true;
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
                    showDealtDamage(ts.targetList[i], 25);
                }
            }
        }
        charDone = true;
    }

    // Hurricane \\
    // General attack targeting all enemies, dealing 35 damage to all.
    public void zorAOE(){
        if (!Enraged){
            // Targets everyone
            List<GameObject> targets = ts.targetList;
            if (cm.e1 != null){
                cm.e1.health -= 35;
                showDealtDamage(cm.enemy1, 35);
            }
            if (cm.e2 != null){
                cm.e2.health -= 35;
                showDealtDamage(cm.enemy2, 35);
            }
            if (cm.e3 != null){
                cm.e3.health -= 35;
                showDealtDamage(cm.enemy3, 35);
            }
            if (cm.e4 != null){
                cm.e4.health -= 35;
                showDealtDamage(cm.enemy4, 35);
            }
        }
        charDone = true;
    }
}
