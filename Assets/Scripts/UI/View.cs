using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardCounterText;
    [SerializeField] private TextMeshProUGUI shovelCounterText;

    public void UpdateRewardCount(int rewardsCollected, int requiredRewards)
    {
        if(rewardCounterText != null)
        {
            rewardCounterText.text = $"{rewardsCollected}/{requiredRewards}";
        }
        else
        {
            Debug.LogWarning("RewardCounterText is not assigned in the inspector!");
        }
    }
    public void UpdateShovelCount(int shovelsRemaining)
    {
        if (shovelCounterText != null)
        {
            shovelCounterText.text = shovelsRemaining.ToString();
        }
        else
        {
            Debug.LogWarning("ShovelCounterText is not assigned in the inspector!");
        }
    }
 
}
