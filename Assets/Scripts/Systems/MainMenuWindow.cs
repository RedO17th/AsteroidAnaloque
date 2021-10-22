using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuWindow : MonoBehaviour
{
    [SerializeField] private UIContinueButton _continueBtn;

    [SerializeField] private Button _newGameBtn;
    [SerializeField] private Button _exitBtn;

    //Вынести в BaseWindow [TODO][FIX]
    private UIManager _uiManager;
    public void Constructor(UIManager uIManager)
    {
        _uiManager = uIManager;
    }
    public void Activate(bool state)
    {
        gameObject.SetActive(state);
        _continueBtn.ActivateFunctional(_uiManager.UISystem.IsFirstLaunch);
    }
    private void OnEnable()
    {
        InitializeWindow();
    }
    private void InitializeWindow()
    {
        InitializeListeners();
    }
    //

    private void InitializeListeners()
    {
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
