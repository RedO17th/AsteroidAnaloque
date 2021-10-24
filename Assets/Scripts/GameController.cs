using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { None = -1, StartGame, Continue, Pause, ExitGame }

public class GameController : MonoBehaviour
{
    [SerializeField] private SystemInitializer _systemInitializer;

    public event Action OnStartGameEvent;
    public event Action OnContinueGameEvent;
    public event Action OnPauseGameEvent;

    public bool IsSessionStart { get; private set; } = false;

    private const int _gameAtPause = 0;
    private const int _gameAtStart = 1;

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
                    //Включить функционал Инпутов
                    OnContinueGameEvent?.Invoke();
                    break;
                }
            case GameState.Pause:
                {
                    SetPause(_gameAtPause);
                    //Выключить функционал Инпутов
                    OnPauseGameEvent?.Invoke();
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
        if (IsSessionStart)
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
        IsSessionStart = state;
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
