using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnIndicator : MonoBehaviour
{
    // game object variables \\
    [Header("GameObject Variables")]
    public GameObject raza;
    public GameObject dorne;
    public GameObject smithson;
    public GameObject zor;
    // Variables \\
    [Header("Logic Variables")]
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
            if (cM.gameObjectsInCombat[cM.initiativeIndex] != null) {
                if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[4].name) {
                    indicatorRenderer.sprite = enemyIndicator;
                    indicator.transform.position = new Vector2(gameObjects[4].transform.position.x - (gameObjects[4].GetComponent<BoxCollider2D>().size.x/2), gameObjects[4].transform.position.y);
                }
                else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[5].name) {
                    indicatorRenderer.sprite = enemyIndicator;
                    indicator.transform.position = new Vector2(gameObjects[5].transform.position.x - (gameObjects[5].GetComponent<BoxCollider2D>().size.x/2), gameObjects[5].transform.position.y);
                }
                else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[6].name) {
                    indicatorRenderer.sprite = enemyIndicator;
                    indicator.transform.position = new Vector2(gameObjects[6].transform.position.x - (gameObjects[6].GetComponent<BoxCollider2D>().size.x/2), gameObjects[6].transform.position.y);
                }
                else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == gameObjects[7].name) {
                    indicatorRenderer.sprite = enemyIndicator;
                    indicator.transform.position = new Vector2(gameObjects[7].transform.position.x - (gameObjects[7].GetComponent<BoxCollider2D>().size.x/2), gameObjects[7].transform.position.y);
                }   
            }
        }
    }
}
