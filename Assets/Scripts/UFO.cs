using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MovementCharacter
{
    public Quaternion Rotation
    {
        get => transform.rotation;
        private set => transform.rotation = value;
    }

    private UFOManagerSystem _ufoManagerSystem;
    private SpatialCharacter _targetPlayer;

    private Quaternion _startRotation;

    private bool isDead = false;

    public void Constructor(UFOManagerSystem manager)
    {
        _ufoManagerSystem = manager;
        _targetPlayer = manager.Player;

        _startRotation = Rotation;
        
        _view.Initialize(this);
        _baseCollisionMechanics.Constructor(this);
    }

    public override void Move(Vector3 direction)
    {
        isDead = false;
        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
    }

    public override void Rotate(Vector3 direction)
    {
        Vector3 dir = (_targetPlayer.Position - Position).normalized;
        _rigidbody.MoveRotation(Quaternion.LookRotation(dir, Vector3.forward));
    }

    private void Update()
    {
        Rotate(new Vector3());
    }

    public override void SetDeath()
    {
        if (isDead) return;

        isDead = true;

        Activate(false);
        SetStopMovement();

        Rotation = _startRotation;
        _ufoManagerSystem.SetDeathEvent();
    }

    private void SetStopMovement()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
