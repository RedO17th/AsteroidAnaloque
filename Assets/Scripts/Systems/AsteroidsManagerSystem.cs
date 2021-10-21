using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AsteroidsSizeType { None = -1, BigAsteroid, MiddleAsteroid, SmallAsteroid }

public class AsteroidsManagerSystem : BaseSystem
{
    private ScreenSystem _screenSystem;
    private AsteroidObjectPoolSystem _asteroidObjectPoolSystem;
    private FractureMechanicsSystem _fractureMechanicsSystem; 
    private ScoringSystem _scoringSystem;

    public float XMaxCoord => _xMaxCoord;
    public float YMaxCoord => _yMaxCoord;

    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;

    protected override void InitializeData()
    {
        _systemInitializer.GameController.OnStartGameEvent += InitializeAsteroidsApperiance;
        _screenSystem = (ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys);
        _asteroidObjectPoolSystem = (AsteroidObjectPoolSystem)_systemInitializer.GetSystem(SystemType.AsteroidObjPoolSys);
        _fractureMechanicsSystem = (FractureMechanicsSystem)_systemInitializer.GetSystem(SystemType.FractureMechSys);
        _scoringSystem = (ScoringSystem)_systemInitializer.GetSystem(SystemType.ScoringSys);

        _xMaxCoord = _screenSystem.XMaxCoord;
        _yMaxCoord = _screenSystem.YMaxCoord;
    }

    private void InitializeAsteroidsApperiance()
    {
        _asteroidObjectPoolSystem.InitializeAsteroids();
    }

    public void SetAsteroidDeathEvent(Asteroid asteroid)
    {
        _scoringSystem.SetSacrifice(asteroid);
        _fractureMechanicsSystem.CheckSplitting(asteroid);

        if (!_fractureMechanicsSystem.IsStandartSize(asteroid))
        {
            _asteroidObjectPoolSystem.RemoveCurrentAsteroid(asteroid);
            asteroid.Destroy();
        }
        else
            _asteroidObjectPoolSystem.SetNewPositionInSpace(asteroid);
    }
}
