using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConsoleAndAchievementsController : MonoBehaviour
{
    // Declaring Varibles \\
    public GameObject console;
    public bool consoleIsActive;
    public string consoleText;
    public GameObject inputField;
    public GameObject consoleLog;
    public GameObject achievements;
    public static bool achievementsIsActive;
    public GameObject player;
    private achievements achs;

    // caching
    void Start() {
        achs = achievements.GetComponent<achievements>();
    }

    void Update() {
        // Opens and closes the console \\
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            consoleIsActive = !consoleIsActive;
            console.SetActive(consoleIsActive); 
            // player cannot move while console menu is open
            PlayerMovement.playerAbleMove = !consoleIsActive;
        }
    }

    // Stores the input of the text box \\
    public void storeInput() {
        consoleText = inputField.GetComponent<TMP_InputField>().text;
        checkCommand();
    }

    // Checks if a valid command has been entered \\
    private void checkCommand() {
        if (SceneManager.GetActiveScene().name == "Template Project") {
            achs.updateCrocSpin();
        }
        // Forces the stored input into full uppercase
        consoleText = consoleText.ToUpper();
        Debug.Log(consoleText);
        // Displays the help menu for the console in the console log
        if (consoleText.Equals("HELP")) {
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\n-----------------------------------------------------------\n'switchscene' - switches the scene (must input scene name).\n'healparty' - heals all party members.\n'endcombat' - ends the current combat.\n'help' - shows this menu.\n'goto' - moves the player character to a location (e.g temple, well)\ncommands are not case sensitive.\n-----------------------------------------------------------";
        }
        // Switchs the scene based on what scene name is entered with the commands
        else if (consoleText == "SWITCHSCENE MAINMENU") {
            SceneManager.LoadScene(0);
        }
        else if (consoleText == "SWITCHSCENE OVERWORLD") {
            SceneManager.LoadScene(1);
        }
        else if (consoleText == "SWITCHSCENE COMBAT") {
            SceneManager.LoadScene(2);
        }
        else if (consoleText == "SWITCHSCENE CREDITS") {
            SceneManager.LoadScene(4);
        }
        // Teleports the player to important locations
        else if (consoleText == "GOTO DESERT") {
            player.transform.position = new Vector2(-0.45f, 0f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to desert";
        }
        else if (consoleText == "GOTO DESERT TEMPLE") {
            player.transform.position = new Vector2(-18, -57);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to desert temple";
        }
        else if (consoleText == "GOTO DESERT WELL") {
            player.transform.position = new Vector2(4.8f, -86.2f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to desert well";
        }
        else if (consoleText == "GOTO SWAMP") {
            player.transform.position = new Vector2(36.87f, -86.2f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to swamp";   
        }
        else if (consoleText == "GOTO SWAMP TEMPLE") {
            player.transform.position = new Vector2(74.35f, -89.77f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to swamp temple";
        }
        else if (consoleText == "GOTO SWAMP WELL") {
            player.transform.position = new Vector2(55.36f, -108.4f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to swamp well";
        }
        else if (consoleText == "GOTO FOREST") {
            player.transform.position = new Vector2(88.05f, -119.3f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to forest";
        }
        else if (consoleText == "GOTO FOREST TEMPLE") {
            player.transform.position = new Vector2(61.31f, -166.67f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to forest temple";
        }
        else if (consoleText == "GOTO FOREST WELL") {
            player.transform.position = new Vector2(54.92f, -129.9f);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to forest well";
        }
        // Ends combat
        else if (consoleText == "ENDCOMBAT" && SceneManager.GetActiveScene().name == "CombatScene") {
            SceneLoader.changeScene = true;
        }
        // skips turns in combat
        else if (consoleText == "SKIPTURN" && SceneManager.GetActiveScene().name == "CombatScene") {
            GameObject.Find("Combat Manager").GetComponent<CombatManager>().initiativeIndex++;
        }
        // Displays that an invalid command is entered in the console
        else {
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\ninvalid command issued to the console";
        }
    }

    //Opens and closes the achievements menu \\
    public void toggleAchievementsMenu() {
        achievementsIsActive = !achievementsIsActive;
        achievements.SetActive(achievementsIsActive);
    }
}