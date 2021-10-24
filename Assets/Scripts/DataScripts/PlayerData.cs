using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Range(1, 10)]
    [SerializeField] private int _maxHealth = 5;

    [Range(1f, 10f)]
    [SerializeField] private float _maxMoveSpeed = 7f;

    [Range(1f, 10f)]
    [SerializeField] private float _accelerationSpeed = 5f;

    [Range(1, 7)]
    [SerializeField] private float _invulnerabilityime = 3f;

    [Range(1f, 5f)]
    [SerializeField] private float _rotateSpeedWithMouse = 10f;

    [Range(1f, 5f)]
    [SerializeField] private float _rotateSpeedWithoutMouse = 10f;

    [Header("Mechanics settings")]
    [SerializeField] private float _bulletSpeed = 0f;
    [SerializeField] private int _amountFlashingAtSec = 2;

    [Tooltip("Частота стрельбы")]
    [Range(0.5f, 3f)]
    [SerializeField] private float _shotFrequency = 3f;


    public int MaxHealth => _maxHealth;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float AccelerationSpeed => _accelerationSpeed;
    public float Invulnerabilityime => _invulnerabilityime;
    public float RotateSpeedWithMouse => _rotateSpeedWithMouse;
    public float RotateSpeedWithoutMouse => _rotateSpeedWithoutMouse;

    public float BulletSpeed => _bulletSpeed;
    public float ShotFrequency => _shotFrequency;
    public int AmountFlashingAtSec => _amountFlashingAtSec;


}
