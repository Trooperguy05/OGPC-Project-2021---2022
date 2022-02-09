using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuManager : MonoBehaviour
{
    // other scripts
    private CombatManager cM;

    // player action buttons \\
    public GameObject razaActions;
    public GameObject dorneActions;
    public GameObject smithsonActions;
    public GameObject zorActions;

    // Start is called before the first frame update
    void Start()
    {
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cM.combatStarted && cM.gameObjectsInCombat[cM.initiativeIndex] != null) {
            if (cM.gameObjectsInCombat[cM.initiativeIndex].name == "Raza") {
                razaActions.SetActive(true);
                dorneActions.SetActive(false);
                smithsonActions.SetActive(false);
                zorActions.SetActive(false);
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == "Dorne") {
                razaActions.SetActive(false);
                dorneActions.SetActive(true);
                smithsonActions.SetActive(false);
                zorActions.SetActive(false);
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == "Smithson") {
                razaActions.SetActive(false);
                dorneActions.SetActive(false);
                smithsonActions.SetActive(true);
                zorActions.SetActive(false);        
            }
            else if (cM.gameObjectsInCombat[cM.initiativeIndex].name == "Zor") {
                razaActions.SetActive(false);
                dorneActions.SetActive(false);
                smithsonActions.SetActive(false);
                zorActions.SetActive(true);                
            }
            else {
                razaActions.SetActive(false);
                dorneActions.SetActive(false);
                smithsonActions.SetActive(false);
                zorActions.SetActive(false);
            }   
        }
    }
}
