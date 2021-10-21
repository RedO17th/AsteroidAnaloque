using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { None = -1, StartGame, Continue, Pause, ExitGame }

public class GameController : MonoBehaviour
{
    [SerializeField] private SystemInitializer _systemInitializer;

    public event Action OnStartGameEvent;

    private const int _gameAtPause = 0;
    private const int _gameAtStart = 1;

    private bool _isSessionStart = false;

    private void Awake()
    { 
        DontDestroyOnLoad(gameObject);

        _systemInitializer.Constructor(this);
        _systemInitializer.InitializeSystems();
        _systemInitializer.AdditionalInitialize();
    }

    public void SetGameState(GameState state)
    { 
        switch(state)
        {
            case GameState.StartGame:
                {
                    PrepareToStart();
                    break;
                }
            case GameState.Continue:
                {
                    SetPause(_gameAtStart);
                    break;
                }
            case GameState.Pause:
                {
                    SetPause(_gameAtPause);
                    break;
                }
            case GameState.ExitGame:
                {
                    ExitGame();
                    break;
                }
        }
    }

    private void PrepareToStart()
    {
        if (_isSessionStart)
        {
            ClearSystemData();
            SetPause(_gameAtStart);
        }
        else
            SetSessionState(true);

        StartGame();
    }

    private void SetSessionState(bool state) 
    {
        _isSessionStart = state;
    }

    private void StartGame()
    {
        OnStartGameEvent?.Invoke();
    }

    private void SetPause(int value)
    {
        Time.timeScale = value;
    }

    private void ClearSystemData()
    {
        _systemInitializer.TurnOffSystems();
    }

    private void ExitGame()
    {
        //Добавить функционал выключения приложения
    }
}
