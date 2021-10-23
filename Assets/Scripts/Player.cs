using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementCharacter
{
    public Quaternion Rotation
    {
        get => transform.rotation;
        private set => transform.rotation = value;
    }

    public PlayerManagerSystem PlayerManagerSystem => _managerSystem;

    private PlayerManagerSystem _managerSystem;

    private Vector3 _tempVelocity;
    private Vector3 _startVelocity = Vector3.zero;
    private Quaternion _tempRotation;

    private float _rotateSpeed = 10f;

    public void Constructor(PlayerManagerSystem manager)
    {
        _managerSystem = manager;

        _view.Initialize(this);
        _baseCollisionMechanics.Constructor(this);

        _tempRotation = Rotation;
        _tempVelocity = _startVelocity;
        _rotateSpeed = _managerSystem.RotateSpeed;
    }



    public void SetStartPosition(Vector3 position)
    {
        Position = position;
    }
    public void SetRestSpeedAndRotation()
    {
        Rotation = _tempRotation;
        _rigidbody.velocity = _tempVelocity;
    }

    public override void Move(Vector3 direction)
    {
        _rigidbody.AddRelativeForce(direction);
    }

    private void Update()
    {
        //Через делегат передавать Позицию мыши, а в методе Манагера считать и возвращать Quaternion (обработанный через Lerp)

        //Vector3 mousePosition = Input.mousePosition;

        //Vector3 direction = (mousePosition - Position).normalized;
        //Quaternion rotation = Quaternion.LookRotation(direction, -Vector3.forward);

        //Rotate(Quaternion.Lerp(Rotation, rotation, Time.deltaTime));



        _managerSystem.ScreenSystem.CheckPlayerPosition(this);

        SetCorrectSpeed();
    }
    private void SetCorrectSpeed()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _managerSystem.MaxSpeed);
    }

    public void Rotate(Quaternion rotation)
    {
        _rigidbody.MoveRotation(rotation);
    }
    public override void Rotate(Vector3 direction)
    {
        _rigidbody.MoveRotation(transform.rotation * Quaternion.AngleAxis(_rotateSpeed, direction));
    }

    public override void TakeDamage(int amountDamage)
    {
        _amountHealth -= amountDamage;
        _managerSystem.SetDamageEvent(_amountHealth);
        CheckHealth();
    }

    public override void SetDeath()
    {
        Debug.Log($"Player.SetDeath");

        base.SetDeath();
        _managerSystem.SetDeadPlayerEvent();
    }
}
