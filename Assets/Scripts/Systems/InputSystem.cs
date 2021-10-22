using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    [SerializeField] private InputSystem.InputType _inputType;

    public InputSystem.InputType InputType => _inputType;

    protected PlayerManagerSystem _playerManagerSystem;

    public void Constructor(InputSystem system)
    {
        _playerManagerSystem = system.PlayerManagerSystem;
    }

    public abstract void SetControlMethods();
    public abstract void UnSetControlMethods();
}

public class InputSystem : BaseSystem
{
    public enum InputType { None = -1, WithoutMouse, WithMouse}

    [SerializeField] private List<BaseInput> _inputs;

    public PlayerManagerSystem PlayerManagerSystem { get; private set; }

    private InputType _currentType = InputType.WithoutMouse;

    protected override void InitializeData()
    {
        PlayerManagerSystem = (PlayerManagerSystem)_systemInitializer.GetSystem(SystemType.PlayerManagerSys);
    }

    public override void AdditionalInitialize()
    {
        for (int i = 0; i < _inputs.Count; i++)
            _inputs[i].Constructor(this);

        SomeMethod();
    }

    private void SomeMethod()
    {
        BaseInput inputWithoutMouse = GetInput(_currentType);
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

}
