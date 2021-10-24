using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AppData", menuName = "Data/AppData")]
public class AppData : ScriptableObject
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private AsteroidData _asteroidData;
    [SerializeField] private UFOData _ufoData;

    public PlayerData PlayerData => _playerData;
    public AsteroidData AsteroidData => _asteroidData;
    public UFOData UFOData => _ufoData;
}
