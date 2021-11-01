using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    // party order
    public static string[] partyOrder = new string[] {" ", " ", " ", " "};

    // visual variables
    [Header("Party Menu Logic")]
    public GameObject partyMenu;
    public GameObject player;
    public bool partyTabOpen = false;
    private SpriteRenderer playerSR;

    // rotating the party "tagging in" visual
    [Header("Rotation Visual")]
    public int imageOffset = 125;

    // visual objects for the "tagging in" system of party order
    [Header("Party Visuals")]
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;
    // the character UI objects' Rect Transforms
    private RectTransform character1RT;
    private RectTransform character2RT;
    private RectTransform character3RT;
    private RectTransform character4RT;

    // character sprites
    [Header("Party Member Sprites")]
    public Sprite character1SP; // character: Astar
    public Sprite character2SP; // character: Gor
    public Sprite character3SP; // character: Gadriel
    public Sprite character4SP; // character: Lyra

    // variables for tracking party member order
    public static int character1Order = 0;
    public static int character2Order = 1;
    public static int character3Order = 2;
    public static int character4Order = 3;

    // positions for the character visuals
    private Vector3 leadPosition;
    private Vector3 secondPosition;
    private Vector3 thirdPosition;
    private Vector3 fourthPosition;

    // variable to make sure some actions are only called once
    private bool partyOrderChanged = false;

    // caching variables
    void Start() {
        // caching the positions for the character visuals
        leadPosition = new Vector3(player.transform.position.x, player.transform.position.y + imageOffset, 0f);
        secondPosition = new Vector3(player.transform.position.x + imageOffset, player.transform.position.y, 0f);
        thirdPosition = new Vector3(player.transform.position.x, player.transform.position.y - imageOffset, 0f);
        fourthPosition = new Vector3(player.transform.position.x - imageOffset, player.transform.position.y, 0f);

        // caching the RectTransforms of the character UI elements
        character1RT = character1.GetComponent<RectTransform>();
        character2RT = character2.GetComponent<RectTransform>();
        character3RT = character3.GetComponent<RectTransform>();
        character4RT = character4.GetComponent<RectTransform>();

        // player sprite renderer
        playerSR = player.GetComponent<SpriteRenderer>();

        // loading the party stats data
        FindObjectOfType<PartyStats>().LoadData();
        // loading the player progress data
        FindObjectOfType<PlayerProgress>().loadPlayerData();
    }
    
    // checking for player input
    void Update() {
        // if the player presses tab, open the party menu
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!FindObjectOfType<PauseMenu>().pauseMenuOpen) { // making sure the pause menu isn't open before opening party wheel
                partyTabOpen = !partyTabOpen;
                partyMenu.SetActive(partyTabOpen);
                PlayerMovement.playerAbleMove = !partyTabOpen;
            }
        }

        // if the party tab is open
        if (partyTabOpen) {
            // move the party menu with the player
            partyMenu.transform.position = player.transform.position;

            // move the characters in clockwise order
            if (Input.GetKeyDown(KeyCode.D)) {
                character1Order++;
                character2Order++;
                character3Order++;
                character4Order++;

                // making sure the order is within bounds
                if (character1Order == 4) {
                    character1Order = 0;
                }
                if (character2Order == 4) {
                    character2Order = 0;
                }
                if (character3Order == 4) {
                    character3Order = 0;
                }
                if (character4Order == 4) {
                    character4Order = 0;
                }
                partyOrderChanged = true;
            }
            // move the characters in counterclockwise order
            else if (Input.GetKeyDown(KeyCode.A)) {
                character1Order--;
                character2Order--;
                character3Order--;
                character4Order--;

                // making sure the order is within bounds
                if (character1Order == -1) {
                    character1Order = 3;
                }
                if (character2Order == -1) {
                    character2Order = 3;
                }
                if (character3Order == -1) {
                    character3Order = 3;
                }
                if (character4Order == -1) {
                    character4Order = 3;
                }
                partyOrderChanged = true;
            }

            if (partyOrderChanged) {
                // updating the wheel visual
                updateWheel(character1RT, character1Order, "Astar");
                updateWheel(character2RT, character2Order, "Gor");
                updateWheel(character3RT, character3Order, "Gadriel");
                updateWheel(character4RT, character4Order, "Lyra");
                updatePlayerSprite();
                partyOrderChanged = false;
            }
        }
    }
    
    // updates the character visual on the wheel
    public void updateWheel(RectTransform characterRT, int order, string name) {
        if (order == 0) {
            characterRT.anchoredPosition = leadPosition;
        }
        else if (order == 1) {
            characterRT.anchoredPosition = secondPosition;
        }
        else if (order == 2) {
            characterRT.anchoredPosition = thirdPosition;
        }
        else if (order == 3) {
            characterRT.anchoredPosition = fourthPosition;
        }

        partyOrder[order] = name;
    }

    // function that updates the player sprite to be that of the lead member's sprite
    public void updatePlayerSprite() {
        if (partyOrder[0] == "Astar") {
            playerSR.sprite = character1SP;
        }
        else if (partyOrder[0] == "Gor") {
            playerSR.sprite = character2SP;
        }
        else if (partyOrder[0] == "Gadriel") {
            playerSR.sprite = character3SP;
        }
        else if (partyOrder[0] == "Lyra") {
            playerSR.sprite = character4SP;
        }
    }

    // updates the partyWheel after loading a save \\
    public void updatePartyWheel() {
        updateWheel(character1RT, character1Order, "Astar");
        updateWheel(character2RT, character2Order, "Gor");
        updateWheel(character3RT, character3Order, "Gadriel");
        updateWheel(character4RT, character4Order, "Lyra");
    }

    // delets player and party save \\
    public void deleteSave() {
        Debug.Log("Deleting Save Data");
        SaveSystem.deleteSaveData();
    }
}
