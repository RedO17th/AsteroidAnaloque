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

    private PlayerManagerSystem managerSystem;

    private Vector3 _tempVelocity;
    private Vector3 _startVelocity = Vector3.zero;
    private Quaternion _tempRotation;

    private float _rotateSpeed = 10f;

    public void Constructor(PlayerManagerSystem manager)
    {
        managerSystem = manager;

        _view.Initialize(this);

        _tempRotation = Rotation;
        _tempVelocity = _startVelocity;
        _rotateSpeed = managerSystem.RotateSpeed;
    }

    public override void Activate(bool state = true)
    {
        base.Activate(state);
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
        SetCorrectSpeed();
    }
    private void SetCorrectSpeed()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, managerSystem.MaxSpeed);
    }

    public override void Rotate(Vector3 direction)
    {
        _rigidbody.MoveRotation(transform.rotation * Quaternion.AngleAxis(_rotateSpeed, direction));
    }
}
