using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool pauseMenuOpen = false;
    // Update is called once per frame
    void Update()
    {
        // if the player presses the escape key, open the pause menu
        if (Input.GetKeyDown(KeyCode.Escape)) {
           pauseMenuOpen = !pauseMenuOpen;
           pauseMenu.SetActive(pauseMenuOpen);
           PlayerMovement.playerAbleMove = !pauseMenuOpen;
        } 
    }
}
