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
    public int size;

    public string name;

    // main object creator \\
    // creates random enemy
    public EnemyCreator() {
        this.createEnemyRandom();
    }
    // creates a specified enemy
    public EnemyCreator(int num) {
        if (num == -1) {
            this.createEnemyRandom();
        }
        else {
            this.createEnemy(num);
        }
    }

    // the object's to string method \\
    public string toString() {
        string text = "Name: " + name + "| Health: " + healthMax + "| Mana: " + manaMax + "| Dexterity: " + dexterity;
        return text;
    }

    ///   Methods that creates the enemy, random or specified   \\\
    // random enemy
    private void createEnemyRandom() {
        int chance = Random.Range(1, 2);

        // zombie enemy
        if (chance == 1) {
            name = "Scorpion";
            healthMax = 150;
            manaMax = 0;
            dexterity = 1;
            size = 1;
        }
        // skeleton enemy
        else if (chance == 2) {
            name = "Mummy";
            healthMax = 200;
            manaMax = 0;
            dexterity = -1;
            size = 1;
        }
        health = healthMax;
        mana = manaMax;
    }
    // specified enemy
    private void createEnemy(int num) {
        // zombie enemy
        if (num == 1) {
            name = "Zombie";
            healthMax = 100;
            manaMax = 0;
            dexterity = -1;
            size = 1;
        }
        // skeleton enemy
        else if (num == 2) {
            name = "Skeleton";
            healthMax = 90;
            manaMax = 0;
            dexterity = 2;
            size = 1;
        }
        // ooze enemy
        else if (num == 3) {
            name = "Ooze";
            healthMax = 110;
            manaMax = 0;
            dexterity = 0;
            size = 2;
        }
        // adult red dragon enemy
        else if (num == 4) {
            name = "Adult Red Dragon";
            healthMax = 200;
            manaMax = 100;
            dexterity = 2;
            size = 3;
        }
        // tarrasque enemy
        else if (num == 5) {
            name = "Tarrasque";
            healthMax = 300;
            manaMax = 0;
            dexterity = 2;
            size = 4; 
        }
        health = healthMax;
        mana = manaMax;
    }

}
