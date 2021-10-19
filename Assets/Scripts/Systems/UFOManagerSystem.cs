using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManagerSystem : BaseSystem
{
    [Header("Additional Mechanics")]
    [SerializeField] private BaseShootingMechanics _shootingMechanics;

    [SerializeField] private UFO _ufoCharacter;
    [SerializeField] private float _timeToCrossScreen = 10f;

    public UFO UFOCharacter => _ufoCharacter;
    public float TimeToCrossScreen => _timeToCrossScreen;

    private PlayerManagerSystem _playerManagerSystem;
    private UFOSpawnSystem _ufoSpawnSystem;

    public SpatialCharacter Player { get; private set; }

    protected override void InitializeData()
    {
        _playerManagerSystem = (PlayerManagerSystem)_systemInitializer.GetSystem(SystemType.PlayerManagerSys);
        _ufoSpawnSystem = (UFOSpawnSystem)_systemInitializer.GetSystem(SystemType.UFOSpawner);
        Player = _playerManagerSystem.Player;

        _shootingMechanics.Constructor(_systemInitializer);
    }

    public override void AdditionalInitialize()
    {
        _ufoCharacter.Constructor(this);

        UFOInitialize();
    }

    private void UFOInitialize()
    {
        _ufoCharacter.SetPosition(_ufoSpawnSystem.GetSpacePosition());
        StartCoroutine(WaitingTimer());
    }

    private IEnumerator WaitingTimer()
    {
        //float time = _ufoSpawnSystem.WaitingTime;
        yield return new WaitForSeconds(_ufoSpawnSystem.WaitingTime);

        _ufoCharacter.Activate();
        _ufoCharacter.Move(_ufoSpawnSystem.GetWayMovement());

        _shootingMechanics.Shoot();
    }

    public void SetDeathEvent()
    {
        UFOInitialize();
    }


}
