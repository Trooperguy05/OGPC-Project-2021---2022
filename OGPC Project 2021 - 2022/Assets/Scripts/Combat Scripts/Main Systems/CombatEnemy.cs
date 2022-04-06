using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CombatEnemy : MonoBehaviour
{
    // enemy object
    public EnemyCreator eOb;
    public int health;
    // the sprite renderer of the enemy
    public SpriteRenderer sr;

    void Update() {
        if (health != 0) {
            health = eOb.health;
        }
        if (eOb != null) {
            if (eOb.health <= 0) {
                sr.color = new Color(0, 0, 0, 1);
            }
        }
    }

    // method that updates their sprites and changes the size of their colliders \\
    public void updateSprite() {
        // desert enemies
        if (eOb.name == "Scorpion") {
            GetComponent<Animator>().SetBool("isScorpion", true);
            GetComponent<BoxCollider2D>().size = new Vector2(1.5f, 1);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.15f, -0.5f);
            health = eOb.health;
        }
        else if (eOb.name == "Mummy") {
            GetComponent<Animator>().SetBool("isMummy", true);
            health = eOb.health;
        }
        else if (eOb.name == "Worm") {
            GetComponent<Animator>().SetBool("isWorm", true);
            GetComponent<BoxCollider2D>().size = new Vector3(3, 3);
            health = eOb.health;
        }
    }
}
