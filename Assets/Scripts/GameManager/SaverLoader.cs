using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaverLoader : MonoBehaviour
{
    private string SavePath => Path.Combine(Application.persistentDataPath, "savegame.json");

    public void SaveGame(GameSaveData saveData)
    {
        Debug.Log("Запись в save");
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(SavePath, json);
    }

    public GameSaveData LoadGame()
    {
        Debug.Log("Загрузка сохранения");
        if (!File.Exists(SavePath))
        {
            Debug.Log("Net faila!");
            return null;
        }
        Debug.Log("Файл найден!");
        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Файл сохранения удалён.");
        }
        else
        {
            Debug.Log("Файл сохранения не найден.");
        }
    }

}
