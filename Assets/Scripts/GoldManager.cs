using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefab;  // ������ ������

    private float _spawnChance;
    private float _spawnChanceDefault;
    private float _chanceIncrement;

    private IGoldSpawner _goldSpawner;

    private void Start()
    {
        _spawnChanceDefault = GameConfigProvider.instance.GameConfig.goldSpawnChance;
        _spawnChance = _spawnChanceDefault;
        _chanceIncrement = GameConfigProvider.instance.GameConfig.goldSpawnChanceIncrement;



        // ������� ��������� GoldSpawner � �������� � ���� ������ ������
        _goldSpawner = new GoldSpawner(goldPrefab);
        

        var cells = FindObjectsOfType<Cell>();
        foreach (var cell in cells)
        {
            cell.OnGoldDigged += TrySpawnGold;  // �������� �� ������� �����
        }
    }

    private void TrySpawnGold(Cell cell)
    {
        if (CheckSpawnChance())
        {
            Debug.Log("Gold find!!");
            _goldSpawner.SpawnGoldObject(cell);  // �������� ����� ������
            ResetSpawnChance();
        }
        else
        {
            Debug.Log("No gold :/. Spawn chance: " + _spawnChance);
            IncreaseSpawnChance();
        }
    }

    private bool CheckSpawnChance()
    {
        return UnityEngine.Random.Range(0f, 1f) <= _spawnChance;
    }

    private void IncreaseSpawnChance()
    {
        
        _spawnChance += _chanceIncrement;
        Debug.Log("Increase spawn chance. Now it: " + _spawnChance);
    }

    private void ResetSpawnChance()
    {
        _spawnChance = _spawnChanceDefault;
    }
}


