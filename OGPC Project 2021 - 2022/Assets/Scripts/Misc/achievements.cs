using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achievements : MonoBehaviour
{
    // Achivement Bools \\
    public bool breathOfTheWilderness = false;
    public bool timeWaster = false;
    public bool damageTaker = false;
    public bool protocall3ProtectThePilot = false;
    public bool combatMedic = false;
    public bool pausePwner = false;
    public bool fileRemover = false;
    public bool phylacteryDown = false;
    public bool ancientWhispers = false;
    public bool phylacteriesDown = false;
    public bool damage = false;
    public bool slappedTheDragon = false;
    public bool crocSpin = false;
    public bool luckOfTheDraw = false;
    public bool missedTheirEyes = false;
    public bool hadToDoItToEm = false;
    public bool exterminatus = false;
    public bool gotANewRoach = false;
    public bool isThisAllowed = false;

    // Achievement counters \\
    public int pausePwnerCount;
    
    void Start() {
        loadAchievementData();
    }

    void Update() {
        // Checks if the player pressed the escape key
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pausePwnerCount++;
            if (pausePwnerCount >= 60 && !pausePwner) {
                pausePwner = true;
            }
        }
    }

    public void fileRemoverCheck() {
        fileRemover = true;
    }

    // Methods for saving and loading achievement data \\
    public void savedAchievementData() {
        Debug.Log("Saving achievement data");
        SaveSystem.saveAchievements(this);
    }
    
    public void loadAchievementData() {
        Debug.Log("Loading achievement data");
        achievementsData data = SaveSystem.loadAchievements();
        if (data != null) {
            breathOfTheWilderness = data.breathOfTheWilderness;
            timeWaster = data.timeWaster;
            damageTaker = data.damageTaker;
            protocall3ProtectThePilot = data.protocall3ProtectThePilot;
            combatMedic = data.combatMedic;
            pausePwner = data.pausePwner;
            pausePwnerCount = data.pausePwnerCount;
            fileRemover = data.fileRemover;
            phylacteryDown = data.phylacteryDown;
            ancientWhispers = data.ancientWhispers;
            phylacteriesDown = data.phylacteriesDown;
            damage = data.damage;
            slappedTheDragon = data.slappedTheDragon;
            crocSpin = data.crocSpin;
            luckOfTheDraw = data.luckOfTheDraw;
            missedTheirEyes = data.missedTheirEyes;
            hadToDoItToEm = data.hadToDoItToEm;
            exterminatus = data.exterminatus;
            gotANewRoach = data.gotANewRoach;
            isThisAllowed = data.isThisAllowed;

        }
    }
    
}
