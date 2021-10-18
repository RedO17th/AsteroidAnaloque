using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBySizeData
{
    public AsteroidsSizeType NextType { get; private set; }
    public float CurrentScale { get; private set; }

    public ScaleBySizeData(AsteroidsSizeType type, float scale)
    {
        NextType = type;
        CurrentScale = scale;
    }
}

[System.Serializable]
public class ScaleBySize
{
    [SerializeField] private AsteroidsSizeType _size;
    [SerializeField] private float _scale;

    public AsteroidsSizeType Type => _size;
    public float Scale => _scale;
}

public class FractureMechanicsSystem : BaseSystem
{
    [SerializeField] private List<ScaleBySize> _scaleSize = new List<ScaleBySize>();
    [SerializeField] private int _amountNewAsteroids = 2; 
    [SerializeField] private float _divergenceAngle = 45f; 

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private SpawnAsteroidsSystem _spawnAsteroidsSystem;
    private AsteroidObjectPoolSystem _asteroidObjectPoolSystem;

    protected override void InitializeData()
    {
        _asteroidsManagerSystem = (AsteroidsManagerSystem)_systemInitializer.GetSystem(SystemType.AsteroidsManagerSys);
        _spawnAsteroidsSystem = (SpawnAsteroidsSystem)_systemInitializer.GetSystem(SystemType.SpawnAsteroidSys);
        _asteroidObjectPoolSystem = (AsteroidObjectPoolSystem)_systemInitializer.GetSystem(SystemType.AsteroidObjPoolSys);
    }

    public bool IsStandartSize(Asteroid asteroid)
    {
        return asteroid.TypeSize == AsteroidsSizeType.Big;
    }

    public void CheckSplitting(Asteroid asteroid)
    {
        if (asteroid.TypeSize == AsteroidsSizeType.Small) return;

        DetermineSize(asteroid);
    }

    private void DetermineSize(Asteroid asteroid)
    {
        ScaleBySizeData data = GetNewScale(asteroid);
        CreateNewAsteroid(asteroid, data);
    }

    private ScaleBySizeData GetNewScale(Asteroid asteroid)
    {
        float newScale = 0f;
        AsteroidsSizeType nextType = AsteroidsSizeType.None;

        for (int i = 0; i < _scaleSize.Count; i++)
        {
            if (_scaleSize[i].Type == asteroid.TypeSize)
            {
                newScale = _scaleSize[i].Scale;
                nextType = GetAsteroidType(++i);

                break;
            }
        }

        return new ScaleBySizeData(nextType, newScale);
    }
    private AsteroidsSizeType GetAsteroidType(int index)
    {
        return (index < _scaleSize.Count) ? _scaleSize[index].Type : AsteroidsSizeType.Small;
    }

    private void CreateNewAsteroid(Asteroid asteroid, ScaleBySizeData data)
    {
        float speed = _asteroidObjectPoolSystem.GetRandomSpeed();

        for (int i = 0; i < _amountNewAsteroids; i++)
        {
            Asteroid newAsteroid = Create(asteroid, data, speed);
            _asteroidObjectPoolSystem.AddCurrentAsteroid(newAsteroid);
        }
    }

    private Asteroid Create(Asteroid asteroid, ScaleBySizeData data, float speed)
    {
        Asteroid newAsteroid = _spawnAsteroidsSystem.Create(asteroid.Position);
        newAsteroid.SetTypeSize(data.NextType);
        newAsteroid.SetValueSize(data.CurrentScale);
        newAsteroid.SetMovement(GetNewVelocity(asteroid, speed));

        return newAsteroid;
    }

    private Vector3 GetNewVelocity(Asteroid asteroid, float speed)
    {
        float angle = Random.Range(-_divergenceAngle, _divergenceAngle);
        Quaternion quaternion = Quaternion.Euler(new Vector3(0f, 0f, angle));

        return (quaternion * asteroid.GetVelocity().normalized) * speed;
    }
}
