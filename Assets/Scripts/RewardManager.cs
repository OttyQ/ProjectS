using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{

    private float _spawnChance;
    private float _spawnChanceDefault;
    private float _chanceIncrement;

    private IGoldSpawner _goldSpawner;

    public void Initialize(float spawnChance, float chanceIncrement, GameObject goldPrefab)
    {
        _spawnChanceDefault = spawnChance;
        _spawnChance = _spawnChanceDefault;
        _chanceIncrement = chanceIncrement;
        // Создаем экземпляр GoldSpawner и передаем в него префаб золота
        _goldSpawner = new GoldSpawner(goldPrefab);
    }

    private void OnDisable()
    {
        UnSubscribeToCellEvents(FindObjectsOfType<Cell>());
    }
    public void SubscribeToCellEvents(Cell[] cells)
    {
        foreach (var cell in cells)
        {
            cell.OnGoldDigged += TrySpawnGold;  // Подписка на событие копки 
           
        }
        Debug.Log("Subscribe good from RewManager!");
    }

    public void UnSubscribeToCellEvents(Cell[] cells)
    {
        foreach (var cell in cells)
        {
            cell.OnGoldDigged -= TrySpawnGold;  // Отписка от событие копки
        }
    }
    private bool TrySpawnGold(Transform cell)
    {
        if (CheckSpawnChance())
        {
            _goldSpawner.SpawnGoldObject(cell);  // Вызываем спавн золота
            ResetSpawnChance();
            return true;
        }
        IncreaseSpawnChance();
        return false; 
    }

    private bool CheckSpawnChance()
    {
        return UnityEngine.Random.Range(0f, 1f) <= _spawnChance;
    }

    private void IncreaseSpawnChance()
    {
        _spawnChance += _chanceIncrement;
        Debug.Log("Chance to spawn increase! It now: " + _spawnChance);
    }

    private void ResetSpawnChance()
    {
        _spawnChance = _spawnChanceDefault;
        Debug.Log("Reset spawn chance to def! It now: " + _spawnChance);
    }

    public void HandleGoldSpawn(Transform cell)
    {
        Debug.Log("Handle spawn gold!");
        _goldSpawner.SpawnGoldObject(cell);
    }
}


