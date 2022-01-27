using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    //party stats and combat manager
    private CombatManager cm;
    private PartyStats pS;

    //enemy specific variables
    public bool snakeCoil = false;
    public int snakeCoilTarget;


    //caching
    void Start()
    {
        // get the combat manager script
        cm = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        // get the party stats manager
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
    }

    //decides what party member is targeted by an enemy
    public int enemyHit(int dmg)
    {
        int target = 0;
        int healthCheck = 0;
        while (healthCheck < 1)
        {
            target = Random.Range(1, 4);
            if (target == 1)
            {
                healthCheck = pS.char1HP;
            }
            else if (target == 2)
            {
                healthCheck = pS.char2HP;
            }
            else if (target == 3)
            {
                healthCheck = pS.char3HP;
            }
            else
            {
                healthCheck = pS.char4HP;
            }
        }
        if (target == 1)
        {
            pS.char1HP -= dmg;
        }
        else if (target == 2)
        {
            pS.char2HP -= dmg;
        }
        else if (target == 3)
        {
            pS.char3HP -= dmg;
        }
        else
        {
            pS.char4HP -= dmg;
        }
        return target;
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
    public void scorpSting()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            int target = enemyHit(10);
        }
        //poisons target, dealing 3 damage per turn for 3 turns. will impliment with status manager.
    }

    ///Pincers\\\
    public void scorpPinch()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            enemyHit(15);
        }
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
    }

    /////Desert Miniboss\\\\\
    /////Sandworm Attacks\\\\\
    ///Sinkhole\\\
    public void sandwormHole()
    {
        enemyAll(15);
    }

    ///Sand-stained Maw\\\
    public void sandwormBite()
    {
        enemyHit(40);
    }

    //////Swamp\\\\\\

    /////Giant Anaconda Attacks\\\\\
    ///Constrict\\\
    public void snakeConstrict()
    {
        snakeCoilTarget = enemyHit(0);
        snakeCoil = true;
        //will lower initiative to minimum on target until the anaconda dies
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
    }

    ///Death Roll\\\
    public void crocSpin()
    {
        int target = enemyHit(20);
        // inflicts bleed condition for 3 turns (5 dmg per turn)
    }

    /////Swamp Miniboss\\\\\
    /////Man Trap Attacks\\\\\
    ///Snap Shut\\\
    public void trapSnap()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            int target = enemyHit(25);
            //lowers enemy initiative to minimum for 2 turns
        }
    }

    public void trapClamp()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            int target = enemyHit(30);
            //If target is slower than man trap, deal an additional 10 damage
        }
    }

    //////Forest\\\\\\
    /////Giant SLime Attacks\\\\\
    ///Envelop\\\
    
    public void slimeEat()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            int target = enemyHit(30);
            //decreases target initiative by 1
        }
    }

    /////Giant Spider Attacks\\\\\
    ///Venemous Fangs\\\
    
    public void spiderBite()
    {
        int toHit = Random.Range(1, 100);
        if(toHit <= 90)
        {
            int target = enemyHit(20);
            // inflicts poison for 3 turns on the target
        }
    }

    ///Webbing\\\
    public void spiderWeb()
    {
        int toHit = Random.Range(1, 100);
        if (toHit <= 90)
        {
            int target = enemyHit(10);
            // subtracts 2 from intiative until combat ends
        }
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
    }

    ///Stomp\\\
    public void giantStomp()
    {
        //lowers initiative of all party members by 1
    }
}
