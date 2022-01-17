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

    // enum for tracking for party lead
    public enum PartyLead {
        Raza,
        Dorne,
        Smithson,
        Zor,
    }
    public PartyLead leader = PartyLead.Raza;
    private Animator playerAnimator;
    private PlayerMovement pM;

    // animators for cool portraits \\
    [Header("Portrait Animators")]
    public Animator char1Animator;
    public Animator char2Animator;
    public Animator char3Animator;
    public Animator char4Animator;

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
        // player animator
        playerAnimator = player.GetComponent<Animator>();
        // player movement script
        pM = player.GetComponent<PlayerMovement>();

        // loading the party stats data
        FindObjectOfType<PartyStats>().LoadData();
        // loading the player progress data
        FindObjectOfType<PlayerProgress>().loadPlayerData();

        // update player sprite
        updatePlayerSprite();
    }
    
    // checking for player input
    void Update() {
        // if the player presses tab, open the party menu
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (!FindObjectOfType<PauseMenu>().pauseMenuOpen) { // making sure the pause menu isn't open before opening party wheel
                Debug.Log(partyOrder[0] + " " + partyOrder[1] + " " + partyOrder[2] + " " + partyOrder[3]);
                // open the menu
                partyTabOpen = !partyTabOpen;
                partyMenu.SetActive(partyTabOpen);
                // activate the animations for the portraits
                char1Animator.SetBool("animationOn", !char1Animator.GetBool("animationOn"));
                char2Animator.SetBool("animationOn", !char2Animator.GetBool("animationOn"));
                char3Animator.SetBool("animationOn", !char3Animator.GetBool("animationOn"));
                char4Animator.SetBool("animationOn", !char4Animator.GetBool("animationOn"));
                // disable the player movement
                PlayerMovement.playerAbleMove = !partyTabOpen;
                // update player sprite
                updatePlayerSprite();
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
                updateWheel(character1RT, character1Order, "Raza");
                updateWheel(character2RT, character2Order, "Dorne");
                updateWheel(character3RT, character3Order, "Smithson");
                updateWheel(character4RT, character4Order, "Zor");
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
        playerAnimator.speed = 1f;
        if (partyOrder[0] == "Raza") {
            leader = PartyLead.Raza;
            playerAnimator.SetBool("razaLeader", true);
            playerAnimator.SetBool("dorneLeader", false);
            playerAnimator.SetBool("smithsonLeader", false);
            playerAnimator.SetBool("zorLeader", false);
        }
        else if (partyOrder[0] == "Dorne") {
            leader = PartyLead.Dorne;
            playerAnimator.SetBool("razaLeader", false);
            playerAnimator.SetBool("dorneLeader", true);
            playerAnimator.SetBool("smithsonLeader", false);
            playerAnimator.SetBool("zorLeader", false);
        }
        else if (partyOrder[0] == "Smithson") {
            leader = PartyLead.Smithson;
            playerAnimator.SetBool("razaLeader", false);
            playerAnimator.SetBool("dorneLeader", false);
            playerAnimator.SetBool("smithsonLeader", true);
            playerAnimator.SetBool("zorLeader", false);
        }
        else if (partyOrder[0] == "Zor") {
            leader = PartyLead.Zor;
            playerAnimator.SetBool("razaLeader", false);
            playerAnimator.SetBool("dorneLeader", false);
            playerAnimator.SetBool("smithsonLeader", false);
            playerAnimator.SetBool("zorLeader", true);
        }
        pM.updateIdleSprite();
    }

    // updates the partyWheel after loading a save \\
    public void updatePartyWheel() {
        updateWheel(character1RT, character1Order, "Raza");
        updateWheel(character2RT, character2Order, "Dorne");
        updateWheel(character3RT, character3Order, "Smithson");
        updateWheel(character4RT, character4Order, "Zor");
    }

    // delets player and party save \\
    public void deleteSave() {
        Debug.Log("Deleting Save Data");
        SaveSystem.deleteSaveData();
    }


}
