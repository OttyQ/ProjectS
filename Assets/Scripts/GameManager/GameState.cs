using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState: MonoBehaviour 
{
    private WinMenu winMenu;
    private CountHandler countHandler;

    public event Action OnGameRestart;

    public void Initialize(WinMenu winMenu, CountHandler countHandler)
    {
        this.winMenu = winMenu;
        this.countHandler = countHandler;
    }

    public void Win()
    {
        winMenu.Setup(countHandler.GetCollectedRewards());
        winMenu.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        winMenu.gameObject.SetActive(false);
        OnGameRestart?.Invoke();
    }
}
