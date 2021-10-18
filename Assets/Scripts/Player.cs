using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicMovingCharacter
{
    private PlayerManagerSystem managerSystem;
    private Rigidbody _playersRigidbody;

    private float _rotateSpeed = 10f;

    private void Awake()
    {
        _playersRigidbody = GetComponent<Rigidbody>();
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
        _playersRigidbody.AddRelativeForce(direction);

        SetCorrectSpeed();
    }
    private void SetCorrectSpeed()
    {
        _playersRigidbody.velocity = Vector2.ClampMagnitude(_playersRigidbody.velocity, managerSystem.MaxSpeed);
    }

    public void Rotate(Vector3 direction)
    {
        _playersRigidbody.MoveRotation(transform.rotation * Quaternion.AngleAxis(_rotateSpeed, direction));
    }
}
