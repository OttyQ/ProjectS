using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{

    public event Func<Transform, bool> OnGoldDigged;
    
    public event Action OnDigged;

    private int _depth;
    private int _curDepth;
    private bool _hasGold = false;

    private RewardItem _rewardItemOnCell;
    private CellRenderer _cellRenderer;
    private CountHandler _countHandler;


    public void Initialize(int maxDepth,int curDepth ,CountHandler countHandler, bool hasGold)
    {
        _depth = maxDepth;
        _curDepth = curDepth;
        _hasGold = hasGold;
        _countHandler = countHandler;
        _cellRenderer = GetComponent<CellRenderer>();
        _cellRenderer?.Initialize(_depth, _curDepth);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dig();
    }

    private void Start()
    {
        _cellRenderer?.UpdateColor(_curDepth);

    }
    private void OnDisable()
    {
        if (_rewardItemOnCell != null)
        {
            _rewardItemOnCell.onGoldSpawned -= GoldAppear;
            _rewardItemOnCell.onGoldRemoved -= GoldRemoved;
        }
    }

    private void Dig()
    {
        if (_hasGold || _curDepth == 0 || _countHandler.GetRemainingShovels() == 0) return;
        _curDepth--;
        Debug.Log("Current depth" + _curDepth);
        _cellRenderer?.UpdateColor(_curDepth);
        
        if ((bool)(OnGoldDigged?.Invoke(this.transform)))// Сообщаем, что клетка была выкопана
        {
            AssignGold();
        }
        OnDigged?.Invoke();
    }

    
    private void GoldRemoved()
    {
        
        _hasGold = false;
        Debug.Log("Золото было убрано!");
    }
    
    private void GoldAppear()
    {
        _hasGold = true;
        Debug.Log("Золото появилось на клетке!");
    }

    public bool HasGold()
    {
        return _hasGold;
    }

    public int GetDepth()
    {
        return _curDepth;
    }

    public void AssignGold()
    {
        _rewardItemOnCell = GetComponentInChildren<RewardItem>();
        if (_rewardItemOnCell != null)
        {
            _rewardItemOnCell.onGoldSpawned += GoldAppear;
            _rewardItemOnCell.onGoldRemoved += GoldRemoved;
        }
    }
}

