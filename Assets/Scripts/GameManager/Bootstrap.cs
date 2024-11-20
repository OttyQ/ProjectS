using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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
    private GameSaveData gameSaveData;
    private Config config;
    private void Awake()
    {
        config = GameConfigProvider.instance.GameConfig;
        InitializeGame();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    public void OnApplicationQuit()
    {
        Debug.Log("On application quit awake!");
        GameSaveData saveData = new GameSaveData
        {
            shovels = countHandler.GetRemainingShovels(),
            collectedGold = countHandler.GetCollectedRewards(),
            cells = gridFill.GetCellsData() // Собираем данные о клетках
        };

        saverLoader.SaveGame(saveData);
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("On application quit awake!");
            GameSaveData saveData = new GameSaveData
            {
                shovels = countHandler.GetRemainingShovels(),
                collectedGold = countHandler.GetCollectedRewards(),
                cells = gridFill.GetCellsData() // Собираем данные о клетках
            };

            saverLoader.SaveGame(saveData);
        }
    }
    private void RestartGame()
    {
        Debug.Log("Bootstrap restartGame starts!");
        saverLoader.DeleteSaveFile();
        UnsubscribeEvents();
        InitializeGame();
    }

    public void InitializeGame()
    {
        Debug.Log("Запуск инициализации");
        AssignDependencies();
        if (gameSaveData != null)
        {
            Debug.Log("Bootstrap initialize from gameData");
            InitialFromSaveData();
        }
        else
        {
            Debug.Log("Bootstrap initialize from config");
            InitialFromConfig();
        }
        SubscribeEvents();
        Debug.Log("Инициализация закончена");
    }

    private void InitialFromConfig()
    {
        countHandler.Initialize(config.initialShovelCount, config.requiredGoldBars);
        rewardManager.Initialize(config.goldSpawnChance, config.goldSpawnChanceIncrement, goldPrefab);
        gridFill.Initialize(gridParent, cellPrefab, config.maxDepth, countHandler);
        gridFill.InitializeGrid(config.fieldSize);
        gameState.Initialize(winMenu, countHandler);
    }

    private void InitialFromSaveData()
    {
        countHandler.Initialize(gameSaveData.shovels, config.requiredGoldBars, gameSaveData.collectedGold);
        rewardManager.Initialize(config.goldSpawnChance, config.goldSpawnChanceIncrement, goldPrefab);
        gridFill.Initialize(gridParent, cellPrefab, config.maxDepth, countHandler);
        gridFill.InitializeGridFromData(gameSaveData.cells, rewardManager);

        gridFill.DataGridSpawnGold(rewardManager);
        gameState.Initialize(winMenu, countHandler);
    }
    

    private void AssignDependencies()
    {
        //Находим или назначаем компоненты, если они не заданы
        countHandler ??= GetComponent<CountHandler>();
        view ??= GetComponent<View>();
        rewardManager ??= GetComponent<RewardManager>();
        bag ??= FindObjectOfType<UIBag>();
        gameState ??= GetComponent<GameState>();
        saverLoader??= GetComponent<SaverLoader>();
        gameSaveData = saverLoader.LoadGame();
        Debug.Log($"If GameSaveData: {gameSaveData == null}" );
    }

    private void SubscribeEvents()
    {
        //Подписка на события
        rewardManager.SubscribeToCellEvents(FindObjectsOfType<Cell>());
        countHandler.AllRewardCollected += gameState.Win;
        countHandler.OnShovelCountChanged += view.UpdateShovelCount;
        countHandler.OnRewardCountChanged += view.UpdateRewardCount;
        bag.OnGoldAddedToBag += countHandler.CollectReward;
        gameState.OnGameRestart += RestartGame;

        countHandler.UpdateView();
    }

    private void UnsubscribeEvents()
    {
        //Отписка от событий
        countHandler.AllRewardCollected -= gameState.Win;
        countHandler.OnShovelCountChanged -= view.UpdateShovelCount;
        countHandler.OnRewardCountChanged -= view.UpdateRewardCount;
        bag.OnGoldAddedToBag -= countHandler.CollectReward;
        gameState.OnGameRestart -= RestartGame;
    }
}
