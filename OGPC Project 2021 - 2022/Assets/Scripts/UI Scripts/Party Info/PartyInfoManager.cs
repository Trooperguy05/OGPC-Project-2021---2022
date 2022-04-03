using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyInfoManager : MonoBehaviour
{
    // menu stuff
    public bool menuOpen;
    public int currentPage = 1;
    public GameObject cycleFwdButton;
    public GameObject cycleBwdButton;

    // PC info pages
    [Header("Menus")]
    public GameObject razaMenu;
    public GameObject dorneMenu;
    public GameObject smithsonMenu;
    public GameObject zorMenu;

    void Update() {
        // turning menu off and on
        if (loadingScreenManager.loadingDone && !DialogueManager.InDialogue && !Inventory.invMenuOpen) {
            if (Input.GetKeyDown(KeyCode.E)) {
                menuOpen = !menuOpen;
                PlayerMovement.playerAbleMove = !PlayerMovement.playerAbleMove;
            }
        }

        // if the menu is on
        if (menuOpen) {
            // cycle through the menus
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                cycleForward();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                cycleBackwards();
            }
            //cycleFwdButton.SetActive(true);
            //cycleBwdButton.SetActive(true);
            if (currentPage == 1) {
                razaMenu.SetActive(true);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(false);
            }
            else if (currentPage == 2) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(true);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(false);
            }
            else if (currentPage == 3) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(true);
                zorMenu.SetActive(false);
            }
            else if (currentPage == 4) {
                razaMenu.SetActive(false);
                dorneMenu.SetActive(false);
                smithsonMenu.SetActive(false);
                zorMenu.SetActive(true);
            }
        }
        else {
            //cycleFwdButton.SetActive(false);
            //cycleBwdButton.SetActive(false);
            razaMenu.SetActive(false);
            dorneMenu.SetActive(false);
            smithsonMenu.SetActive(false);
            zorMenu.SetActive(false);
        }
    }

    // methods for cycling through the party info menu \\
    public void cycleForward() {
        currentPage++;
        if (currentPage > 4) {
            currentPage = 1;
        }
    }
    public void cycleBackwards() {
        currentPage--;
        if (currentPage < 1) {
            currentPage = 4;
        }
    }
}
