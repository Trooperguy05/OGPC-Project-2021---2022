using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsMenu;
    public bool controlsIsActive;

    // play the game
    public void playGame() {
        SceneLoader.changeScene = true;
        PartyManager.partyOrder = new string[] {"", "", "", ""};
    }

    // open the controls menu
    public void openControlsMenu()
    {
        controlsIsActive = !controlsIsActive;
        controlsMenu.SetActive(controlsIsActive);
    }

    // quit the game
    public void exitGame() {
        Application.Quit();
    }
}
