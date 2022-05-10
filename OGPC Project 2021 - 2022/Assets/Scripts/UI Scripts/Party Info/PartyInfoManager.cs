using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyInfoManager : MonoBehaviour
{
    // menu stuff
    public bool menuOpen;
    public int currentPage = 1;
    public GameObject cycleFwdButton;
    public GameObject cycleBwdButton;

    // party stats
    private PartyStats pS;

    // PC info pages
    [Header("Menus")]
    public GameObject razaMenu;
    public GameObject dorneMenu;
    public GameObject smithsonMenu;
    public GameObject zorMenu;
    public GameObject partyStatsMenu;
    public GameObject statInfoText;

    // cache access to PartyStats
    void Start() {
        pS = GameObject.Find("Party and Player Manager").GetComponent<PartyStats>();
    }

    // info logic
    void Update() {
        // turning menu off and on
        if (loadingScreenManager.loadingDone && !DialogueManager.InDialogue && !Inventory.invMenuOpen) {
            if (Input.GetKeyDown(KeyCode.E)) {
                menuOpen = !menuOpen;
                PlayerMovement.playerAbleMove = !PlayerMovement.playerAbleMove;
                updateStatInfo();
            }
        }

        // if the menu is on
        if (menuOpen) {
            // cycle through the menus
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                cycleForward();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                cycleBackwards();
            }
            cycleFwdButton.SetActive(true);
            cycleBwdButton.SetActive(true);
            // raza page
            if (currentPage == 1) {
                razaMenu.SetActive(true);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(false);
                partyStatsMenu.SetActive(false);
            }
            // dorne page
            else if (currentPage == 2) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(true);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(false);
                partyStatsMenu.SetActive(false);
            }
            // smithson page
            else if (currentPage == 3) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(true);
                zorMenu.SetActive(false);
                partyStatsMenu.SetActive(false);
            }
            // zor page
            else if (currentPage == 4) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(true);
                partyStatsMenu.SetActive(false);
            }
            // other pages
            else if (currentPage == 5) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(false);
                partyStatsMenu.SetActive(true);
            }
        }
        else {
            cycleFwdButton.SetActive(false);
            cycleBwdButton.SetActive(false);
            razaMenu.SetActive(false);
            dorneMenu.SetActive(false);
            smithsonMenu.SetActive(false);
            zorMenu.SetActive(false);
            partyStatsMenu.SetActive(false);
        }
    }

    // method that updates the party stats info with correct information of each pc's stats \\
    public void updateStatInfo() {
        TextMeshProUGUI text = statInfoText.GetComponent<TextMeshProUGUI>();

        text.text = "";
        // raza info
        text.text += "raza - " + pS.char1HP + "/" + pS.char1HPMax + " hp";
        text.text += "\n\n";
        // dorne info
        text.text += "dorne - " + pS.char2HP + "/" + pS.char2HPMax + " hp";
        text.text += "\n\n";
        // smithson info
        text.text += "smithson - " + pS.char3HP + "/" + pS.char2HPMax + " hp\n";
        text.text += "                 " + pS.char3Mana + "/" + pS.char3ManaMax + " mana";
        text.text += "\n\n";
        // zor info
        text.text += "zor - " + pS.char4HP + "/" + pS.char4HPMax + " hp";
    }

    // methods for cycling through the party info menu \\
    public void cycleForward() {
        currentPage++;
        if (currentPage > 5) {
            currentPage = 1;
        }
    }
    public void cycleBackwards() {
        currentPage--;
        if (currentPage < 1) {
            currentPage = 5;
        }
    }
}
