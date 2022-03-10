using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CombatEnemy : MonoBehaviour
{
    // enemy object
    public EnemyCreator eOb;
    // the sprite renderer of the enemy
    public SpriteRenderer sr;

    // method that updates their sprites and changes the size of their colliders \\
    public void updateSprite() {
        if (eOb.name == "Scorpion") {
            GetComponent<Animator>().SetBool("isScorpion", true);
            GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 1);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.15f, -0.5f);
        }
        else if (eOb.name == "Mummy") {
            GetComponent<Animator>().SetBool("isMummy", true);
        }
    }
}
