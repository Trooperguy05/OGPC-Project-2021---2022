using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PlayerActions : MonoBehaviour
{
    // targeting system variable
    private TargetingSystem ts;

    // caching
    void Start() {
        // create a new targeting system
        ts = new TargetingSystem();
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
        Debug.Log(target.name);
        // act on target
    }

}
