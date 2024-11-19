using System;
using TMPro;
using UnityEngine;

public class CountHandler : MonoBehaviour
{
    public event Action<int> OnShovelCountChanged;            
    public event Action<int, int> OnRewardCountChanged;       
    public event Action AllRewardCollected;                   

    private int _countShovels;                               
    private int _collectedRewards;                           
    private int _requiredRewards;                            

    public void Initialize(int countShovels, int requiredRewards)
    {
        _countShovels = countShovels;
        _requiredRewards = requiredRewards;
        _collectedRewards = 0;

        OnShovelCountChanged?.Invoke(_countShovels);
        OnRewardCountChanged?.Invoke(_collectedRewards, _requiredRewards);
        Debug.Log("Intit params CountHandler: Shovels: " +  _countShovels + " ReqRewards: " + requiredRewards);
    }

    public void UseShovel()
    {
        if (_countShovels > 0)
        {
            _countShovels--;
            OnShovelCountChanged?.Invoke(_countShovels);
            if(_countShovels == 0)
            {
                Debug.Log("������� �����������");
            }
        }
    }

    public void CollectReward()
    {
        if (_collectedRewards < _requiredRewards)
        {
            _collectedRewards++;
            OnRewardCountChanged?.Invoke(_collectedRewards, _requiredRewards);

            // ��������, �������� �� ������������ ���������� ������
            if (_collectedRewards == _requiredRewards)
            {
                AllRewardCollected?.Invoke();
                Debug.Log("��� ������� �������!");
            }
        }
    }

    public int GetRemainingShovels()
    {
        return _countShovels;
    }

    public int GetCollectedRewards()
    {
        return _collectedRewards;
    }

    public int GetRequiredRewards()
    {
        return _requiredRewards;
    }
}