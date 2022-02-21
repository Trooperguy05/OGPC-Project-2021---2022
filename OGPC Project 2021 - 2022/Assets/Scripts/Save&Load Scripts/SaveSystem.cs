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

    // delete save data \\
    public static void deleteSaveData() {
        string partyDataPath = Application.persistentDataPath + "/partyData.txt";
        string playerDataPath = Application.persistentDataPath + "/playerData.txt";
        string enemyDataPath = Application.persistentDataPath + "/activeEnemies.txt";
        string invDataPath = Application.persistentDataPath + "/inventory.txt";
        string achivementDataPath = Application.persistentDataPath + "/achievements.txt";

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
        // Delete the achivement data
        if (File.Exists(achivementDataPath)) {
            File.Delete(achivementDataPath);
        }
        else {
            Debug.LogError("File not found in " + achivementDataPath);
        }
    }
}
