using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToogleInputs : MonoBehaviour
{
    [SerializeField] private Toggle _withoutMouse;
    [SerializeField] private Toggle _withMouse;

    private MainMenuWindow _mainMenuWindow;

    public void Constructor(MainMenuWindow mainMenu)
    {
        _mainMenuWindow = mainMenu;
    }

    private void OnEnable()
    {
        _withoutMouse.onValueChanged.AddListener(SetWithoutMouseInputEvent);
        _withMouse.onValueChanged.AddListener(SetWithMouseInputEvent);
    }

    private void SetWithoutMouseInputEvent(bool isCheked)
    {
        if (_withoutMouse.isOn)
        {
            _mainMenuWindow.SetInputType(InputSystem.InputType.WithoutMouse);
        }
    }

    private void SetWithMouseInputEvent(bool isCheked)
    {
        if (_withMouse.isOn)
        {
            _mainMenuWindow.SetInputType(InputSystem.InputType.WithMouse);
        }
    }

    private void OnDisable()
    {
        _withoutMouse.onValueChanged.RemoveListener(SetWithoutMouseInputEvent);
        _withMouse.onValueChanged.RemoveListener(SetWithMouseInputEvent);
    }
}
