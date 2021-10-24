using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UFOData", menuName = "Data/UFOData")]
public class UFOData : ScriptableObject
{
    [Range(1, 10)]
    [SerializeField] private int _maxHealth = 5;

    [Range(1f, 5f)]
    [SerializeField] private float _maxMoveSpeed = 7f;

    [Header("UFO manager settings")]
    [Range(7, 15)]
    [SerializeField] private float _timeToCrossScreen = 10f;

    [Header("UFO spawner settings")]
    [Tooltip("Vertical offset in percent")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _verticalOffset = 0.8f;

    [SerializeField] private float _sideSpawnOffset = 1f;

    [Tooltip("Random time spawn")]
    [SerializeField] private float _leftTimeBorder = 20f;

    [SerializeField] private float _rightTimeBorder = 40f;

    [Header("UFO shootings mech settings")]
    [Range(1, 3)]
    [SerializeField] private float _shotFrequencyMin = 2f;

    [Range(4, 5)]
    [SerializeField] private float _shotFrequencyMax = 5f;

    public int MaxHealth => _maxHealth;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float TimeToCrossScreen => _timeToCrossScreen;

    public float VerticalOffset => _verticalOffset;
    public float SideSpawnOffset => _sideSpawnOffset;
    public float LeftTimeBorder => _leftTimeBorder;
    public float RrightTimeBorder => _rightTimeBorder;

    public float ShotFrequencyMin => _shotFrequencyMin;
    public float ShotFrequencyMax => _shotFrequencyMax;



}
