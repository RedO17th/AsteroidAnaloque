using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpatialCharacter
{
    private PlayerManagerSystem managerSystem;
    private Rigidbody _rigidbody;

    private float _rotateSpeed = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Constructor(PlayerManagerSystem manager)
    {
        managerSystem = manager;
        _rotateSpeed = managerSystem.RotateSpeed;

        _view.Initialize(this);
    }

    public void Activate(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void Move(Vector3 direction)
    {
        _rigidbody.AddRelativeForce(direction);

        SetCorrectSpeed();
    }
    private void SetCorrectSpeed()
    {
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, managerSystem.MaxSpeed);
    }

    public void Rotate(Vector3 direction)
    {
        _rigidbody.MoveRotation(transform.rotation * Quaternion.AngleAxis(_rotateSpeed, direction));
    }
}
