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
        if (winMenu == null || countHandler == null)
        {
            Debug.LogError("GameState initialization failed: winMenu or countHandler is null.");
            return;
        }

        this.winMenu = winMenu;
        this.countHandler = countHandler;
        Debug.Log("GameState successfully initialized.");
    }

    public void Win()
    {
        Debug.Log("Player has won the game.");
        winMenu.Setup(countHandler.GetCollectedRewards());
        winMenu.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game.");
        winMenu.gameObject.SetActive(false);
        OnGameRestart?.Invoke();
    }
}
