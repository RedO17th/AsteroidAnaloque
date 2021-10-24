using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSystem : BaseSystem
{

    public delegate void ChangeGameStateDelegate(GameState state);
    public event ChangeGameStateDelegate OnChangeGameStateEvent;

    public event Action<int> OnPlayerDamageEvent;

    [SerializeField] private Player _player;

    [Space]
    [Header("Additional Mechanics")]
    [SerializeField] private BaseShootingMechanics _shootingMechanics;
    [SerializeField] private FlashingMechanics _flashingMechanics;

    public int MaxHealth { get; private set; } = 5;
    public float MaxSpeed => _maxMoveSpeed;
    public float RotateSpeedWithMouse => _rotateSpeedWithMouse;
    public float RotateSpeedWithoutMouse => _rotateSpeedWithoutMouse;
    public Player Player => _player;
    public ScreenSystem ScreenSystem => _screenSystem;

    private InputSystem _inputSystem;
    private ScreenSystem _screenSystem;

    private Vector3 _direction = Vector3.zero;

    private Coroutine _invulnerabilityTimer;

    private float _maxMoveSpeed = 7f;
    private float _accelerationSpeed = 5f;
    private float _rotateSpeedWithMouse = 10f;
    private float _rotateSpeedWithoutMouse = 10f;
    private float _invulnerabilityime = 3f;

    private bool isMovement = false;

    protected override void InitializeData()
    {
        InitializeBlockData();

        _inputSystem = (InputSystem)_systemInitializer.GetSystem(SystemType.InputSys);
        _screenSystem = (ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys);

        _player.Constructor(this);

        //BaseMech
        _shootingMechanics.Constructor(_systemInitializer);
        _flashingMechanics.Constructor(_systemInitializer);

        _shootingMechanics.InitializePoolBullets();
    }

    private void InitializeBlockData()
    {
        PlayerData data = Data.PlayerData;

        MaxHealth = data.MaxHealth;
        _maxMoveSpeed = data.MaxMoveSpeed;
        _accelerationSpeed = data.AccelerationSpeed;
        _rotateSpeedWithMouse = data.RotateSpeedWithMouse;
        _rotateSpeedWithoutMouse = data.RotateSpeedWithoutMouse;
        _invulnerabilityime = data.Invulnerabilityime;

        _systemInitializer.GameController.OnStartGameEvent += InitializePlayerAppearence;
        _systemInitializer.GameController.OnPauseGameEvent += SetDataAtPauseState;

        OnChangeGameStateEvent += _systemInitializer.GameController.SetGameState;
    }

    private void InitializePlayerAppearence()
    {
        PreparePlayer();
        SetInputEvents();
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
        _player.SetAmountHealth(MaxHealth);
        _player.SetStartPosition(Vector3.zero);
        _player.SetRestSpeedAndRotation();
    }

    private void SetInputEvents()
    {
        _inputSystem.SetInputMechanism();
    }
    private void UnSetInputEvents()
    {
        _inputSystem.UnSetInputMechanism();
    }

    public void MovementMechanic(float verticalInput)
    {
        _direction = verticalInput * transform.forward * _accelerationSpeed;
        _player.Move(_direction);

        SetMovementSound();
    }

    public void RotateMechanic(float horizontalInput)
    {
        Vector3 _angleRotation = new Vector3(0f, horizontalInput, 0f);
        _player.Rotate(_angleRotation);
    }
    public void RotateMechanic(Vector3 mousePosition)
    {
        if (_player.IsActivate) 
        {
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 correctPosition = new Vector3(mPosition.x, mPosition.y, _player.Position.z);

            Vector3 direction = (correctPosition - _player.Position).normalized;

            Quaternion rotation = Quaternion.LookRotation(direction, -Vector3.forward);
            Quaternion angleRotation = Quaternion.Lerp(_player.Rotation, rotation, Time.deltaTime * _rotateSpeedWithMouse);

            _player.Rotate(angleRotation);            
        }
    }

    public void ShootingMechanics()
    {
        _shootingMechanics.Shoot();
    }

    private void OnDisable()
    {
        UnSetInputEvents();
        SetStopMovementSound();

        OnChangeGameStateEvent -= _systemInitializer.GameController.SetGameState;
        _systemInitializer.GameController.OnStartGameEvent -= InitializePlayerAppearence;
        _systemInitializer.GameController.OnPauseGameEvent -= SetDataAtPauseState;
    }

    public override void OffSystem()
    {
        _player.Activate(false);
        SetStopMovementSound();

        _shootingMechanics.TurnOffMechanics();
        _flashingMechanics.TurnOffMechanics();

        UnSetInputEvents();
        if(_invulnerabilityTimer != null) StopCoroutine(_invulnerabilityTimer);
    }

    public void SetDamageEvent(int amountHealth)
    {
        OnPlayerDamageEvent?.Invoke(amountHealth);
    }
    public void SetDeadPlayerEvent()
    {
        SetStopMovementSound();
        OnChangeGameStateEvent?.Invoke(GameState.StartGame);
    }

    private void SetMovementSound()
    {
        if (!isMovement)
        {
            isMovement = true;
            SoundSystem.PlaySound(SoundSystem.SoundType.Accelerate);
        }
    }
    public void SetStopMovementSound()
    {
        if (isMovement)
        {
            isMovement = false;
            SoundSystem.StopSound(SoundSystem.SoundType.Accelerate);
        }
    }

    private void SetDataAtPauseState()
    {
        isMovement = false;
    }
}
