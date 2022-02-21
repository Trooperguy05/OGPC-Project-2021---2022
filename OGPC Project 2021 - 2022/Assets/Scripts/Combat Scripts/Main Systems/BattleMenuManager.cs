using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuManager : MonoBehaviour
{
    // other scripts
    private CombatManager cM;
    private PlayerActions pA;

    // player action buttons \\
    public GameObject razaActions;
    public GameObject dorneActions;
    public GameObject smithsonActions;
    public GameObject zorActions;

    // variable for checking if the manager is hiding the player actions \\
    public bool hidingActions = false;

    // Start is called before the first frame update
    void Start()
    {
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        pA = GameObject.Find("Action Manager").GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cM.combatStarted && cM.gameObjectsInCombat[cM.initiativeIndex] != null && !hidingActions) {
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

    // method that hides the actions when the player chooses one \\
    public void execute_hideActions() {
        StartCoroutine(hideActions());
    }
    public IEnumerator hideActions() {
        hidingActions = true;
        while (!pA.charDone) {
            razaActions.SetActive(false);
            dorneActions.SetActive(false);
            smithsonActions.SetActive(false);
            zorActions.SetActive(false);
            yield return null; 
        }
        yield return new WaitForSeconds(0.5f);
        hidingActions = false;
    }
}
