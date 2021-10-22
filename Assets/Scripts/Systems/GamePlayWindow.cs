using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayWindow : BaseWindow
{
    [SerializeField] private UIScoreText _scoreText;
    [SerializeField] private Button _pauseBtn;

    public override void Constructor(UIManager uIManager)
    {
        base.Constructor(uIManager);
        InitializeWindow();
    }
    private void InitializeWindow()
    {
        _scoreText.SetValue(0);
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

    public void InitializeStartData()
    {
        _scoreText.SetValue(0);
        //Initialize health player
    }

    private void OnDisable()
    {
        _pauseBtn.onClick.RemoveListener(SetPauseUIMechanics);
    }
}
