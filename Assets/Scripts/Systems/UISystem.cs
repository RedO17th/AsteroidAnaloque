using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuMechanics { None = -1, Continue, NewGame, Pause, Exit }

public class UISystem : BaseSystem
{
    [SerializeField] private UIManager _uiManager;

    [SerializeField] private int _startScoreValue = 0;

    private GameController _gameController;
    private ScoringSystem _scoringSystem;

    public bool IsSessionStart
    {
        get => _gameController.IsSessionStart;
    }
    public int StartScoreValue => _startScoreValue;

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;
        _scoringSystem = (ScoringSystem)_systemInitializer.GetSystem(SystemType.ScoringSys);
    }

    public override void AdditionalInitialize()
    {
        _uiManager.Constructor(this);
        _scoringSystem.OnSetScoreEvent += SetPlayerScore;
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
                    break;
                }
        }

        _gameController.SetGameState(state);
    }

    private void SetPlayerScore(int score)
    {
        _uiManager.SetScore(score);
    }

    private void OnDisable()
    {
        _scoringSystem.OnSetScoreEvent -= SetPlayerScore;
    }

}
