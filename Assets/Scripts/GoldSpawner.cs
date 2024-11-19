using UnityEngine;
using System;

public class GoldSpawner : IGoldSpawner
{

    public event Action<RewardItem, Transform> OnGoldSpawned;
    private GameObject _goldPrefab;

    public GoldSpawner(GameObject goldPrefab)
    {
        _goldPrefab = goldPrefab;
    }

    public void SpawnGoldObject(Transform cell)
    {
        if (_goldPrefab != null)
        {
            
            GameObject gold = UnityEngine.Object.Instantiate(_goldPrefab, cell.position, Quaternion.identity);
            gold.transform.SetParent(cell, false);

            RectTransform cellRectTransform = cell.GetComponent<RectTransform>();
            RectTransform goldRectTransform = gold.GetComponent<RectTransform>();
            RewardItem rewardItem = gold.GetComponent<RewardItem>();
            OnGoldSpawned?.Invoke(rewardItem, cell);

            if (cellRectTransform != null && goldRectTransform != null)
            {
                float scaleFactor = 0.5f; // Коэффициент размера золота, чтобы оно было меньше клетки (80% от размера)

                // Подгоняем золото под уменьшенный размер клетки
                goldRectTransform.sizeDelta = cellRectTransform.sizeDelta * scaleFactor;
                goldRectTransform.anchorMin = new Vector2(0.5f, 0.5f); // Центрируем золото
                goldRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                goldRectTransform.anchoredPosition = Vector2.zero; // Ставим золото по центру
            }
            Debug.Log("Hello from GoldSpawner! Gold was spawned");
        }
        else
        {
            Debug.LogWarning("Gold prefab is not assigned!");
        }
    }
}

