using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : BaseSystem
{
    public delegate void MovementMessager(float value);
    public event MovementMessager OnMovementEvent;

    public delegate void RotationMessager(float value);
    public event RotationMessager OnRotationEvent;

    public event Action OnShootingEvent;

    void Update()
    {
        //dir
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0f)
            OnMovementEvent?.Invoke(verticalInput);

        //rot
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0f)
            OnRotationEvent?.Invoke(horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
            OnShootingEvent?.Invoke();

    }
}
