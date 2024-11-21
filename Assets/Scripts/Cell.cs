using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public event Func<Transform, bool> GoldDigged; //Событие при попытке выкопать золото
    public event Action OnCellDigged; //Событие при копке клетки

    private int _maxDepth;
    private int _currentDepth;
    private bool _hasGold;
    
    private RewardItem rewardItemOnCell;
    private CellRenderer cellRenderer;
    private CountHandler countHandler;

    public void Initialize(int maxDepth, int currentDepth, CountHandler countHandler, bool hasGold)
    {
        _maxDepth = maxDepth;
        _currentDepth = currentDepth;
        _hasGold = hasGold;
        this.countHandler = countHandler;

        cellRenderer = GetComponent<CellRenderer>();
        cellRenderer?.Initialize(maxDepth, currentDepth);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Dig();
    }

    private void Start()
    {
        cellRenderer?.UpdateColor(_currentDepth);
    }

    private void OnDisable()
    {
        UnsubscribeFromRewardEvents();
    }

    private void Dig()
    {
        if (!CanDig()) return;

        _currentDepth--;
        Debug.Log($"Current depth: {_currentDepth}");
        cellRenderer?.UpdateColor(_currentDepth);

        if (GoldDigged?.Invoke(transform) == true)
        {
            AssignGold();
        }

        OnCellDigged?.Invoke();
    }

    private bool CanDig()
    {
        return !_hasGold && _currentDepth > 0 && countHandler.GetRemainingShovels() > 0;
    }

    public void AssignGold()
    {
        rewardItemOnCell = GetComponentInChildren<RewardItem>();
        if (rewardItemOnCell != null)
        {
            rewardItemOnCell.onGoldSpawned += GoldSpawned;
            rewardItemOnCell.onGoldRemoved += GoldRemoved;
        }
    }

    private void UnsubscribeFromRewardEvents()
    {
        if (rewardItemOnCell != null)
        {
            rewardItemOnCell.onGoldSpawned -= GoldSpawned;
            rewardItemOnCell.onGoldRemoved -= GoldRemoved;
        }
    }

    private void GoldRemoved()
    {
        _hasGold = false;
        Debug.Log("Gold was removed from the cell!");
    }

    private void GoldSpawned()
    {
        _hasGold = true;
        Debug.Log("Gold appeared on the cell!");
    }

    public bool HasGold() => _hasGold;

    public int GetDepth() => _currentDepth;
}
