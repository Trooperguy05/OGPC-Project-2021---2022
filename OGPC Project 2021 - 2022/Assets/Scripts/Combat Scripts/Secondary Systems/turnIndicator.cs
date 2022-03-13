using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnIndicator : MonoBehaviour
{
    // VAriables \\
    public Sprite playerIndicator;
    public Sprite enemyIndicator;
    public GameObject indicator;
    private SpriteRenderer indicatorRenderer;
    public float indicatorOffset = 1.0f;
    private CombatManager cM;
    public GameObject[] gameObjects = new GameObject[8];

    // grabbing stuff
    void Start() {
        cM = GetComponent<CombatManager>();
        indicatorRenderer = indicator.GetComponent<SpriteRenderer>();
    }

    // method that updates where the turn indicator points to \\
    public void updateIndicator() {
        // if it's one of the player's turn
        GameObject raza = GameObject.Find("Raza");
        GameObject dorne = GameObject.Find("Dorne");
        GameObject smithson = GameObject.Find("Smithson");
        GameObject zor = GameObject.Find("Zor");
        if (cM.initiativeNames[cM.initiativeIndex] == "Raza") {
            indicatorRenderer.sprite = playerIndicator;
            indicator.transform.position = new Vector2(raza.transform.position.x + indicatorOffset, raza.transform.position.y);
        }
        else if (cM.initiativeNames[cM.initiativeIndex] == "Dorne") {
            indicatorRenderer.sprite = playerIndicator;
            indicator.transform.position = new Vector2(dorne.transform.position.x + indicatorOffset, dorne.transform.position.y);
        }
        else if (cM.initiativeNames[cM.initiativeIndex] == "Smithson") {
            indicatorRenderer.sprite = playerIndicator;
            indicator.transform.position = new Vector2(smithson.transform.position.x + indicatorOffset, smithson.transform.position.y);
        }
        else if (cM.initiativeNames[cM.initiativeIndex] == "Zor") {
            indicatorRenderer.sprite = playerIndicator;
            indicator.transform.position = new Vector2(zor.transform.position.x + indicatorOffset, zor.transform.position.y);
        }
        else { // if it's one of the enemy's turn
            if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[4].name) {
                indicatorRenderer.sprite = enemyIndicator;
                indicator.transform.position = new Vector2(gameObjects[4].transform.position.x - indicatorOffset, gameObjects[4].transform.position.y);
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[5].name) {
                indicatorRenderer.sprite = enemyIndicator;
                indicator.transform.position = new Vector2(gameObjects[5].transform.position.x - indicatorOffset, gameObjects[5].transform.position.y);
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[6].name) {
                indicatorRenderer.sprite = enemyIndicator;
                indicator.transform.position = new Vector2(gameObjects[6].transform.position.x - indicatorOffset, gameObjects[6].transform.position.y);
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[7].name) {
                indicatorRenderer.sprite = enemyIndicator;
                indicator.transform.position = new Vector2(gameObjects[7].transform.position.x - indicatorOffset, gameObjects[7].transform.position.y);
            }
        }
    }
}
