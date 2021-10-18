using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnAsteroidsSystem : BaseSystem
{
    public enum SpawnAxis { None = -1, VerticalUp, HorizontalRight, VerticalBottom, HorizontalLeft }

    [Header("Additional settings")]
    [SerializeField] private float _axisSpawnOffset = 0.5f;
    [SerializeField] private float _sideSpawnOffset = 1f;

    [Header("Asteroid settings")]
    [SerializeField] private Transform _asteroidHandler;
    [SerializeField] private Asteroid _asteroidPrefab;

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private AsteroidObjectPoolSystem _asteroidObjectPoolSystem;

    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;

    protected override void InitializeData()
    {
        _asteroidsManagerSystem = (AsteroidsManagerSystem)_systemInitializer.GetSystem(SystemType.AsteroidsManagerSys);
        _asteroidObjectPoolSystem = (AsteroidObjectPoolSystem)_systemInitializer.GetSystem(SystemType.AsteroidObjPoolSys);

        _xMaxCoord = _asteroidsManagerSystem.XMaxCoord;
        _yMaxCoord = _asteroidsManagerSystem.YMaxCoord;
    }

    public void ToPrepareAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 position = GetRandomPosition();
            CreateAsteroid(position);
        }
    }

    public Vector3 GetRandomPosition()
    {
        return GetPosition(GetSpawnAxis());
    }
    private SpawnAxis GetSpawnAxis()
    {
        SpawnAxis axis = SpawnAxis.None;

        string[] spawnAxis = Enum.GetNames(typeof(SpawnAxis));
        int index = Random.Range(0, spawnAxis.Length - 1);

        Enum.TryParse(spawnAxis[index], out axis);

        if(axis == SpawnAxis.None)
            Debug.LogError($"SpawnAsteroidsSystem.GetSpawnAxis: Axis is abscent ({ axis }) ");

        return axis;
    }
    private Vector3 GetPosition(SpawnAxis axis)
    {
        float xCoord = 0f;
        float yCoord = 0f;

        switch (axis)
        {
            case SpawnAxis.VerticalUp:
                {
                    yCoord = _yMaxCoord + _axisSpawnOffset;
                    xCoord = Random.Range(-_xMaxCoord + _sideSpawnOffset, _xMaxCoord - _sideSpawnOffset);

                    break;
                }
            case SpawnAxis.HorizontalRight:
                {
                    xCoord = _xMaxCoord + _axisSpawnOffset;
                    yCoord = Random.Range(-_yMaxCoord + _sideSpawnOffset, _yMaxCoord - _sideSpawnOffset);

                    break;
                }
            case SpawnAxis.VerticalBottom:
                {
                    yCoord = -_yMaxCoord - _axisSpawnOffset;
                    xCoord = Random.Range(-_xMaxCoord + _sideSpawnOffset, _xMaxCoord - _sideSpawnOffset);

                    break;
                }
            case SpawnAxis.HorizontalLeft:
                {
                    xCoord = -_xMaxCoord - _axisSpawnOffset;
                    yCoord = Random.Range(-_yMaxCoord + _sideSpawnOffset, _yMaxCoord - _sideSpawnOffset);

                    break;
                }
        }

        return new Vector3(xCoord, yCoord, 0f);
    }

    private void CreateAsteroid(Vector3 position)
    {
        Asteroid newAsteroid = Create(position);
        newAsteroid.Activate(false);

        _asteroidObjectPoolSystem.AddBigAsteroid(newAsteroid);
    }

    public Asteroid Create(Vector3 position)
    {
        Asteroid newAsteroid = Instantiate(_asteroidPrefab, position, Quaternion.identity, _asteroidHandler);
        newAsteroid.Constructor(_asteroidsManagerSystem);

        return newAsteroid;
    }
}
