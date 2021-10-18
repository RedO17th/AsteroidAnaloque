using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AsteroidsSize { None = -1, Big, Middle, Small }

public class AsteroidsManagerSystem : BaseSystem
{
    private ScreenSystem _screenSystem;
    private AsteroidObjectPoolSystem _asteroidObjectPoolSystem;
    private FractureMechanicsSystem _fractureMechanicsSystem;

    public float XMaxCoord => _xMaxCoord;
    public float YMaxCoord => _yMaxCoord;

    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;

    protected override void InitializeData()
    {
        _screenSystem = (ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys);
        _asteroidObjectPoolSystem = (AsteroidObjectPoolSystem)_systemInitializer.GetSystem(SystemType.AsteroidObjPoolSys);
        _fractureMechanicsSystem = (FractureMechanicsSystem)_systemInitializer.GetSystem(SystemType.FractureMechSys);

        _xMaxCoord = _screenSystem.XMaxCoord;
        _yMaxCoord = _screenSystem.YMaxCoord;
    }

    public void SetAsteroidDeathEvent(Asteroid asteroid)
    {
        _fractureMechanicsSystem.CheckSize(asteroid);
        _asteroidObjectPoolSystem.SetSettings(asteroid);
    }
}
