using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayWindow : BaseWindow
{
    [SerializeField] private UIScoreText _scoreText;
    [SerializeField] private UIHealthBar _healthBar;
    [SerializeField] private Button _pauseBtn;

    public override void Constructor(UIManager uIManager)
    {
        base.Constructor(uIManager);
        InitializeWindow();
    }
    private void InitializeWindow()
    {
        _scoreText.SetValue(_uiManager.UISystem.StartScoreValue);
        _healthBar.Initialize(_uiManager.UISystem.PlayerManagerSystem.MaxHealth);
        _pauseBtn.onClick.AddListener(SetPauseUIMechanics);
    }

    public override void Activate(bool state)
    {
        base.Activate(state);
        _pauseBtn.onClick.AddListener(SetPauseUIMechanics);
    }

    private void SetPauseUIMechanics()
    {
        _uiManager.SetUIAction(MainMenuMechanics.Pause);
    }

    public void SetScore(int score)
    {
        _scoreText.SetValue(score);
    }

    public void SetHealth(int amountHealth)
    {
        _healthBar.SetHealth(amountHealth);
    }

    public void InitializeStartData()
    {
        _scoreText.SetValue(_uiManager.UISystem.StartScoreValue);
        _healthBar.SetHealth(_uiManager.UISystem.PlayerManagerSystem.MaxHealth);
    }

    private void OnDisable()
    {
        _pauseBtn.onClick.RemoveListener(SetPauseUIMechanics);
    }
}
