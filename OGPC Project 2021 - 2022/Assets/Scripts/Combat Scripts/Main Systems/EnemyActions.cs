using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyActions : MonoBehaviour
{
    [Header("Enemy Animators")]
    public Animator e1Animator;
    public Animator e2Animator;
    public Animator e3Animator;
    public Animator e4Animator;
    //party stats and combat manager
    private CombatManager cm;
    private PartyStats pS;
    private PlayerActions pA;
    private StatusManager sM;
    private HealthbarManager hM;
    private BattleMenuManager bMM;
    
    //enemy sfx variables
    public AudioSource AS;
    public AudioClip ScorpSting;
    public AudioClip ScorpSnip;
    public AudioClip MummyWalk;
    public AudioClip WormHole;
    public AudioClip WormBite;
    public AudioClip SnakeCoil;
    public AudioClip SnakeBite;
    public AudioClip CrocSpin;
    public AudioClip CrocBite;
    public AudioClip TrapSnap;
    public AudioClip TrapClamp;
    public AudioClip Slime;
    public AudioClip SpiderBite;
    public AudioClip SpiderWeb;
    public AudioClip GiantStrike;
    public AudioClip GiantStomp;

    //enemy specific variables
    [Header("Enemy Abilities")]
    public bool snakeCoil = false;
    public string snakeCoilTarget;

    // other important variables for dictating flow of combat \\
    [Header("Other Variables")]
    public bool enemyDone = false;
    public float pauseWait = 1.5f;


    //caching
    void Start()
    {
        // get the combat manager script
        cm = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        // get the party stats manager
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
        // get the status effect manager
        sM = GameObject.Find("Combat Manager").GetComponent<StatusManager>();
        // healthbar manager
        hM = GameObject.Find("Healthbar Manager").GetComponent<HealthbarManager>();
        // battle menu manager
        bMM = GameObject.Find("Battle Menu").GetComponent<BattleMenuManager>();
        // player actions
        pA = GameObject.Find("Action Manager").GetComponent<PlayerActions>();

    }

    // method that checks if the animation is done playing before passing the turn \\
    public IEnumerator animPlaying(Animator animator, string anim) {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(anim)) {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        enemyDone = true;
        hM.updateHealthbarValues();
    }

    // method that pauses the flow of combat when the enemy misses before passing the turn \\
    public IEnumerator pauseOnMiss(float wait) {
        yield return new WaitForSeconds(wait);
        enemyDone = true;
    }
 
    // method that finds the animator of the current enemy (whose turn it is) \\
    public Animator findAnimator() {
        if (cm.gameObjectsInCombat[cm.initiativeIndex].name == "Enemy1") {
            return e1Animator;
        }
        if (cm.gameObjectsInCombat[cm.initiativeIndex].name == "Enemy2") {
            return e2Animator;
        }
        if (cm.gameObjectsInCombat[cm.initiativeIndex].name == "Enemy3") {
            return e3Animator;
        }
        if (cm.gameObjectsInCombat[cm.initiativeIndex].name == "Enemy4") {
            return e4Animator;
        }
        return null;
    }

    //decides what party member is targeted by an enemy
    public string enemyHit(int dmg)
    {
        string character = "";
        // until the enemy finds a target, loop
        while (true) {
            // get a random player character
            int target = Random.Range(1, 5);

            // if raza is alive, target him
            if (target == 1 && pS.char1HP > 0) {
                character = "raza";
                // deal damage
                pS.char1HP -= dmg;
                StartCoroutine(hM.dealDamage(hM.razaSlider, dmg, 0.01f));
                pA.razaAnimator.SetTrigger("hurt");
                // exit loop
                break;
            }
            // if dorne is alive, target him
            else if (target == 2 && pS.char2HP > 0) {
                character = "dorne";
                // deal damage
                pS.char2HP -= dmg;
                StartCoroutine(hM.dealDamage(hM.dorneSlider, dmg, 0.01f));
                pA.dorneAnimator.SetTrigger("hurt");
                // exit loop
                break;
            }
            // if smithson is alive, target her
            else if (target == 3 && pS.char3HP > 0) {
                character = "smithson";
                // deal damage
                pS.char3HP -= dmg;
                StartCoroutine(hM.dealDamage(hM.smithsonSlider, dmg, 0.01f));
                pA.smithsonAnimator.SetTrigger("hurt");
                // exit loop
                break;                
            }
            // if zor is alive, target him
            else if (target == 4 && pS.char4HP > 0) {
                character = "zor";
                // deal damage
                pS.char4HP -= dmg;
                StartCoroutine(hM.dealDamage(hM.zorSlider, dmg, 0.01f));
                pA.zorAnimator.SetTrigger("hurt");
                // exit loop
                break;
            }
        }
        return character;
    }

    //variation of the enemy attack method but without a return value, as to hit all player characters
    public void enemyAll(int dmg)
    {
        pS.char1HP -= dmg;
        StartCoroutine(hM.dealDamage(hM.razaSlider, dmg, 0.01f));
        pS.char2HP -= dmg;
        StartCoroutine(hM.dealDamage(hM.dorneSlider, dmg, 0.01f));
        pS.char3HP -= dmg;
        StartCoroutine(hM.dealDamage(hM.smithsonSlider, dmg, 0.01f));
        pS.char4HP -= dmg;
        StartCoroutine(hM.dealDamage(hM.zorSlider, dmg, 0.01f));
    }

    ////////Enemy Types\\\\\\\\

    //////Desert\\\\\\

    /////Scorpion Attacks\\\\\
    ///Stinger\\\
    public IEnumerator scorpSting()
    {
        yield return new WaitForSeconds(1);
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            // damage and status effect
            string character = enemyHit(10);
            sM.statusAdd(character, "poison", 3);
            // action text
            StartCoroutine(bMM.typeActionText("scorpion used sting!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "scorpionCombat_active"));
        }
        else {
            StartCoroutine(bMM.typeActionText("scorpion missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(ScorpSting, 1);
    }

    ///Pincers\\\
    public IEnumerator scorpPinch()
    {
        yield return new WaitForSeconds(1);
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(15);
            // action text
            StartCoroutine(bMM.typeActionText("scorpion used pincers!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "scorpionCombat_active"));
        }
        else {
            StartCoroutine(bMM.typeActionText("scorpion missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(ScorpSnip, 1);
    }

    /////Mummy Attacks\\\\\
    ///Shamble\\\
    public IEnumerator mummyWalkin()
    {
        yield return new WaitForSeconds(1f);
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(25);
            // action text
            StartCoroutine(bMM.typeActionText("mummy used shamble!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "mummyCombat_active"));
        }
        else {
            StartCoroutine(bMM.typeActionText("mummy missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(MummyWalk, 1);
    }

    /////Desert Miniboss\\\\\
    /////Sandworm Attacks\\\\\
    ///Sinkhole\\\
    public IEnumerator sandwormHole()
    {
        yield return new WaitForSeconds(1f);
        enemyAll(15);
        
        // action text
        StartCoroutine(bMM.typeActionText("sandworm used sinkhole!", 0.01f));

        // play animation
        Animator animator = findAnimator();
        animator.SetTrigger("act");
        StartCoroutine(animPlaying(animator, "wormCombat_active"));
        AS.PlayOneShot(WormHole, 1);
    }

    ///Sand-stained Maw\\\
    public IEnumerator sandwormBite()
    {
        yield return new WaitForSeconds(1f);
        enemyHit(40);

        // action text
        StartCoroutine(bMM.typeActionText("sandworm used sand-stained maw!", 0.01f));

        // play animation
        Animator animator = findAnimator();
        animator.SetTrigger("act");
        StartCoroutine(animPlaying(animator, "wormCombat_active"));
        AS.PlayOneShot(WormBite, 1);
    }

    //////Swamp\\\\\\

    /////Giant Anaconda Attacks\\\\\
    ///Constrict\\\
    public IEnumerator snakeConstrict()
    {
        yield return new WaitForSeconds(1f);

        int reduceAmt = 1;
        /// 'coil' the target
        string snakeCoilTarget = enemyHit(0);
        snakeCoil = true;
        // reduce initiative
        if (snakeCoilTarget == "raza") {
            cm.initiativeCount[0] -= reduceAmt;
        }
        else if (snakeCoilTarget == "dorne") {
            cm.initiativeCount[1] -= reduceAmt;
        }
        else if (snakeCoilTarget == "smithson") {
            cm.initiativeCount[2] -= reduceAmt;
        }
        else if (snakeCoilTarget == "zor") {
            cm.initiativeCount[3] -= reduceAmt;
        }
        cm.sortInitiative(cm.initiativeCount);
        
        // action text
        StartCoroutine(bMM.typeActionText("anaconda used constrict!", 0.01f));
        // play attack animation
        Animator animator = findAnimator();
        animator.SetTrigger("act");
        StartCoroutine(animPlaying(animator, "snakeCombat_active"));
        //will lower initiative to minimum on target until the anaconda dies
        AS.PlayOneShot(SnakeCoil, 1);
    }

    ///Fangs\\\
    public IEnumerator snakeBite()
    {
        yield return new WaitForSeconds(1f);

        // if the snake has coiled a target, attack them
        if (snakeCoil)
        {
            if (snakeCoilTarget == "raza") {
                pS.char1HP -= 20;
                StartCoroutine(hM.dealDamage(hM.razaSlider, 20, 0.01f));
            }
            else if (snakeCoilTarget == "dorne") {
                pS.char2HP -= 20;
                StartCoroutine(hM.dealDamage(hM.dorneSlider, 20, 0.01f));
            }
            else if (snakeCoilTarget == "smithson") {
                pS.char3HP -= 20;
                StartCoroutine(hM.dealDamage(hM.smithsonSlider, 20, 0.01f));
            }
            else if (snakeCoilTarget == "zor") {
                pS.char4HP -= 20;
                StartCoroutine(hM.dealDamage(hM.zorSlider, 20, 0.01f));
            }
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "snakeCombat_active"));
        }
        else {
            // normal to hit attack
            int toHit = Random.Range(1, 100);
            if (toHit <= 90) {
                enemyHit(20);
                // action text
                StartCoroutine(bMM.typeActionText("anaconda used bite!", 0.01f));
                // play attack animation
                Animator animator = findAnimator();
                animator.SetTrigger("act");
                StartCoroutine(animPlaying(animator, "snakeCombat_active"));
            }
            else {
                StartCoroutine(bMM.typeActionText("anaconda missed!", 0.01f));
                StartCoroutine(pauseOnMiss(pauseWait));
            }
        }
        AS.PlayOneShot(SnakeBite, 1);
    }

    /////Crocodile Attacks\\\\\
    ///Bite\\\
    public IEnumerator crocBite()
    {
        yield return new WaitForSeconds(1f);

        int toHit = Random.Range(1, 100);
        // hit
        if (toHit <= 90)
        {
            // damage
            enemyHit(30);
            // action text
            StartCoroutine(bMM.typeActionText("crocodile used bite!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "crocodileCombat_active"));
        }
        // miss
        else {
            StartCoroutine(bMM.typeActionText("crocodile missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(CrocBite, 1);
    }

    ///Death Roll\\\
    public IEnumerator crocSpin()
    {
        yield return new WaitForSeconds(1f);

        int toHit = Random.Range(1, 101);
        // hit
        if (toHit <= 90) {
            // damage
            string target = enemyHit(20);
            sM.statusAdd(target, "bleed", 3);
            // action text
            StartCoroutine(bMM.typeActionText("crocodile used death spin!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "crocodileCombat_active"));
        }
        // miss
        else {
            StartCoroutine(bMM.typeActionText("crocodile missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
        AS.PlayOneShot(CrocSpin, 1);
    }

    /////Swamp Miniboss\\\\\
    /////Man Trap Attacks\\\\\
    ///Snap Shut\\\
    public void trapSnap()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            string target = enemyHit(25);
            //lowers enemy initiative to minimum for 2 turns
        }
        enemyDone = true;
        AS.PlayOneShot(TrapSnap, 1);
    }

    public void trapClamp()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            string target = enemyHit(30);
            //If target is slower than man trap, deal an additional 10 damage
        }
        enemyDone = true;
        AS.PlayOneShot(TrapClamp, 1);
    }

    //////Forest\\\\\\
    /////Giant SLime Attacks\\\\\
    ///Envelop\\\
    
    public void slimeEat()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            string target = enemyHit(30);
            //decreases target initiative by 1
        }
        enemyDone = true;
        AS.PlayOneShot(Slime, 1);
    }

    /////Giant Spider Attacks\\\\\
    ///Venemous Fangs\\\
    
    public void spiderBite()
    {
        int toHit = Random.Range(1, 100);
        if(toHit <= 90)
        {
            string target = enemyHit(20);
            sM.statusAdd(target, "poison", 3);
            // inflicts poison for 3 turns on the target
        }
        enemyDone = true;
        AS.PlayOneShot(SpiderBite, 1);
    }

    ///Webbing\\\
    public void spiderWeb()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            string target = enemyHit(10);
            // subtracts 2 from intiative until combat ends
        }
        enemyDone = true;
        AS.PlayOneShot(SpiderWeb, 1);
    }

    /////Forest Miniboss\\\\\
    /////Giant Attacks\\\\\
    ///Tree Smack\\\
    
    public void giantWack()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(50);
        }
        enemyDone = true;
        AS.PlayOneShot(GiantStrike, 1);
    }

    ///Stomp\\\
    public void giantStomp()
    {
        //lowers initiative of all party members by 1
        enemyDone = true;
        AS.PlayOneShot(GiantStomp, 1);
    }
}
