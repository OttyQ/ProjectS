using UnityEngine;
using System;

public class GoldSpawner : IGoldSpawner
{
    private GameObject _goldPrefab;

    public GoldSpawner(GameObject goldPrefab)
    {
        _goldPrefab = goldPrefab;
    }

    public void SpawnGoldObject(Cell cell)
    {
        if (_goldPrefab != null)
        {
            //������ ���������� ����� ������� (������������ ��� ������ ������)
            GameObject gold = UnityEngine.Object.Instantiate(_goldPrefab, cell.transform.position, Quaternion.identity);
            gold.transform.SetParent(cell.transform);
            cell.SetGold();  // ��������� ��������� ������, ��� � ��� ���� ������
            Debug.Log("Hello from GoldSpawner! Gold was spawned");
        }
        else
        {
            Debug.LogWarning("Gold prefab is not assigned!");
        }
    }
}

