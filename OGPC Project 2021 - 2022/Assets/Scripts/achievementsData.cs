using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class achievementsData 
{
    public bool breathOfTheWilderness = false;
    public bool timeWaster = false;
    public bool damageTaker = false;
    public bool protocall3ProtectThePilot = false;
    public bool combatMedic = false;
    public bool pauserPwner = false;
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
    
    public achievementsData(achievements ach) {
        breathOfTheWilderness = ach.breathOfTheWilderness;
        timeWaster = ach.timeWaster;
        damageTaker = ach.damageTaker;
        protocall3ProtectThePilot = ach.protocall3ProtectThePilot;
        combatMedic = ach.combatMedic;
        pauserPwner = ach.pauserPwner;
        fileRemover = ach.fileRemover;
        phylacteryDown = ach.phylacteryDown;
        ancientWhispers = ach.ancientWhispers;
        phylacteriesDown = ach.phylacteriesDown;
        damage = ach.damage;
        slappedTheDragon = ach.slappedTheDragon;
        crocSpin = ach.crocSpin;
        luckOfTheDraw = ach.luckOfTheDraw;
        missedTheirEyes = ach.missedTheirEyes;
        hadToDoItToEm = ach.hadToDoItToEm;
        exterminatus = ach.exterminatus;
        gotANewRoach = ach.gotANewRoach;
    }
    
}
