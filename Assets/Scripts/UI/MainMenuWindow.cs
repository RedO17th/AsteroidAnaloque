using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuWindow : BaseWindow
{
    [SerializeField] private UIContinueButton _continueBtn;

    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _exitBtn;

    public override void Activate(bool state)
    {
        base.Activate(state);

        InitializeListeners();
        _continueBtn.ActivateFunctional(_uiManager.UISystem.IsSessionStart);
    }

    private void Awake()
    {
        InitializeListeners();
    }

    private void InitializeListeners()
    {
        _continueBtn.Initialize();
        _continueBtn.AddLisener(SetContinueUIMechanics);

        _newGameBtn.onClick.AddListener(SetNewGameUIMechanics);
        _exitBtn.onClick.AddListener(SetExitUIMechanics);
    }

    private void SetContinueUIMechanics()
    {
        _uiManager.SetUIAction(MainMenuMechanics.Continue);
    }
    private void SetNewGameUIMechanics()
    {
        _uiManager.SetUIAction(MainMenuMechanics.NewGame);
    }
    private void SetExitUIMechanics()
    {
        _uiManager.SetUIAction(MainMenuMechanics.Exit);
    }

    private void OnDisable()
    {
        _continueBtn.RemoveListener(SetContinueUIMechanics);
        _newGameBtn.onClick.RemoveListener(SetNewGameUIMechanics);
        _exitBtn.onClick.RemoveListener(SetExitUIMechanics);
    }
}
