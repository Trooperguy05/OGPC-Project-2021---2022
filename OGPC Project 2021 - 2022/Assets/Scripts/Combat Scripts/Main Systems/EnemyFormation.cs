using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    private CombatManager cM;
    private HealthbarManager hM;

    // the 4 different formations the enemies could be in on the field \\
    private Vector2[] fourEnemyPositions = {
        new Vector2(4, 4), new Vector2(4, 2.6f), new Vector2(4, 1.2f), new Vector2(4, -0.2f)
    };
    private Vector2[] threeEnemyPositions = {
        new Vector2(4, 4), new Vector2(4, 1.9f), new Vector2(4, -0.2f)
    };
    private Vector2[] twoEnemyPositions = {
        new Vector2(4, 3.3f), new Vector2(4, 0.5f)
    };
    private Vector2 oneEnemyPosition = new Vector2(4, 1.9f);

    void Start() {
        // grabbing the combat manager
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        // getting the healthbar manager
        hM = GameObject.Find("Healthbar Manager").GetComponent<HealthbarManager>();
    }

    // method that reorganizes the field of enemies \\
    public void organizeField() {
        // checking the amount of enemies
        if (cM.enemiesInCombat.Count == 4) {
            for (int i = 0; i < fourEnemyPositions.Length; i++) {
                cM.enemiesInCombat[i].transform.position = fourEnemyPositions[i];
            }
        }
        if (cM.enemiesInCombat.Count == 3) {
            for (int i = 0; i < threeEnemyPositions.Length; i++) {
                cM.enemiesInCombat[i].transform.position = threeEnemyPositions[i];
            }
        }
        if (cM.enemiesInCombat.Count == 2) {
            for (int i = 0; i < twoEnemyPositions.Length; i++) {
                cM.enemiesInCombat[i].transform.position = twoEnemyPositions[i];
            }
        }
        if (cM.enemiesInCombat.Count == 1) {
            cM.enemiesInCombat[0].transform.position = oneEnemyPosition;
        }

        // move the enemies' healthbars
        if (cM.enemy1 != null) {
            hM.enemy1Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy1.transform.position.x + 2, cM.enemy1.transform.position.y);
        }
        if (cM.enemy2 != null) {
            hM.enemy2Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy2.transform.position.x + 2, cM.enemy2.transform.position.y);
        }
        if (cM.enemy3 != null) {
            hM.enemy3Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy3.transform.position.x + 2, cM.enemy3.transform.position.y);
        }
        if (cM.enemy4 != null) {
            hM.enemy4Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy4.transform.position.x + 2, cM.enemy4.transform.position.y);
        }
    }
}
