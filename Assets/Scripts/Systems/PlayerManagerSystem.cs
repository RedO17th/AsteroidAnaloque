using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSystem : BaseSystem
{
    [SerializeField] private Player _player;

    [Space]
    [Header("Additional Mechanics")]
    [SerializeField] private BaseShootingMechanics _shootingMechanics;
    [SerializeField] private FlashingMechanics _flashingMechanics;

    //TO SO settings [TODO][FIX]
    [SerializeField] private float _maxMoveSpeed = 7f;
    [SerializeField] private float _accelerationSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private float _invulnerabilityime = 3f;

    public float MaxSpeed => _maxMoveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public Player Player => _player;

    private InputSystem inputSystem;

    private Vector3 _direction = Vector3.zero;

    private Coroutine _invulnerabilityTimer;

    protected override void InitializeData()
    {
        _systemInitializer.GameController.OnStartGameEvent += InitializePlayerAppearence;
        inputSystem = (InputSystem)_systemInitializer.GetSystem(SystemType.InputSys);

        _player.Constructor(this);

        //------------------------------------------------------------BaseMech
        _shootingMechanics.Constructor(_systemInitializer);
        _flashingMechanics.Constructor(_systemInitializer);

        _shootingMechanics.InitializePoolBullets();
    }

    private void InitializePlayerAppearence()
    {
        PreparePlayer();
        SetInputEvents();
        _invulnerabilityTimer = StartCoroutine(InvulnerabilityTimer());
    }

    private void PreparePlayer()
    {
        _player.Activate();
        _player.SetStartPosition(Vector3.zero);
        _player.SetRestSpeedAndRotation();
    }

    private void SetInputEvents()
    {
        inputSystem.OnMovementEvent += MovementMechanic;
        inputSystem.OnRotationEvent += RotateMechanic;
        inputSystem.OnShootingEvent += ShootingMechanics;
    }
    private void UnSetInputEvents()
    {
        inputSystem.OnMovementEvent -= MovementMechanic;
        inputSystem.OnRotationEvent -= RotateMechanic;
        inputSystem.OnShootingEvent -= ShootingMechanics;
    }

    IEnumerator InvulnerabilityTimer()
    {
        _flashingMechanics.Activate();

        yield return new WaitForSeconds(_invulnerabilityime);

        _flashingMechanics.TurnOffMechanics();
    }

    private void MovementMechanic(float verticalInput)
    {
        _direction = verticalInput * transform.up * _accelerationSpeed;
        _player.Move(_direction);
    }
    private void RotateMechanic(float horizontalInput)
    {
        Vector3 _angleRotation = new Vector3(0f, 0f, -horizontalInput);
        _player.Rotate(_angleRotation);
    }
    private void ShootingMechanics()
    {
        _shootingMechanics.Shoot();
    }

    private void OnDisable()
    {
        UnSetInputEvents();
    }

    public override void OffSystem()
    {
        _player.Activate(false);

        _flashingMechanics.TurnOffMechanics();

        UnSetInputEvents();
        if(_invulnerabilityTimer != null) StopCoroutine(_invulnerabilityTimer);
    }
}
