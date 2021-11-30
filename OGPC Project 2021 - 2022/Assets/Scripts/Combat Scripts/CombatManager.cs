using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Random=UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    // arrays for tracking initiative \\
    public GameObject[] initiativeNames;
    public int[] initiativeCount = new int[8];

    // load the party stats when the player enters combat
    void Start() {
        Debug.Log("Loading Party Stats");
        FindObjectOfType<PartyStats>().LoadData();
    }

    void Update() {
        // return to the overworld scene from the combat scene
        // will remove later
        if (Input.GetKeyDown(KeyCode.Space)) {
            FindObjectOfType<PartyStats>().SaveData();

            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            startCombat();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            startCombat();
            sortThing(initiativeCount);
            string str = "";
            for (int i = 0; i < initiativeCount.Length; i++) {
                str += initiativeCount[i] + ", ";
            }
            Debug.Log(str);
        }
    }

    // method that "rolls" for combat initiative \\
    void startCombat() {
        // grabbing party stats
        PartyStats stats = FindObjectOfType<PartyStats>();

        initiativeCount[0] = Random.Range(1, 20);
        initiativeCount[1] = Random.Range(1, 20);
        initiativeCount[2] = Random.Range(1, 20);
        initiativeCount[3] = Random.Range(1, 20);
        initiativeCount[4] = Random.Range(1, 20);
        initiativeCount[5] = Random.Range(1, 20);
        initiativeCount[6] = Random.Range(1, 20);
        initiativeCount[7] = Random.Range(1, 20);
    }

    void sortThing(int[] arr) {
        Array.Sort(arr);
    }
}
