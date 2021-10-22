using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainMenuMechanics { None = -1, Continue, NewGame, Pause, Exit }

public class UISystem : BaseSystem
{
    [SerializeField] private UIManager _uiManager;

    private GameController _gameController;

    public bool IsFirstLaunch
    {
        get => _gameController.IsSessionStart;
    }

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;
    }

    public override void AdditionalInitialize()
    {
        _uiManager.Constructor(this);
    }

    public void SetMainMenuMechanic(MainMenuMechanics mechanics)
    {
        switch (mechanics)
        {
            case MainMenuMechanics.Continue:
                {
                    _gameController.SetGameState(GameState.Continue);
                    break;
                }
            case MainMenuMechanics.NewGame:
                {
                    _gameController.SetGameState(GameState.StartGame);
                    break;
                }
            case MainMenuMechanics.Pause:
                {
                    _gameController.SetGameState(GameState.Pause);
                    break;
                }
            case MainMenuMechanics.Exit:
                {
                    break;
                }
        }
    }
}
