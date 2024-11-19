using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardCounterText;
    [SerializeField] private TextMeshProUGUI shovelCounterText;
    //[SerializeField] private GameObject winMenu;

    public void Initialize(int initialShovels, int requiredRewards )
    {
        // Устанавливаем начальные значения текстовых элементов
        rewardCounterText.text = $"{0}/{requiredRewards}";
        shovelCounterText.text = initialShovels.ToString();

        // Скрываем меню победы при запуске
        //winMenu.SetActive(false);
        Debug.Log("Intit params View: " + initialShovels + " " + requiredRewards);
    }

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
