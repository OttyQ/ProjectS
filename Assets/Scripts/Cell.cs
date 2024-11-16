using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    private int _depth;
    private int _curDepth;
    private bool _CellOnGold = false;

    private Image _cellImage;
    public event Action<Cell> OnGoldDigged;

    private void Start()
    {
        _depth = GameConfigProvider.instance.GameConfig.maxDepth;
        _curDepth = _depth;
        _cellImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dig();
    }

    private void Dig()
    {
        if (_CellOnGold || _curDepth == 0) return;

        _curDepth--;
        Debug.Log("Current depth" + _depth);
        UpdateCellColor();
        OnGoldDigged?.Invoke(this);  // Сообщаем, что клетка была выкопана
    }

    private void UpdateCellColor()
    {
        if (_cellImage != null)
        {
            //заменить 3 на какой-нибудь maxDepth
            float brightness = Mathf.InverseLerp(0, _depth, _curDepth);
            _cellImage.color = new Color(brightness, brightness, brightness);
        }
    }

    public void SetGold()
    {
        _CellOnGold = true;
        Debug.Log("Gold is now in this cell!");
    }
}

