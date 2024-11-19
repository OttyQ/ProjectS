public interface IUIController
{
    void InitializeUI(int shovelsCount, int requiderGoldBars);
    void UpdateShovelCount(int shovelsRemaining);

    void UpdateRewardCount(int rewardsCollected);


}