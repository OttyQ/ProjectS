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

            // —оздаем золото на позиции клетки (используем мировую позицию)
            GameObject gold = UnityEngine.Object.Instantiate(_goldPrefab, cell.position, Quaternion.identity);

            // Ќастроим золото как дочерний объект клетки, чтобы оно следовало за ней
            gold.transform.SetParent(cell); // ѕрикрепл€ем золото к клетке, чтобы оно двигалось вместе с ней

            // ≈сли ты не хочешь использовать RectTransform дл€ размера и центрировани€, можешь использовать обычный Transform:
            // ”станавливаем произвольный размер золота
            gold.transform.localScale = Vector3.one * 0.5f; // Ќапример, уменьшение размера до 50% от стандартного размера

            Debug.Log("Gold spawned at position: " + gold.transform.position);
        }
        else
        {
            Debug.LogWarning("Gold prefab is not assigned!");
        }
    }


}

