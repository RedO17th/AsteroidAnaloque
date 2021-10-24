using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] private InputSystem.InputType _inputType;

    public InputSystem.InputType InputType => _inputType;

    protected PlayerManagerSystem _playerManagerSystem;

    public bool IsActive { get; protected set; } = false;

    public void Constructor(InputSystem system)
    {
        _playerManagerSystem = system.PlayerManagerSystem;
    }

    public void Activate(bool state = true)
    {
        IsActive = state;
        gameObject.SetActive(state);
    }

    public abstract void SetControlMethods();
    public abstract void UnSetControlMethods();
}

public class InputSystem : BaseSystem
{
    public enum InputType { None = -1, WithoutMouse, WithMouse}

    [SerializeField] private InputType _defaultInputType;
    [SerializeField] private List<BaseInput> _inputs;

    public PlayerManagerSystem PlayerManagerSystem { get; private set; }

    private GameController _gameController;
    private InputType _currentType = InputType.None;

    protected override void InitializeData()
    {
        _gameController = _systemInitializer.GameController;
        _gameController.OnPauseGameEvent += PauseGameEvent;
        _gameController.OnContinueGameEvent += ContinueGameEvent;

        PlayerManagerSystem = (PlayerManagerSystem)_systemInitializer.GetSystem(SystemType.PlayerManagerSys);
    }

    public override void AdditionalInitialize()
    {
        for (int i = 0; i < _inputs.Count; i++)
            _inputs[i].Constructor(this);

        _currentType = _defaultInputType;
        OffAllInputSubSystems();
    }

    private void ContinueGameEvent()
    {
        SetInputMechanism();
    }
    private void PauseGameEvent()
    {
        UnSetInputMechanism();
    }

    public void SetInputType(InputType inputType)
    {
        if (_gameController.IsSessionStart && _currentType != inputType)
        {
            _currentType = inputType;
            SetInputMechanism();
        }
        else
            _currentType = inputType;
    }

    public void SetInputMechanism()
    {
        OffAllInputSubSystems();

        BaseInput inputWithoutMouse = GetInput(_currentType);
        inputWithoutMouse.Activate();
        inputWithoutMouse.SetControlMethods();
    }
    private BaseInput GetInput(InputType type)
    {
        BaseInput input = null;
        for (int i = 0; i < _inputs.Count; i++)
        {
            if (_inputs[i].InputType == type)
                input = _inputs[i];
        }

        return input;
    }

    public void UnSetInputMechanism()
    {
        for (int i = 0; i < _inputs.Count; i++)
        {
            if (_inputs[i].InputType == _currentType)
            {
                _inputs[i].Activate(false);
                _inputs[i].UnSetControlMethods();
            }
        }
    }

    private void OffAllInputSubSystems()
    {
        for (int i = 0; i < _inputs.Count; i++)
        {
            _inputs[i].Activate(false);
            _inputs[i].UnSetControlMethods();
        }
    }

    private void OnDisable()
    {
        _gameController.OnPauseGameEvent -= PauseGameEvent;
        _gameController.OnContinueGameEvent -= ContinueGameEvent;
    }
}
