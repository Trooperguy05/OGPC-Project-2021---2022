using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsMenu;
    public bool controlsIsActive;

    public void playGame() {
        SceneManager.LoadScene(1);
    }

    public void openControlsMenu()
    {
        controlsIsActive = !controlsIsActive;
        controlsMenu.SetActive(controlsIsActive);
    }
}
