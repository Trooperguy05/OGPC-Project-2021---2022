using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable
{
    public int interactableRange = 1;
    private bool interact = false;

    // constructor
    public interactable(int iR) {
        interactableRange = iR;
    }

    // check distance
    public float checkDistance(GameObject o1, GameObject o2) {
        float distance = Vector2.Distance(o1.transform.position, o2.transform.position);
        return distance;
    }

    // change interactability
    public void changeInteract(bool change) {
        interact = change;
    }

    // check interactbility
    public bool isInteractable() {
        return this.interact;
    }
}
