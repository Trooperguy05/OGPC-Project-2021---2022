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

    // delete save data \\
    public static void deleteSaveData() {
        string partyDataPath = Application.persistentDataPath + "/partyData.txt";
        string playerDataPath = Application.persistentDataPath + "/playerData.txt";
        string enemyDataPath = Application.persistentDataPath + "/activeEnemies.txt";

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
    }
}
