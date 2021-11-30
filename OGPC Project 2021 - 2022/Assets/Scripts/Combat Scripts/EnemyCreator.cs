using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator
{
    // stats variables \\
    public int health;
    public int healthMax;
    public int mana;
    public int manaMax;
    public int dexterity;

    public string name;

    // main object creator \\
    public EnemyCreator() {
        int chance = Random.Range(1, 3);

        // zombie enemy
        if (chance == 1) {
            name = "Zombie";
            healthMax = 100;
            manaMax = 0;
            dexterity = -1;
        }
        // skeleton enemy
        else if (chance == 2) {
            name = "Skeleton";
            healthMax = 90;
            manaMax = 0;
            dexterity = 2;
        }
        // ooze enemy
        else if (chance == 3) {
            name = "Ooze";
            healthMax = 110;
            manaMax = 0;
            dexterity = 0;
        }
        health = healthMax;
        mana = manaMax;
    }

    // the object's to string method \\
    public string toString() {
        string text = "Name: " + name + "| Health: " + healthMax + "| Mana: " + manaMax + "| Dexterity: " + dexterity;
        return text;
    }

}
