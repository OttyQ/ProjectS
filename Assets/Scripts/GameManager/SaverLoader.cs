using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverLoader
{
    private CountHandler countHandler;

    public SaverLoader(CountHandler countHandler)
    {
        this.countHandler = countHandler;
    }

    public void SaveGame()
    {
        // Сохранение данных countHandler
    }

    public void LoadGame()
    {
        // Загрузка данных countHandler
    }
}
