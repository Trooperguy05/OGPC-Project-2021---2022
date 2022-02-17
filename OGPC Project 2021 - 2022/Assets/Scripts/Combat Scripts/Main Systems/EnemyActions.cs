using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemyActions : MonoBehaviour
{
    //party stats and combat manager
    private CombatManager cm;
    private PartyStats pS;
    private StatusManager sM;
    private HealthbarManager hM;

    //enemy specific variables
    public bool snakeCoil = false;
    public int snakeCoilTarget;

    // dictates whether the enemy is done or not \\
    public bool enemyDone = false;


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

    }

    //decides what party member is targeted by an enemy
    public string enemyHit(int dmg)
    {
        int target = 0;
        string character = "";
        int healthCheck = 0;
        while (healthCheck < 1)
        {
            target = Random.Range(1, 4);
            if (target == 1)
            {
                healthCheck = pS.char1HP;
                character = "raza";
            }
            else if (target == 2)
            {
                healthCheck = pS.char2HP;
                character = "dorne";
            }
            else if (target == 3)
            {
                healthCheck = pS.char3HP;
                character = "smithson";
            }
            else
            {
                healthCheck = pS.char4HP;
                character = "zor";
            }
        }
        if (target == 1)
        {
            pS.char1HP -= dmg;
            StartCoroutine(hM.dealDamage(hM.razaSlider, dmg, 0.01f));
        }
        else if (target == 2)
        {
            pS.char2HP -= dmg;
            StartCoroutine(hM.dealDamage(hM.dorneSlider, dmg, 0.01f));
        }
        else if (target == 3)
        {
            pS.char3HP -= dmg;
            StartCoroutine(hM.dealDamage(hM.smithsonSlider, dmg, 0.01f));
        }
        else
        {
            pS.char4HP -= dmg;
            StartCoroutine(hM.dealDamage(hM.zorSlider, dmg, 0.01f));
        }
        return character;
    }

    //variation of the enemy attack method but without a return value, as to hit all player characters
    public void enemyAll(int dmg)
    {
        pS.char1HP -= dmg;
        pS.char2HP -= dmg;
        pS.char3HP -= dmg;
        pS.char4HP -= dmg;
    }

    ////////Enemy Types\\\\\\\\

    //////Desert\\\\\\

    /////Scorpion Attacks\\\\\
    ///Stinger\\\
    public IEnumerator scorpSting()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            string character = enemyHit(10);
            //sM.statusAdd(character, "poison", 3);
        }
        //poisons target, dealing 3 damage per turn for 3 turns. will impliment with status manager.
        yield return new WaitForSeconds(1);
        enemyDone = true;
    }

    ///Pincers\\\
    public IEnumerator scorpPinch()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(15);
        }

        yield return new WaitForSeconds(1);
        enemyDone = true;
    }

    /////Mummy Attacks\\\\\
    ///Shamble\\\
    public void mummyWalkin()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(25);
        }
        enemyDone = true;
    }

    /////Desert Miniboss\\\\\
    /////Sandworm Attacks\\\\\
    ///Sinkhole\\\
    public void sandwormHole()
    {
        enemyAll(15);
        enemyDone = true;
    }

    ///Sand-stained Maw\\\
    public void sandwormBite()
    {
        enemyHit(40);
        enemyDone = true;
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
