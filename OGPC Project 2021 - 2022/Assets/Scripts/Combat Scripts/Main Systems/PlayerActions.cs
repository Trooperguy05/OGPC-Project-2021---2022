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

    // stunned effect if the PC critted \\
    public int razaStunRound = -1;
    public int dorneStunRound = -1;
    public int smithsonStunRound = -1;
    public int zorStunRound = -1;

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
    private VirusMeterManager vMM;

    // audio sets \\
    [Header("Audio")]
    public AudioSource AS;
    // raza audio
    public AudioClip razaFireSFX;
    public AudioClip razaAimSFX;
    public AudioClip razaMarkSFX;
    public AudioClip razaGambleSFX;
    // Dorne audio
    public AudioClip dorneStrikeSFX;
    public AudioClip dorneMageSFX;
    public AudioClip dorneChargeSFX;
    public AudioClip tightenHarnessSFX;
    // Smithson audio
    public AudioClip smithsonGrabSFX;
    public AudioClip smithsonLifeStealSFX;
    public AudioClip smithsonChillOfTheGraveSFX;
    public AudioClip smithsonCleanWoundsSFX;
    // Zor audio
    public AudioClip zorCleaveSFX;
    public AudioClip zorAngySFX;
    public AudioClip zorZapSFX;
    public AudioClip zorAOESFX;

    [Header("Other Action Variables")]
    // variable that tells the combatmanager if the player is done
    // with a character's turn
    public bool charDone = false;
    // time amt of pause when the player misses
    public float pauseWait = 1.5f;
    // amt the crit increases
    public int critIncreaseMin = 5;
    public int critIncreaseMax = 11;

    // caching the other scripts \\
    void Start() {
        ts = new TargetingSystem();
        cm = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
        hM = GameObject.Find("Healthbar Manager").GetComponent<HealthbarManager>();
        mM = GameObject.Find("Healthbar Manager").GetComponent<ManabarManager>();
        vMM = GameObject.Find("Healthbar Manager").GetComponent<VirusMeterManager>();
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
    }

    // method that, if a pc misses, pauses the turn before passing it \\
    public IEnumerator pauseOnMiss(float duration) {
        yield return new WaitForSeconds(duration);
        charDone = true;
    }

    // method that combines the three below methods: showDealtDamage, animPlaying, & hurtEnemy into one \\
    public IEnumerator updateGameField(Animator animator, string anim, GameObject target, int dmg) {
        StartCoroutine(animPlaying(animator, anim));
        hurtEnemy(target, dmg);
        yield return null;
    }

    // shows damage through the enemy healthbars \\
    public void showDealtDamage(GameObject target, int amt) {
        if (target.name == "Enemy1") {
            StartCoroutine(hM.dealDamage(hM.enemy1Slider, amt, 0));
        }
        if (target.name == "Enemy2") {
            StartCoroutine(hM.dealDamage(hM.enemy2Slider, amt, 0));
        }
        if (target.name == "Enemy3") {
            StartCoroutine(hM.dealDamage(hM.enemy3Slider, amt, 0));
        }
        if (target.name == "Enemy4") {
            StartCoroutine(hM.dealDamage(hM.enemy4Slider, amt, 0));
        }
    }

    // method that checks if the animation is done playing before passing the turn \\
    public IEnumerator animPlaying(Animator animator, string anim) {
        // wait for animation to finish
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        // pass the turn
        yield return new WaitForSeconds(1f);
        charDone = true;
        mM.updateManabar();
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
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(razaFire, "Enemy"));
    }
    public void execute_razaAim() {
        razaAim();
    }
    public void execute_razaMark() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(razaMark, "Enemy"));
    }
    public void execute_razaGamble() {
        razaGamble();
    }
    /// Actions \\\
    //  basic shot  \\
    public void razaFire(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.razaSlider, "raza"));
        bool crit = false;
        
        // define base damage
        int dmg = 30;

        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);

        /// damage mutlipliers
        // increases damage on marked targets
        if (target == Mark){
            dmg += 20;
        }
        // multiplies damage by 2.5 and guarantees hit if deadeye is active
        if (Deadeye){
            dmg = dmg * 2 + dmg / 2;
        }
        // if gamble, double the damage
        if (Gamble) {
            dmg *= 2;
        }
        // check virus meter for "crit"
        if (pS.char1VMeter == 100) {
            dmg = (int) Mathf.Pow(dmg, 2.5f);
            StartCoroutine(vMM.updateMeter(-100, vMM.razaSlider, "raza"));
            crit = true;
            razaStunRound = cm.roundNum+1;
        }

        /// act on target
        // Checks if trick shot is active
        if (Gamble){
            int chance = Random.Range(1,3);
            // hits the gamble shot
            if (chance == 2){
                enemy.health -= dmg;
                /// play active animation
                // for non-crit
                if (!crit) {
                    razaAnimator.SetTrigger("act");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_active", target, dmg));                   
                }
                // for crit
                else {
                    razaAnimator.SetTrigger("crit");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_crit", target, dmg));
                }
                // update battle menu with action text
                StartCoroutine(bMM.typeActionText("raza used fire!", 0.01f));
            }
            // misses the gamble shot
            else {
                StartCoroutine(bMM.typeActionText("raza missed", 0.01f));
                StartCoroutine(pauseOnMiss(pauseWait));
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
                /// play active animation
                // for non-crit
                if (!crit) {
                    razaAnimator.SetTrigger("act");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_active", target, dmg));                   
                }
                // for crit
                else {
                    razaAnimator.SetTrigger("crit");
                    StartCoroutine(updateGameField(razaAnimator, "razaCombat_crit", target, dmg));
                }
                // update battle menu with action text
                StartCoroutine(bMM.typeActionText("raza used fire!", 0.01f));
                // reset
                dmg = 30;
                Deadeye = false;
            }
            else {
                // update battle menu with action text
                StartCoroutine(bMM.typeActionText("raza missed!", 0.01f));
                StartCoroutine(pauseOnMiss(pauseWait));
            }
        }
        AS.PlayOneShot(razaFireSFX, 1);
    }
    // deadeye \\
    // Automatically triggers a critical hit and guarantees an attack hits
    // unless trick shot is active
    public void razaAim(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.razaSlider, "raza"));

        // update deadeye
        if (!Deadeye) {
            Deadeye = true;
        }
        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));

        // update battle menu with action text
        StartCoroutine(bMM.typeActionText("raza used deadeye!", 0.01f));
        AS.PlayOneShot(razaAimSFX, 1);
    }
    //  mark   \\
    // not finished
    public void razaMark(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.razaSlider, "raza"));

        // create the mark
        GameObject target = ts.target;
        Mark = target;
        markTurn = cm.roundNum;

        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));
        // update battle menu with action text
        StartCoroutine(bMM.typeActionText("raza used mark!", 0.01f));
        AS.PlayOneShot(razaMarkSFX, 1);
    }
    // trick shot \\
    public void razaGamble(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.razaSlider, "raza"));

        // update gamble
        if (!Gamble) {
            Gamble = true;
        }

        // play active animation
        razaAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(razaAnimator, "razaCombat_active"));

        // update battle menu with action text
        StartCoroutine(bMM.typeActionText("raza used gamble!", 0.01f));
        AS.PlayOneShot(razaGambleSFX, 1);
    }
    
    /////   Character: Dorne's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_dorneStrike() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(dorneStrike, "Enemy"));
    }
    public void execute_dorneMage() {
        StartCoroutine(bMM.typeHelperActionText("select two enemies", 0.01f));
        StartCoroutine(ts.waitForClick(dorneRoulette, 2, "Enemy"));
    }
    public void execute_dorneCharge() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(dorneCharge, "Enemy"));
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
        int dmg = 40;

        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.dorneSlider, "dorne"));
        bool crit = false;
        // check virus meter for "crit"
        if (pS.char2VMeter == 100) {
            dmg = (int) Mathf.Pow(dmg, 2.5f);
            StartCoroutine(vMM.updateMeter(-100, vMM.dorneSlider, "dorne"));
            crit = true;
            dorneStunRound = cm.roundNum+1;
        }

        // act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90) {
            if (crit) {
                enemy.health -= dmg;
                // play crit animation
                dorneAnimator.SetTrigger("crit");
                StartCoroutine(updateGameField(dorneAnimator, "dorneCombat_crit", target, dmg));
            }
            else {
                enemy.health -= dmg;
                // play active animation
                dorneAnimator.SetTrigger("act");
                StartCoroutine(updateGameField(dorneAnimator, "dorneCombat_active", target, dmg));          
            }
            // action text
            StartCoroutine(bMM.typeActionText("dorne used strike!", 0.01f));
        }
        // on miss
        else {
            StartCoroutine(bMM.typeActionText("dorne missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(dorneStrikeSFX, 1);
    }
    //  Roulette  \\
    // Deals a random amount of damage to two enemies, though this deals no damage to Dorne
    public void dorneRoulette()
    {
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.dorneSlider, "dorne"));

        //chance to hit
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90)
        {
            for (int i = 0; i < 2; i++)
            {
                //hits two enemies
                int dmg = Random.Range(20, 51);
                getEnemy(ts.targetList[i].name).health -= dmg;
                hurtEnemy(ts.targetList[i], dmg);
            }
            // play animation
            dorneAnimator.SetTrigger("act");
            StartCoroutine(animPlaying(dorneAnimator, "dorneCombat_active"));
            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("dorne used roulette!", 0.01f));
        }
        // on miss
        else
        {
            StartCoroutine(bMM.typeActionText("dorne missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }


        AS.PlayOneShot(dorneMageSFX, 1);
    }
    
    //  Cavalier Charge \\
    // Deals a random amount of damage 3 times over to one enemy, while
    // doing a lower amount of random damage to Dorne.
    public void dorneCharge(){
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        int chanceToHit = Random.Range(1, 100);

        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.dorneSlider, "dorne"));

        // act on target
        if (chanceToHit <= 90) {
            //randomize damage on charge
            int dmg = (Random.Range(1, 70) + Random.Range(1, 70) + Random.Range(1, 70));
            int selfDmg = Random.Range(1, 30);
            enemy.health -= dmg;
            pS.char2HP -= selfDmg;
            // animation
            dorneAnimator.SetTrigger("act");
            StartCoroutine(hM.dealDamage(hM.dorneSlider, selfDmg, 0.01f));
            StartCoroutine(updateGameField(dorneAnimator, "dorneCombat_active", target, dmg));
            // action text
            StartCoroutine(bMM.typeActionText("dorne used charge!", 0.01f));
        }
        // on miss
        else {
            StartCoroutine(bMM.typeActionText("dorne missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(dorneChargeSFX, 1);
    }
    //  Tighten Harness \\
    // Will increase Dorne's initiative by 2
    public void dorneSpeedUp() {
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.dorneSlider, "dorne"));

        // increases dorne's speed in initiative
        cm.initiativeCount[1] = cm.initiativeCount[1] + 2;
        
        // play animation
        dorneAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(dorneAnimator, "dorneCombat_active"));
        // action text
        StartCoroutine(bMM.typeActionText("dorne used tighten harness!", 0.01f));
        AS.PlayOneShot(tightenHarnessSFX, 1);
    }
    
    /////   Character: Smithson's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_smithsonGrab() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(smithsonGrab, "Enemy"));
    }
    public void execute_smithsonSteal() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(smithsonSteal, "Enemy"));
    }
    public void execute_smithsonAOE() {
        smithsonAOE();
    }
    public void execute_smithsonHeal() {
        StartCoroutine(bMM.typeHelperActionText("select one ally", 0.01f));
        StartCoroutine(ts.waitForClick(smithsonHeal, "Player"));
    }
    // Actions \\
    //  Bony Grasp  \\
    // Basic attack dealing 35 damage, doing an extra 20 if that enemy is
    // below half of its maximum health.
    public void smithsonGrab(){
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);

        int dmg = 20;
        if (enemy.health < enemy.healthMax/2) {
            dmg += 15;
        }
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.smithsonSlider, "smithson"));
        bool crit = false;
        // check virus meter for "crit"
        if (pS.char3VMeter == 100) {
            dmg = (int) Mathf.Pow(dmg, 2.5f);
            StartCoroutine(vMM.updateMeter(-100, vMM.smithsonSlider, "smithson"));
            crit = true;
            smithsonStunRound = cm.roundNum+1;
        }

        // act on target
        int chanceToHit = Random.Range(1, 100);
        // hits
        if (chanceToHit <= 90) {
            if (crit) {
                enemy.health -= dmg;
                smithsonAnimator.SetTrigger("crit");
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_crit", target, dmg));
            }
            else {
                enemy.health -= dmg;
                smithsonAnimator.SetTrigger("act");
                StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", target, dmg));   
            }
            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used bony grasp!", 0.01f));
        }
        // misses
        else {
            StartCoroutine(bMM.typeActionText("smithson missed", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(smithsonGrabSFX, 1);
    }

    //  Siphon Life \\
    // Deals 50 damage, heals Smithson for 20 plus an additional 10 if the
    // enemy dies
    public void smithsonSteal(){
        //wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);

        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.smithsonSlider, "smithson"));

        //act on target
        int chanceToHit = Random.Range(1, 100);
        if (chanceToHit <= 90 && pS.char3Mana >= 15){
            /// deals damage and heals caster
            int heal = 20;
            enemy.health -= 50;
            if (enemy.health <= 0) { // if the ability kills the enemy, increase the heal
                heal += 10;
            }
            pS.char3HP += heal;
            pS.char3Mana -= 35;

            // make sure the player isn't overhealed
            if (pS.char3HP > pS.char3HPMax) {
                pS.char3HP = pS.char3HPMax;
            }

            // animations
            StartCoroutine(hM.giveHeal(hM.smithsonSlider, heal, 0.01f));
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 35, 0.01f));
            smithsonAnimator.SetTrigger("act");
            StartCoroutine(updateGameField(smithsonAnimator, "smithsonCombat_active", target, 50));

            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used siphon life!", 0.01f));
        } 
        else {
            StartCoroutine(bMM.typeActionText("smithson's spell fizzled", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(smithsonLifeStealSFX, 1);
    }

   // Chill of the Grave \\
   // Targets all enemies, dealing 25 damage and reducing their initiative 
   // count by 1
    public void smithsonAOE(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.smithsonSlider, "smithson"));

        // check if necessary mana remains
        if (pS.char3Mana >= 40){
            // subtract mana
            pS.char3Mana -= 40;
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 40, 0.01f));
            // Targets all enenies, dealing damage and reducing their initiative order
            List<GameObject> targets = ts.targetList;
            if (cm.e1 != null){
                cm.e1.health -= 25;
                cm.initiativeCount[4] -= 1;
                hurtEnemy(cm.enemy1, 25);
            }
            if (cm.e2 != null){
                cm.e2.health -= 25;
                cm.initiativeCount[5] -= 1;
                hurtEnemy(cm.enemy2, 25);
            }
            if (cm.e3 != null){
                cm.e3.health -= 25;
                cm.initiativeCount[6] -= 1;
                hurtEnemy(cm.enemy3, 25);
            }
            if (cm.e4 != null){
                cm.e4.health -= 25;
                cm.initiativeCount[7] -= 1;
                hurtEnemy(cm.enemy4, 25);
            }

            // play animation
            smithsonAnimator.SetTrigger("act");
            StartCoroutine(animPlaying(smithsonAnimator, "smithsonCombat_active"));

            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("smithson used chill of the grave!", 0.01f));
        }
        // out of mana
        else {
            StartCoroutine(bMM.typeActionText("smithson's spell fizzled", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(smithsonChillOfTheGraveSFX, 1);
    }

    // Clean Wounds \\
    // Basic healing spell, heals for 35.
    public void smithsonHeal(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.smithsonSlider, "smithson"));

        // check for mana
        if (pS.char3Mana >= 10){
            pS.char3Mana -= 25;
            StartCoroutine(mM.depleteMana(mM.smithsonManabarSlider, 25, 0.01f));
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
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(smithsonCleanWoundsSFX, 1);
    }
    /////   Character: Zor's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_zorCleave() {
        StartCoroutine(bMM.typeHelperActionText("select one enemy", 0.01f));
        StartCoroutine(ts.waitForClick(zorCleave, "Enemy"));
    }
    public void execute_zorAngy() {
        zorAngy();
    }
    public void execute_zorZap() {
        StartCoroutine(bMM.typeHelperActionText("select two enemies", 0.01f));
        StartCoroutine(ts.waitForClick(zorZap, 2, "Enemy"));
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
        // enrage calcs
        if (Enraged){
            hitChance = ZorToHit;
        } else {
            ZorDamage = 60;
            hitChance = 90;
        }
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.zorSlider, "zor"));
        bool crit = false;
        // check virus meter for "crit"
        if (pS.char4VMeter == 100) {
            StartCoroutine(vMM.updateMeter(-100, vMM.zorSlider, "zor"));
            crit = true;
            zorStunRound = cm.roundNum+1;
        }
        //chance to hit
        if (chanceToHit <= hitChance){
            if (crit) {
                int dmg = (int) Mathf.Pow(ZorDamage, 2.5f);
                enemy.health -= dmg;
                // active animation
                zorAnimator.SetTrigger("crit");
                StartCoroutine(updateGameField(zorAnimator, "zorCombat_crit", target, dmg));
            }
            else {
                enemy.health -= ZorDamage;
                // animation
                zorAnimator.SetTrigger("act");
                StartCoroutine(updateGameField(zorAnimator, "zorCombat_active", target, ZorDamage));           
            }
            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("zor used cleave!", 0.01f));

            // after action things
            ZorDamage += 10;
            ZorToHit -= 5;
        }
        else {
            StartCoroutine(bMM.typeActionText("zor missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(zorCleaveSFX, 1);
    }

    // Tempestuous Fury \\
    // Induces rage, which allows exclusively for the use of cleave but increases damage with each use.
    public void zorAngy(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.zorSlider, "zor"));

        //can be toggled
        Enraged = !Enraged;

        // play active animation
        zorAnimator.SetTrigger("act");
        StartCoroutine(animPlaying(zorAnimator, "zorCombat_active"));
        // update battle menu with action text
        StartCoroutine(bMM.typeActionText("zor used rage!", 0.01f));

        // play sfx
        AS.PlayOneShot(zorAngySFX, 1);
    }

    // Barbaric Bolt \\
    // Targets two enemies, dealing 25 damage each
    public void zorZap(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.zorSlider, "zor"));

        if (!Enraged){
            //chance to hit
            int chanceToHit = Random.Range(1, 100);
            if (chanceToHit <= 90) {
                for(int i = 0; i < 2; i++){
                    //hits two enemies
                    getEnemy(ts.targetList[i].name).health -= 25;
                    hurtEnemy(ts.targetList[i], 25);
                }
                // play animation
                zorAnimator.SetTrigger("act");
                StartCoroutine(animPlaying(zorAnimator, "zorCombat_active"));
                // update battle menu with action text
                StartCoroutine(bMM.typeActionText("zor used barbaric bolt!", 0.01f));
            }
            // on miss
            else {
                StartCoroutine(bMM.typeActionText("zor missed!", 0.01f));
                StartCoroutine(pauseOnMiss(pauseWait));
            }
        }
        // if he is enraged
        else {
            StartCoroutine(bMM.typeActionText("zor is enraged! he cannot use this action!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(zorZapSFX, 1);
    }

    // Hurricane \\
    // General attack targeting all enemies, dealing 35 damage to all.
    public void zorAOE(){
        // add to virus meter
        StartCoroutine(vMM.updateMeter(Random.Range(critIncreaseMin, critIncreaseMax), vMM.zorSlider, "zor"));

        if (!Enraged){
            // Targets everyone
            List<GameObject> targets = ts.targetList;
            zorAnimator.SetTrigger("act");
            if (cm.e1 != null){
                cm.e1.health -= 35;
                hurtEnemy(cm.enemy1, 35);
            }
            if (cm.e2 != null){
                cm.e2.health -= 35;
                hurtEnemy(cm.enemy2, 35);
            }
            if (cm.e3 != null){
                cm.e3.health -= 35;
                hurtEnemy(cm.enemy3, 35);
            }
            if (cm.e4 != null){
                cm.e4.health -= 35;
                hurtEnemy(cm.enemy4, 35);
            }
            // play animation
            StartCoroutine(animPlaying(zorAnimator, "zorCombat_active"));
            // update battle menu with action text
            StartCoroutine(bMM.typeActionText("zor used hurricane!", 0.01f));
        }
        // if he is enraged
        else {
            StartCoroutine(bMM.typeActionText("zor is enraged! he cannot use this action!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(zorAOESFX, 1);
    }
}
