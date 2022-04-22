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

    //enemy specific variables
    [Header("Enemy Abilities")]
    public bool snakeCoil = false;
    public int snakeCoilTarget;

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
            string character = enemyHit(10);
            // action text
            StartCoroutine(bMM.typeActionText("scorpion used sting!", 0.01f));
            // play attack animation
            Animator animator = findAnimator();
            animator.SetTrigger("act");
            StartCoroutine(animPlaying(animator, "scorpionCombat_active"));
            //sM.statusAdd(character, "poison", 3);
        }
        else {
            StartCoroutine(bMM.typeActionText("scorpion missed!", 0.01f));
            StartCoroutine(pauseOnMiss(pauseWait));
        }
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
    }

    //////Swamp\\\\\\

    /////Giant Anaconda Attacks\\\\\
    ///Constrict\\\
    public void snakeConstrict()
    {
        string snakeCoilTarget = enemyHit(0);
        snakeCoil = true;
        //will lower initiative to minimum on target until the anaconda dies
        enemyDone = true;
    }

    ///Fangs\\\
    public void snakeBite()
    {
        int toHit = Random.Range(1, 100);
        if (snakeCoil)
        {
            if (snakeCoilTarget == 1)
            {
                pS.char1HP -= 20;
            }
            else if (snakeCoilTarget == 2)
            {
                pS.char2HP -= 20;
            }
            else if (snakeCoilTarget == 3)
            {
                pS.char3HP -= 20;
            }
            else
            {
                pS.char4HP -= 20;
            }
        }
        else if (toHit <= 90)
        {
            enemyHit(20);
        }
        enemyDone = true;
    }

    /////Crocodile Attacks\\\\\
    ///Bite\\\
    public void crocBite()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(30);
        }
        enemyDone = true;
    }

    ///Death Roll\\\
    public void crocSpin()
    {
        string target = enemyHit(20);
        sM.statusAdd(target, "bleed", 3);
        // inflicts bleed condition for 3 turns (5 dmg per turn)
        enemyDone = true;
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
    }

    ///Stomp\\\
    public void giantStomp()
    {
        //lowers initiative of all party members by 1
        enemyDone = true;
    }
}
