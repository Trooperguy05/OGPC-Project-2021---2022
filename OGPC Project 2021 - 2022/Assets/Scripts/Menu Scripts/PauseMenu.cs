using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // pause menu variables 
    public GameObject pauseMenu;
    public bool pauseMenuOpen = false;
    // Update is called once per frame
    void Update()
    {
        // if the player presses the escape key, open the pause menu
        if (!PartyManager.partyTabOpen && !DialogueManager.InDialogue && loadingScreenManager.loadingDone && !achievementListManager.listOpen) {
           if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1)) {
                pauseMenuOpen = !pauseMenuOpen;
                pauseMenu.SetActive(pauseMenuOpen);
                PlayerMovement.playerAbleMove = !pauseMenuOpen;
           }
        } 
    }

    // method for main menu button (returns player back to main menu)
    public void returnToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
