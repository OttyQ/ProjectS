using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardCounterText;
    [SerializeField] private TextMeshProUGUI shovelCounterText;
    //[SerializeField] private GameObject winMenu;

    public void UpdateRewardCount(int rewardsCollected, int requiredRewards)
    {
        rewardCounterText.text = $"{rewardsCollected}/{requiredRewards}";
    }
    public void UpdateShovelCount(int shovelsRemaining)
    {
        shovelCounterText.text = shovelsRemaining.ToString();
    }
    //public void ShowWinMenu()
    //{
    //    winMenu.SetActive(true);
    //}

}
