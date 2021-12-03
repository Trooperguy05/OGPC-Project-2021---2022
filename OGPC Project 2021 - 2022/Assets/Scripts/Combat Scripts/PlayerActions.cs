using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // targeting system variable
    private TargetingSystem ts;

    // create a new targeting system
    void Start() {
        ts = new TargetingSystem();
    }

    ///   Character: Dorne's Actions   \\\
    public void dorneStrike() {
        // wait for target to return
        // act on target
    }
}
