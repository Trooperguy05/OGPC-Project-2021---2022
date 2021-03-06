using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

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
        int chance = Random.Range(1, 3);

        // scorpion
        if (chance == 1) {
            name = "Scorpion";
            healthMax = 100;
            manaMax = 0;
            dexterity = 1;
            size = 1;
        }
        // mummy
        else if (chance == 2) {
            name = "Mummy";
            healthMax = 130;
            manaMax = 0;
            dexterity = -1;
            size = 1;
        }
        // crocodile
        else if (chance == 3) {
            name = "Crocodile";
            healthMax = 110;
            manaMax = 0;
            dexterity = 0;
            size = 1;
        }
        // snake
        else if (chance == 4) {
            name = "Snake";
            healthMax = 65;
            manaMax = 0;
            dexterity = 2;
            size = 1;
        }
        // worm miniboss
        else if (chance == 7) {
            name = "Worm";
            healthMax = 500;
            manaMax = 0;
            dexterity = -2;
            size = 4;
        }
        health = healthMax;
        mana = manaMax;
    }
    // specified enemy
    private void createEnemy(int num) {
        // scorpion
        if (num == 1) {
            name = "Scorpion";
            healthMax = 100;
            manaMax = 0;
            dexterity = 1;
            size = 1;
        }
        // mummy
        else if (num == 2) {
            name = "Mummy";
            healthMax = 130;
            manaMax = 0;
            dexterity = -1;
            size = 1;
        }
        // crocodile
        else if (num == 3) {
            name = "Crocodile";
            healthMax = 110;
            manaMax = 0;
            dexterity = 0;
            size = 1;
        }
        // snake
        else if (num == 4) {
            name = "Snake";
            healthMax = 65;
            manaMax = 0;
            dexterity = 2;
            size = 1;
        }
        // slime
        else if (num == 5) {
            name = "Slime";
            healthMax = 150;
            manaMax = 0;
            dexterity = -2;
            size = 2;
        }
        // spider
        else if (num == 6) {
            name = "Spider";
            healthMax = 130;
            manaMax = 0;
            dexterity = 3;
            size = 2;
        }
        // worm miniboss
        else if (num == 7) {
            name = "Worm";
            healthMax = 500;
            manaMax = 0;
            dexterity = -2;
            size = 4;
        }
        // man trap miniboss
        else if (num == 8) {
            name = "Man Trap";
            healthMax = 400;
            manaMax = 0;
            dexterity = 1;
            size = 4;
        }
        // giant miniboss
        else if (num == 9) {
            name = "Giant";
            healthMax = 600;
            manaMax = 0;
            dexterity = 1;
            size = 4;
        }
        // valazak final boss
        else if (num == 10) {
            name = "Dragon";
            healthMax = 1000;
            manaMax = 0;
            dexterity = 2;
            size = 4;
        }
        health = healthMax;
        mana = manaMax;
    }

}
