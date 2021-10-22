using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayWindow : MonoBehaviour
{
    [SerializeField] private UIScoreText _scoreText;
    [SerializeField] private Button _pauseBtn;

    //Вынести в BaseWindow [TODO][FIX]
    private UIManager _uiManager;
    public void Constructor(UIManager uIManager)
    {
        _uiManager = uIManager;
        InitializeWindow();
    }
    private void InitializeWindow()
    {
        _scoreText.SetValue(0);
        _pauseBtn.onClick.AddListener(SetPauseUIMechanics);
    }

    public void Activate(bool state)
    {
        gameObject.SetActive(state);
        _pauseBtn.onClick.AddListener(SetPauseUIMechanics);
    }
    //

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
