using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TargetingSystem
{
    // the target of the click \\
    public GameObject target;

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
        while (target == null) {
            if (Input.GetMouseButtonDown(0)) {
                target = this.onClick();
                yield return target;
            }
            yield return null;
        }
        if (callback != null) callback();
        target = null;
    }
}
