using UnityEngine;
using System;
using UnityEngine.UI;

public class GoldSpawner : IGoldSpawner
{

    //public event Action<RewardItem, Transform> OnGoldSpawned;
    private GameObject _goldPrefab;

    public GoldSpawner(GameObject goldPrefab)
    {
        _goldPrefab = goldPrefab;
    }

    public void SpawnGoldObject(Transform cell)
    {
        if (_goldPrefab != null)
        {
            Debug.Log("GoldSpawner start gold spawn! Transform cell: " + cell);

            // ������� ������ �� ������� ������ (���������� ������� �������)
            GameObject gold = UnityEngine.Object.Instantiate(_goldPrefab, cell.position, Quaternion.identity);

            // �������� ������ ��� �������� ������ ������, ����� ��� ��������� �� ���
            gold.transform.SetParent(cell); // ����������� ������ � ������, ����� ��� ��������� ������ � ���

            // ���� �� �� ������ ������������ RectTransform ��� ������� � �������������, ������ ������������ ������� Transform:
            // ������������� ������������ ������ ������
            gold.transform.localScale = Vector3.one * 0.5f; // ��������, ���������� ������� �� 50% �� ������������ �������

            Debug.Log("Gold spawned at position: " + gold.transform.position);
        }
        else
        {
            Debug.LogWarning("Gold prefab is not assigned!");
        }
    }


}

