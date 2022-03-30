using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    // save the party stats \\
    public static void SavePartyStats(PartyStats partyStats) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/partyData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PartyData data = new PartyData(partyStats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load the party stats \\
    public static PartyData LoadPartyStats() {
        string path = Application.persistentDataPath + "/partyData.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PartyData data = formatter.Deserialize(stream) as PartyData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    // save player progress \\
    public static void SavePlayerProgress(PlayerProgress playerProgress) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerProgress);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load player progress \\
    public static PlayerData LoadPlayerProgress() {
        string path = Application.persistentDataPath + "/playerData.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    // save active enemies \\
    public static void SaveActiveEnemies(OpenWorldEnemyManager eM) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/activeEnemies.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        OverworldEnemyData data = new OverworldEnemyData(eM);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load active enemies \\
    public static OverworldEnemyData LoadActiveEnemies() {
        string path = Application.persistentDataPath + "/activeEnemies.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OverworldEnemyData data = formatter.Deserialize(stream) as OverworldEnemyData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    // saving specified enemy \\
    public static void SaveSpecifiedEnemy(OpenWorldEnemy oE) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/specifiedEnemy.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        SpecifiedEnemyData data = new SpecifiedEnemyData(oE);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // loading specified enemy \\
    public static SpecifiedEnemyData LoadSpecifiedEnemy() {
        string path = Application.persistentDataPath + "/specifiedEnemy.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SpecifiedEnemyData data = formatter.Deserialize(stream) as SpecifiedEnemyData;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    // saving the player inventory \\
    public static void SavePlayerInventory(Inventory inv) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inv);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load the player inventory \\
    public static InventoryData LoadPlayerInventory() {
        string path = Application.persistentDataPath + "/inventory.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;
            stream.Close();
            
            return data;
        }
        else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    // saving the achievement data \\
    public static void saveAchievements(achievements ach) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/achievements.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        achievementsData data = new achievementsData(ach);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // loading the achievement data \\
    public static achievementsData loadAchievements() {
        string path = Application.persistentDataPath + "/achievements.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            achievementsData data = formatter.Deserialize(stream) as achievementsData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("SaveFile not found in " + path);
            return null;
        }
    }

    // save pickup item data \\
    public static void saveItemPickups(itemPickupManager iPM) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/itemPickups.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        ItemData data = new ItemData(iPM);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load pickup item data \\
    public static ItemData loadItemPickups() {
        string path = Application.persistentDataPath + "/itemPickups.txt";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ItemData data = formatter.Deserialize(stream) as ItemData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("SaveFile not found in " + path);
            return null;
        }
    }

    // save story dialogue data \\
    public static void saveStoryDialogue(StoryDialogueManager sDM) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/storyDialogue.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        StoryDialogueData data = new StoryDialogueData(sDM);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load the story dialogue data \\
    public static StoryDialogueData loadStoryDialogue() {
        string path = Application.persistentDataPath + "/storyDialogue.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            StoryDialogueData data = formatter.Deserialize(stream) as StoryDialogueData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("Save file does not exist at " + path);
            return null;
        }
    }

    // save combat report data \\
    public static void saveCombatReport(CombatReport cR) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/combatReport.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        CombatReportData data = new CombatReportData(cR);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // load combat report data \\
    public static CombatReportData loadCombatReport() {
        string path = Application.persistentDataPath + "/combatReport.txt";

        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CombatReportData data = formatter.Deserialize(stream) as CombatReportData;
            stream.Close();
            return data;
        }
        else {
            Debug.LogError("Save file does not exist at " + path);
            return null;
        }
    }

    // delete save data \\
    public static void deleteSaveData() {
        string partyDataPath = Application.persistentDataPath + "/partyData.txt";
        string playerDataPath = Application.persistentDataPath + "/playerData.txt";
        string enemyDataPath = Application.persistentDataPath + "/activeEnemies.txt";
        string invDataPath = Application.persistentDataPath + "/inventory.txt";
        string achivementDataPath = Application.persistentDataPath + "/achievements.txt";
        string storyDialoguePath = Application.persistentDataPath + "/storyDialogue.txt";
        string itemDataPath = Application.persistentDataPath + "/itemPickups.txt";
        string combatReportPath = Application.persistentDataPath + "/combatReport.txt";

        // delete the party data
        if (File.Exists(partyDataPath)) {
            File.Delete(partyDataPath);
        }
        else {
            Debug.LogError("File not found in " + partyDataPath);
        }
        // delete the player data
        if (File.Exists(playerDataPath)) {
            File.Delete(playerDataPath);
        }
        else {
            Debug.LogError("File not found in " + playerDataPath);
        }
        // delete active enemy data
        if (File.Exists(enemyDataPath)) {
            File.Delete(enemyDataPath);
        }
        else {
            Debug.LogError("File not found in " + enemyDataPath);
        }
        // delete inventory data
        if (File.Exists(invDataPath)) {
            File.Delete(invDataPath);
        }
        else {
            Debug.LogError("File not found in " + invDataPath);
        }
        // delete story dialogue data
        if (File.Exists(storyDialoguePath)) {
            File.Delete(storyDialoguePath);
        }
        else {
            Debug.LogError("File not found in " + storyDialoguePath);
        }
        // delete item pickup data
        if (File.Exists(itemDataPath)) {
            File.Delete(itemDataPath);
        }
        else {
            Debug.LogError("File not found in " + itemDataPath);
        }
        // delete combat report data
        if (File.Exists(combatReportPath)) {
            File.Delete(combatReportPath);
        }
        else {
            Debug.LogError("File not found in " + combatReportPath);
        }
        /*
        // Delete the achivement data
        if (File.Exists(achivementDataPath)) {
            File.Delete(achivementDataPath);
        }
        else {
            Debug.LogError("File not found in " + achivementDataPath);
        }
        */
    }
}
