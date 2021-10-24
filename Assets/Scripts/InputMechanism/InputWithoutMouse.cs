using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWithoutMouse : BaseInput
{
    public delegate void MovementMessager(float value);
    public event MovementMessager OnMovementEvent;

    public delegate void StopMovementMessager();
    public event StopMovementMessager OnStopMovementEvent;

    public event Action OnShootingEvent;

    public delegate void RotationMessager(float value);
    public event RotationMessager OnRotationEvent;

    public override void SetControlMethods()
    {
        OnMovementEvent += _playerManagerSystem.MovementMechanic;
        OnStopMovementEvent += _playerManagerSystem.SetStopMovementSound;

        OnShootingEvent += _playerManagerSystem.ShootingMechanics;
        OnRotationEvent += _playerManagerSystem.RotateMechanic;
    }

    public override void UnSetControlMethods()
    {
        OnMovementEvent -= _playerManagerSystem.MovementMechanic;
        OnStopMovementEvent -= _playerManagerSystem.SetStopMovementSound;

        OnShootingEvent -= _playerManagerSystem.ShootingMechanics;
        OnRotationEvent -= _playerManagerSystem.RotateMechanic;
    }

    private void Update()
    {
        //dir
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0f)
            OnMovementEvent?.Invoke(verticalInput);
        else
            OnStopMovementEvent?.Invoke();

        //rot
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0f)
            OnRotationEvent?.Invoke(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
            OnShootingEvent?.Invoke();
    }


}
