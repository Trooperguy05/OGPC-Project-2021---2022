using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random=UnityEngine.Random;

public class PlayerActions : MonoBehaviour
{
    // targeting system variable
    private TargetingSystem ts;

    // combat manager
    public GameObject combatManager;
    private CombatManager cm;

    // variable that tells the combatmanager if the player is done
    // with a character's turn
    public bool charDone = false;

    // caching
    void Start() {
        // create a new targeting system
        ts = new TargetingSystem();
        // get the combat manager script
        cm = combatManager.GetComponent<CombatManager>();
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

    /////   Character: Dorne's Actions   \\\\\
    /// Action Wrappers \\\
    public void execute_dorneStrike() {
        StartCoroutine(ts.waitForClick(dorneStrike));
    }
    /// Actions \\\
    // basic strike action \\
    public void dorneStrike() {
        // wait for target to return
        GameObject target = ts.target;
        EnemyCreator enemy = getEnemy(target.name);
        // act on target
        int chanceToMiss = Random.Range(1, 100);
        if (chanceToMiss >= 20) {
            enemy.health -= 10;
            Debug.Log("Health: " + enemy.health);
        }
    }

}
