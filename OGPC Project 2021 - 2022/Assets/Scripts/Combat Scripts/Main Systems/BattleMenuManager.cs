using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenuManager : MonoBehaviour
{
    // other scripts
    private CombatManager cM;
    public PlayerActions pA;

    // player action buttons \\
    public GameObject razaActions;
    public GameObject dorneActions;
    public GameObject smithsonActions;
    public GameObject zorActions;

    // action text \\
    public TextMeshProUGUI actionText;

    // variable for checking if the manager is hiding the player actions \\
    public bool hidingActions = false;

    // variable that checks if the action text is done \\
    public bool actionTextDone = false;

    // variable that checks what index the hidingActions was called at \\
    private int indexCalled;

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

    // method that types out the action text to the battle menu \\
    public IEnumerator typeActionText(string text, float typeSpeed) {
        // typing
        actionText.text = "";
        for (int i = 0; i < text.Length; i++) {
            actionText.text += text[i];
            yield return new WaitForSeconds(typeSpeed);
        }

        // after typing
        yield return new WaitForSeconds(2.5f);
        actionText.text = "";
    }
    public IEnumerator typeHelperActionText(string text, float typeSpeed) {
        actionText.text = "";
        // typing
        for (int i = 0; i < text.Length; i++) {
            actionText.text += text[i];
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    // method that hides the actions when the player chooses one \\
    public void execute_hideActions() {
        StartCoroutine(hideActions());
    }
    public IEnumerator hideActions() {
        indexCalled = cM.initiativeIndex;
        hidingActions = true;
        while (!pA.charDone) {
            razaActions.SetActive(false);
            dorneActions.SetActive(false);
            smithsonActions.SetActive(false);
            zorActions.SetActive(false);
            if (indexCalled != cM.initiativeIndex) {
                break;
            }
            yield return null; 
        }
        yield return new WaitForSeconds(0.5f);
        hidingActions = false;
    }
}
