using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWithMouse : BaseInput
{
    public delegate void MovementMessager(float value);
    public event MovementMessager OnMovementEvent;

    public delegate void StopMovementMessager();
    public event StopMovementMessager OnStopMovementEvent;

    public event Action OnShootingEvent;

    public delegate void RotationMessagerTroughMouse(Vector3 mousePosition);
    public event RotationMessagerTroughMouse OnRotationTroughMouseEvent;

    public override void SetControlMethods()
    {
        OnMovementEvent += _playerManagerSystem.MovementMechanic;
        OnStopMovementEvent += _playerManagerSystem.SetStopMovementSound;

        OnShootingEvent += _playerManagerSystem.ShootingMechanics;

        OnRotationTroughMouseEvent += _playerManagerSystem.RotateMechanic;
    }

    public override void UnSetControlMethods()
    {
        OnMovementEvent -= _playerManagerSystem.MovementMechanic;
        OnStopMovementEvent -= _playerManagerSystem.SetStopMovementSound;

        OnShootingEvent -= _playerManagerSystem.ShootingMechanics;

        OnRotationTroughMouseEvent -= _playerManagerSystem.RotateMechanic;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetMouseButton(1) || Input.GetKey(KeyCode.UpArrow))
            OnMovementEvent?.Invoke(1f);
        else
            OnStopMovementEvent?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            OnShootingEvent?.Invoke();

        OnRotationTroughMouseEvent?.Invoke(Input.mousePosition);
    }
}
