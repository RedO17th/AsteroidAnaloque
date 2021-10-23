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
    public ScreenSystem ScreenSystem => _screenSystem;

    private InputSystem inputSystem;
    private ScreenSystem _screenSystem;

    private Vector3 _direction = Vector3.zero;

    private Coroutine _invulnerabilityTimer;

    protected override void InitializeData()
    {
        _systemInitializer.GameController.OnStartGameEvent += InitializePlayerAppearence;
        inputSystem = (InputSystem)_systemInitializer.GetSystem(SystemType.InputSys);
        _screenSystem = (ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys);

        _player.Constructor(this);

        //------------------------------------------------------------BaseMech
        _shootingMechanics.Constructor(_systemInitializer);
        _flashingMechanics.Constructor(_systemInitializer);

        _shootingMechanics.InitializePoolBullets();
    }

    private void InitializePlayerAppearence()
    {
        PreparePlayer();
        //SetInputEvents();
        SetStartFlashingMechanic();
    }

    public void SetStartFlashingMechanic()
    {
        _invulnerabilityTimer = StartCoroutine(InvulnerabilityTimer());
    }

    IEnumerator InvulnerabilityTimer()
    {
        _player.ViewObject.ActiveCollider(false);
        _flashingMechanics.Activate();

        yield return new WaitForSeconds(_invulnerabilityime);

        _flashingMechanics.TurnOffMechanics();
        _player.ViewObject.ActiveCollider();
    }

    private void PreparePlayer()
    {
        _player.Activate();
        _player.SetStartPosition(Vector3.zero);
        _player.SetRestSpeedAndRotation();
    }

    private void SetInputEvents()
    {
        //inputSystem.OnMovementEvent += MovementMechanic;
        //inputSystem.OnShootingEvent += ShootingMechanics;

        //inputSystem.OnRotationEvent += RotateMechanic;
    }
    private void UnSetInputEvents()
    {
        //inputSystem.OnMovementEvent -= MovementMechanic;
        //inputSystem.OnShootingEvent -= ShootingMechanics;

        //inputSystem.OnRotationEvent -= RotateMechanic;
    }

    public void MovementMechanic(float verticalInput)
    {
        _direction = verticalInput * transform.forward * _accelerationSpeed;
        _player.Move(_direction);
    }

    public void RotateMechanic(float horizontalInput)
    {
        Vector3 _angleRotation = new Vector3(0f, horizontalInput, 0f);
        _player.Rotate(_angleRotation);
    }

    public void ShootingMechanics()
    {
        _shootingMechanics.Shoot();
    }

    private void OnDisable()
    {
        UnSetInputEvents();
        _systemInitializer.GameController.OnStartGameEvent -= InitializePlayerAppearence;
    }

    public override void OffSystem()
    {
        _player.Activate(false);

        _shootingMechanics.TurnOffMechanics();
        _flashingMechanics.TurnOffMechanics();

        UnSetInputEvents();
        if(_invulnerabilityTimer != null) StopCoroutine(_invulnerabilityTimer);
    }
}
