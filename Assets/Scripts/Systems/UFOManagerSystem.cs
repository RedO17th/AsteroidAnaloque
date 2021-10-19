using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOManagerSystem : BaseSystem
{
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
    }

    public void SetDeathEvent()
    {
        _ufoSpawnSystem.UFOInitialize();
    }


}
