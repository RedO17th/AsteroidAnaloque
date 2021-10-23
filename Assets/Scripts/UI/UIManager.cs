using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenuWindow _mainMenuWindow;
    [SerializeField] private GamePlayWindow _gamePlayWindow;

    private UISystem _uiSystem;

    public UISystem UISystem => _uiSystem;

    public void Constructor(UISystem uISystem)
    {
        _uiSystem = uISystem;
    }

    private void Awake()
    {
        _mainMenuWindow.Constructor(this);
        _gamePlayWindow.Constructor(this);
    }

    public void SetUIAction(MainMenuMechanics mechanics)
    {
        switch (mechanics)
        {
            case MainMenuMechanics.Continue:
                {
                    ShowMenu(false);
                    ShowGamePlayMenu();
                    break;
                }
            case MainMenuMechanics.NewGame:
                {
                    ShowMenu(false);
                    ShowGamePlayMenu();
                    InitializeGamePlayWindow();
                    break;
                }
            case MainMenuMechanics.Pause:
                {
                    ShowGamePlayMenu(false);
                    ShowMenu();
                    break;
                }
            case MainMenuMechanics.Exit:
                {
                    ShowMenu(false);
                    ShowGamePlayMenu(false);
                    break;
                }
        }

        _uiSystem.SetMainMenuMechanic(mechanics);
    }

    //Хардкод, перевести в baseWindow [FIX][TODO]
    private void ShowMenu(bool state = true)
    {
        _mainMenuWindow.Activate(state);
    }
    private void ShowGamePlayMenu(bool state = true)
    {
        _gamePlayWindow.Activate(state);
    }

    private void InitializeGamePlayWindow()
    {
        _gamePlayWindow.InitializeStartData();
    }

    public void SetScore(int score)
    {
        _gamePlayWindow.SetScore(score);
    }
}
