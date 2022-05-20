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
        if (target != null) {
            target = null;
        }
        
        while (target == null) {
            if (Input.GetMouseButtonDown(0)) {
                target = this.onClick();
                yield return target;
            }
            yield return null;
        }
        if (callback != null) callback();
    }

    // method that waits for the player to click certain objects \\
    public IEnumerator waitForClick(Action callback, string tag) {
        if (target != null) {
            target = null;
        }

        while (target == null) {
            if (Input.GetMouseButtonDown(0)) {
                target = this.onClick();
                if (target != null) {
                    if (target.tag == tag) {
                        yield return target;
                    }
                }
                else {
                    target = null;
                }
            }
            yield return null;
        }
        if (callback != null) callback();
    }

    // method that waits for the player to click on multiple 'things' \\
    public IEnumerator waitForClick(Action callback, int num) {
        // reset
        if (target != null) {
            target = null;
        }
        if (targetList.Count != -1) {
            int loops = targetList.Count;
            for (int i = 0; i < loops; i++) {
                targetList.RemoveAt(0);
            }
        }

        // main checking
        for (int i = 0; i < num; i++) {
            while (target == null) {
                if (Input.GetMouseButtonDown(0)) {
                    target = this.onClick();
                    targetList.Add(target);
                    yield return target;
                }
                yield return null;
            }
            target = null;
        }

        // callback to player action functions
        if (callback != null) callback();
    }

    // method that waits for the player to click on multiple 'certain' 'things' \\
    public IEnumerator waitForClick(Action callback, int num, string tag) {
        // reset
        if (target != null) {
            target = null;
        }
        if (targetList.Count != -1) {
            int loops = targetList.Count;
            for (int i = 0; i < loops; i++) {
                targetList.RemoveAt(0);
            }
        }

        // main checking
        for (int i = 0; i < num; i++) {
            while (target == null) {
                if (Input.GetMouseButtonDown(0)) {
                    target = this.onClick();
                    if (target != null) {
                        if (target.tag == tag) {
                            targetList.Add(target);
                            yield return target;
                        }
                    }
                    else {
                        target = null;
                    }
                }
                yield return null;
            }
            target = null;
        }

        // callback to player action functions
        if (callback != null) callback();
    }
}
