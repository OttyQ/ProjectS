using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Req Elements")]
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private View view;
    [SerializeField] private GridFill gridFill;
    [SerializeField] private WinMenu winMenu;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private UIBag bag;
    [SerializeField] private GameState gameState;

    private CountHandler countHandler;
    private SaverLoader saverLoader;

    private void Awake()
    {
        InitializeGame();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void InitializeGame()
    {
        AssignDependencies();
        InitializeComponents();
        InitializeGrid();
        SubscribeEvents();
    }

    private void RestartGame()
    {
        UnsubscribeEvents();
        InitializeGame();
    }

    private void AssignDependencies()
    {
        //������� ��� ��������� ����������, ���� ��� �� ������
        countHandler ??= GetComponent<CountHandler>();
        view ??= GetComponent<View>();
        rewardManager ??= GetComponent<RewardManager>();
        bag ??= FindObjectOfType<UIBag>();
        gameState ??= GetComponent<GameState>();
    }

    private void InitializeComponents()
    {
        // ��������� ������������
        var config = GameConfigProvider.instance.GameConfig;

        // ������������� ����������� � �������������� �������� �� ������������
        view.Initialize(config.initialShovelCount, config.requiredGoldBars);
        rewardManager.Initialize(config.goldSpawnChance, config.goldSpawnChanceIncrement, goldPrefab);
        countHandler.Initialize(config.initialShovelCount, config.requiredGoldBars);
        gameState.Initialize(winMenu, countHandler);
    }

    private void InitializeGrid()
    {
        //��������� ������������
        var config = GameConfigProvider.instance.GameConfig;

        //������������� �����
        gridFill.Initialize(gridParent, cellPrefab, config.maxDepth, countHandler);
        gridFill.InitializeGrid(config.fieldSize);
        rewardManager.SubscribeToCellEvents(FindObjectsOfType<Cell>());
    }


    private void SubscribeEvents()
    {
        //�������� �� �������
        countHandler.AllRewardCollected += gameState.Win;
        countHandler.OnShovelCountChanged += view.UpdateShovelCount;
        countHandler.OnRewardCountChanged += view.UpdateRewardCount;
        bag.OnGoldAddedToBag += countHandler.CollectReward;
        gameState.OnGameRestart += RestartGame;
    }

    private void UnsubscribeEvents()
    {
        //������� �� �������
        countHandler.AllRewardCollected -= gameState.Win;
        countHandler.OnShovelCountChanged -= view.UpdateShovelCount;
        countHandler.OnRewardCountChanged -= view.UpdateRewardCount;
        bag.OnGoldAddedToBag -= countHandler.CollectReward;
        gameState.OnGameRestart -= RestartGame;
    }
}
