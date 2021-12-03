using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem
{
    public TargetingSystem() {

    }

    public GameObject onClick() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null) {
            return hit.collider.gameObject;
        }
        return null;
    }
}
