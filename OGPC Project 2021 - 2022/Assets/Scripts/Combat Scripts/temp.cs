using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
    public TargetingSystem ts;

    void Start() {
        ts = new TargetingSystem();
    }

    public void activate() {
        StartCoroutine(ts.waitForClickMultiple(null, 2));
    }
}
