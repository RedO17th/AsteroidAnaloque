using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovementCharacter
{
    private PlayerManagerSystem managerSystem;

    private float _rotateSpeed = 10f;

    public void Constructor(PlayerManagerSystem manager)
    {
        managerSystem = manager;
        _rotateSpeed = managerSystem.RotateSpeed;

        _view.Initialize(this);
    }

    public override void Move(Vector3 direction)
    {
        _rigidbody.AddRelativeForce(direction);

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
