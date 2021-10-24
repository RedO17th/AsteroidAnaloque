using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuMechanics { None = -1, Continue, NewGame, Pause, Exit }

public class UISystem : BaseSystem
{
    public delegate void ChangeGameStateDelegate(GameState state);
    public event ChangeGameStateDelegate OnChangeGameStateEvent;

    [SerializeField] private UIManager _uiManager;

    [SerializeField] private int _startScoreValue = 0;

    public bool IsSessionStart
    {
        get => _gameController.IsSessionStart;
    }
    public int StartScoreValue => _startScoreValue;
    public PlayerManagerSystem PlayerManagerSystem => _playerManagerSystem;
    public MainMenuMechanics GameStateAtUI { get; private set; }  = MainMenuMechanics.None;

    private GameController _gameController;

    private InputSystem _inputSystem;
    private ScoringSystem _scoringSystem;
    private PlayerManagerSystem _playerManagerSystem;

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;

        _inputSystem = (InputSystem)_systemInitializer.GetSystem(SystemType.InputSys);
        _scoringSystem = (ScoringSystem)_systemInitializer.GetSystem(SystemType.ScoringSys);
        _playerManagerSystem = (PlayerManagerSystem)_systemInitializer.GetSystem(SystemType.PlayerManagerSys);
    }

    public override void AdditionalInitialize()
    {
        _uiManager.Constructor(this);
        _scoringSystem.OnSetScoreEvent += SetPlayerScore;
        _playerManagerSystem.OnPlayerDamageEvent += SetPlayerHealth;

        OnChangeGameStateEvent += _gameController.SetGameState;
    }

    public void SetMainMenuMechanic(MainMenuMechanics mechanics)
    {
        GameState state = GameState.None;

        switch (mechanics)
        {
            case MainMenuMechanics.Continue:
                {
                    state = GameState.Continue;
                    break;
                }
            case MainMenuMechanics.NewGame:
                {
                    state = GameState.StartGame;
                    break;
                }
            case MainMenuMechanics.Pause:
                {
                    state = GameState.Pause;
                    break;
                }
            case MainMenuMechanics.Exit:
                {
                    state = GameState.ExitGame;
                    break;
                }
        }

        OnChangeGameStateEvent?.Invoke(state);
    }

    private void SetPlayerScore(int score)
    {
        _uiManager.SetScore(score);
    }

    private void SetPlayerHealth(int amountHealth)
    {
        _uiManager.SetHealth(amountHealth);
    }

    public void SetInputType(InputSystem.InputType type)
    {
        _inputSystem.SetInputType(type);
    }

    public bool IsGameStateAtUIEquals(MainMenuMechanics state) 
    {
        return GameStateAtUI == state;
    }

    private void OnDisable()
    {
        _scoringSystem.OnSetScoreEvent -= SetPlayerScore;
        OnChangeGameStateEvent -= _gameController.SetGameState;
        _playerManagerSystem.OnPlayerDamageEvent -= SetPlayerHealth;
    }

}
