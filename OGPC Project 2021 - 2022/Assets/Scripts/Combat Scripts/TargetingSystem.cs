using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetingSystem
{
    // the target of the click \\
    public GameObject target;
    public List<GameObject> targetList = new List<GameObject>();

    // empty constructor because it doesn't need anything \\
    public TargetingSystem() { }

    // finding the gameobject the player clicked on \\
    public GameObject onClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }

    // method that waits for the player to click \\
    public IEnumerator waitForClick(Action callback) {
        // reset variable
        if (target != null) {
            target = null;
        }
        
        // wait until we get a target from a click
        while (target == null) {
            if (Input.GetMouseButtonDown(0)) {
                target = this.onClick();
                yield return target;
            }
            yield return null;
        }
        if (callback != null) callback();
    }

    // method that allows for multiple clicks \\
    public IEnumerator waitForClickMultiple(Action callback, int numTargets) {
        // reset list and variable
        if (targetList.Count != -1) {
            for (int i = 0; i < targetList.Count; i++) {
                targetList.RemoveAt(0);
            }
        }
        if (target != null) {
            target = null;
        }

        // for how many enemies we need to target, wait for that many
        for (int i = 0; i < numTargets; i++) {
            while (target == null) {
                if (Input.GetMouseButtonDown(0)) {
                    target = this.onClick();
                    targetList.Add(target);
                    Debug.Log(target.name);
                    yield return target;
                }
                yield return null;
            }
            target = null;
        }
        if (callback != null) callback();
    }
}
