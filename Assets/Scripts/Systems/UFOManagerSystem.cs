using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManagerSystem : BaseSystem
{
    [Header("Additional Mechanics")]
    [SerializeField] private BaseShootingMechanics _shootingMechanics;

    [SerializeField] private UFO _ufoCharacter;

    public UFO UFOCharacter => _ufoCharacter;
    public SpatialCharacter Player { get; private set; }
    public float TimeToCrossScreen => _timeToCrossScreen;

    private PlayerManagerSystem _playerManagerSystem;
    private UFOSpawnSystem _ufoSpawnSystem;
    private ScoringSystem _scoringSystem;

    private Coroutine _waitingTimer;

    private float _timeToCrossScreen = 10f;

    protected override void InitializeData()
    {
        _timeToCrossScreen = _systemInitializer.Data.UFOData.TimeToCrossScreen;

        _systemInitializer.GameController.OnStartGameEvent += InitializeUFOApperiance;
        _playerManagerSystem = (PlayerManagerSystem)_systemInitializer.GetSystem(SystemType.PlayerManagerSys);
        _ufoSpawnSystem = (UFOSpawnSystem)_systemInitializer.GetSystem(SystemType.UFOSpawner);
        _scoringSystem = (ScoringSystem)_systemInitializer.GetSystem(SystemType.ScoringSys);
        Player = _playerManagerSystem.Player;

        _shootingMechanics.Constructor(_systemInitializer);
    }

    public override void AdditionalInitialize()
    {
        _ufoCharacter.Constructor(this);
        _shootingMechanics.InitializePoolBullets();
    }

    private void InitializeUFOApperiance()
    {
        _ufoCharacter.SetPosition(_ufoSpawnSystem.GetSpacePosition());
        _waitingTimer = StartCoroutine(WaitingTimer());
    }

    private IEnumerator WaitingTimer()
    {
        yield return new WaitForSeconds(_ufoSpawnSystem.WaitingTime);

        _ufoCharacter.SetKiller(null);
        _ufoCharacter.Activate();
        _ufoCharacter.Move(_ufoSpawnSystem.GetWayMovement());
    }

    public void Shooot()
    {
        _shootingMechanics.Shoot();
    }

    public void SetDeathEvent(BasicCharacter character)
    {
        _scoringSystem.SetSacrifice(character);
        
        InitializeUFOApperiance();
    }
    public override void OffSystem()
    {
        _ufoCharacter.SetDeath();
        _shootingMechanics.TurnOffMechanics();
        if (_waitingTimer != null) StopCoroutine(_waitingTimer);
    }

}
