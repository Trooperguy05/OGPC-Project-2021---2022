using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static bool changeScene = false;
    // load a scene based on where the player is
    void Start()
    {
        changeScene = false;
        if (SceneManager.GetActiveScene().name == "Template Project") {
            StartCoroutine(loadCombatScene());
        }
        else if (SceneManager.GetActiveScene().name == "CombatScene") {
            StartCoroutine(loadOverworldScene());
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu") {
            StartCoroutine(loadOverworldScene());
        }
    }

    // load the main menu
    public IEnumerator loadMainMenu() {
        AsyncOperation aO = SceneManager.LoadSceneAsync("MainMenu");
        aO.allowSceneActivation = false;

        while (!aO.isDone) {
            if (changeScene) {
                aO.allowSceneActivation = true;
            }
            yield return null;
        }  
    }

    // load the combat scene
    public IEnumerator loadCombatScene() {
        AsyncOperation aO = SceneManager.LoadSceneAsync("CombatScene");
        aO.allowSceneActivation = false;

        while (!aO.isDone) {
            if (changeScene) {
                aO.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    // load the overworld scene
    public IEnumerator loadOverworldScene() {
        AsyncOperation aO = SceneManager.LoadSceneAsync("Template Project");
        aO.allowSceneActivation = false;

        while (!aO.isDone) {
            if (changeScene) {
                aO.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
