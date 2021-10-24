using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "Data/AsteroidData")]
public class AsteroidData : ScriptableObject
{
    [Range(1, 10)]
    [SerializeField] private int _maxHealth = 5;

    [SerializeField] private int _maxAmountAsteroids = 5;
    [Range(0.3f, 0.7f)]
    [SerializeField] private float _areaTargetPoints = 0.5f;

    [Range(0.3f, 0.7f)]
    [SerializeField] private float _minSpeed = 1f;

    [Range(0.3f, 0.7f)]
    [SerializeField] private float _maxSpeed = 5f;

    [Range(1f, 5f)]
    [SerializeField] private float _waveWaitingTime = 2f;

    [Range(2f, 5f)]
    [SerializeField] private int _amountAtStartSession = 2;

    [Header("Spawn settings")]
    [Range(0.1f, 1f)]
    [SerializeField] private float _axisSpawnOffset = 0.5f;

    [Range(0.5f, 2f)]
    [SerializeField] private float _sideSpawnOffset = 1f;

    [Header("Fracture settings")]
    [Range(1, 3)]
    [SerializeField] private int _amountNewAsteroids = 2;

    [SerializeField] private float _divergenceAngle = 45f;

    public int MaxHealth => _maxHealth;
    public int AmountAtStartSession => _amountAtStartSession;
    public int MaxAmountAsteroids => _maxAmountAsteroids;
    public float AreaTargetPoints => _areaTargetPoints;
    public float MinSpeed => _minSpeed;
    public float MaxSpeed => _maxSpeed;
    public float WaveWaitingTime => _waveWaitingTime;


    public float AxisSpawnOffset => _axisSpawnOffset;
    public float SideSpawnOffset => _sideSpawnOffset;

    public int AmountNewAsteroids => _amountNewAsteroids;
    public float DivergenceAngle => _divergenceAngle;

}
