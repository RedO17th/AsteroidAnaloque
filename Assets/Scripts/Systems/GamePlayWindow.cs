using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayWindow : MonoBehaviour
{
    [SerializeField] private Button _pauseBtn;

    //Вынести в BaseWindow [TODO][FIX]
    private UIManager _uiManager;
    public void Constructor(UIManager uIManager)
    {
        _uiManager = uIManager;
    }
    public void Activate(bool state)
    {
        gameObject.SetActive(state);
    }
    private void OnEnable()
    {
        InitializeWindow();
    }
    private void InitializeWindow()
    {
        _pauseBtn.onClick.AddListener(SetPauseUIMechanics);
    }
    //

    private void SetPauseUIMechanics()
    {
        _uiManager.SetUIAction(MainMenuMechanics.Pause);
    }

}
