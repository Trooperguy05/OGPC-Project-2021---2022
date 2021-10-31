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
}
