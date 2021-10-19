using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidObjectPoolSystem : BaseSystem
{
    [Header("Asteroid settings")]
    [SerializeField] private int _maxAmountAsteroids = 5;
    [Range(0.5f, 0.9f)]
    [SerializeField] private float _areaTargetPoints = 0.5f;
    [SerializeField] private float _minSpeed = 1f;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _waveWaitingTime = 2f;
    [SerializeField] private int _amountAtStartSession = 2;

    public List<Asteroid> _asteroids = new List<Asteroid>();
    public List<Asteroid> _curretAsteroids = new List<Asteroid>();

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private SpawnAsteroidsSystem _spawnAsteroidsSystem;

    private int _currentAmountAtSession = 0;

    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;

    protected override void InitializeData()
    {
        _asteroidsManagerSystem = (AsteroidsManagerSystem)_systemInitializer.GetSystem(SystemType.AsteroidsManagerSys);
        _spawnAsteroidsSystem = (SpawnAsteroidsSystem)_systemInitializer.GetSystem(SystemType.SpawnAsteroidSys);

        _xMaxCoord = _asteroidsManagerSystem.XMaxCoord;
        _yMaxCoord = _asteroidsManagerSystem.YMaxCoord;

        if (_amountAtStartSession > _maxAmountAsteroids)
            Debug.LogError($"AsteroidObjectPoolSystem.InitializeData: Amount asteroids at session bigger than max amount");

        _currentAmountAtSession = _amountAtStartSession;
    }

    public override void AdditionalInitialize()
    {
        _spawnAsteroidsSystem.ToPrepareAsteroids(_maxAmountAsteroids);
        LaunchAsteroids();        
    }

    private void LaunchAsteroids()
    {
        for (int i = 0; i < _currentAmountAtSession; i++)
        {
            Asteroid asteroid = GetAsteroid();
            if (asteroid)
            {
                Launch(asteroid);
                AddCurrentAsteroid(asteroid);
            }
            else
                Debug.LogError($"AsteroidsManagerSystem.LaunchAsteroids: Asteroid is null ");
        }
    }

    private Asteroid GetAsteroid()
    {
        Asteroid asteroid = null;
        if (_asteroids.Count != 0)
        {
            asteroid = _asteroids[0];
            _asteroids.Remove(asteroid);
        }

        return asteroid;
    }

    private void Launch(Asteroid asteroid)
    {
        Vector3 way = GetWay(asteroid);
        asteroid.Activate();
        asteroid.Move(way);
    }

    private Vector3 GetWay(Asteroid asteroid)
    {
        Vector3 target = GetRandomPoint();
        Vector3 direction = (target - asteroid.Position).normalized;
        Vector3 way = direction * GetRandomSpeed();

        return way;
    }
    private Vector3 GetRandomPoint()
    {
        float tempX = _xMaxCoord * _areaTargetPoints;
        float tempY = _yMaxCoord * _areaTargetPoints;

        float xCoord = Random.Range(-tempX, tempX);
        float yCoord = Random.Range(-tempY, tempY);

        return new Vector3(xCoord, yCoord);
    }
    public float GetRandomSpeed()
    {
        return Random.Range(_minSpeed, _maxSpeed);
    }

    public void SetNewPositionInSpace(Asteroid asteroid)
    {
        Vector3 newPosition = _spawnAsteroidsSystem.GetRandomPosition();
        asteroid.SetPosition(newPosition);

        AddBigAsteroid(asteroid);
        RemoveCurrentAsteroid(asteroid);
    }

    public void AddBigAsteroid(Asteroid asteroid)
    {
        _asteroids.Add(asteroid);
    }

    public void AddCurrentAsteroid(Asteroid asteroid)
    {
        _curretAsteroids.Add(asteroid);
    }
    public void RemoveCurrentAsteroid(Asteroid asteroid)
    {
        _curretAsteroids.Remove(asteroid);
        CheckAmountCurrentAsteroids();
    }

    private void CheckAmountCurrentAsteroids()
    {
        if (_curretAsteroids.Count == 0)
        {
            _currentAmountAtSession++;
            if (_currentAmountAtSession > _maxAmountAsteroids) _currentAmountAtSession = _maxAmountAsteroids;

            StartCoroutine(WaveWaitingTimer());
        }
    }
    private IEnumerator WaveWaitingTimer()
    {
        Debug.Log($"AsteroidObjectPoolSystem.WaveWaitingTimer: Asteroid is all");

        yield return new WaitForSeconds(_waveWaitingTime);
        LaunchAsteroids();
    }

}
