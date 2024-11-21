using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    public void SaveGame(GameSaveData saveData)
    {
        Debug.Log("Write to save");
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SavePath, json);
    }

    public GameSaveData LoadGame()
    {
        Debug.Log("Loading a save...");
        if (!File.Exists(SavePath))
        {
            Debug.Log("File not found!");
            return null;
        }
        Debug.Log("File found!");
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("The save file was deleted.");
        }
        else
        {
            Debug.Log("File not found!");
        }
    }

}
