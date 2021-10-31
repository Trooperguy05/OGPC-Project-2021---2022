using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    public GameObject[] initiative;

    void Start() {
        Debug.Log("Loading Party Stats");
        FindObjectOfType<PartyStats>().LoadData();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            FindObjectOfType<PartyStats>().SaveData();

            SceneManager.LoadScene(0);
        }
    }
}
