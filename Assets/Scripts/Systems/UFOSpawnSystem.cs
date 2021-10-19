using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOSpawnSystem : BaseSystem
{
    public enum SpawnAxis { None = -1, HorizontalRight, HorizontalLeft }

    [Tooltip("Vertical offset in percent")]
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _verticalOffset = 0.8f;
    [SerializeField] private float _sideSpawnOffset = 1f;

    [Tooltip("Random time spawn")]
    [SerializeField] private float _leftTimeBorder = 20f;
    [SerializeField] private float _rightTimeBorder = 40f;

    private ScreenSystem _screenSystem;
    private UFOManagerSystem _ufoManagerSystem;

    private UFO _ufo;

    private Vector2 _fromLeftDirection = new Vector2(1, 0);
    private Vector2 _fromRightDirection = new Vector2(-1, 0);
    private Vector2 _currentDirection = Vector2.zero;

    private const int _screenSizeMultiplayer = 2;
    private float _xMaxCoord = 0f;
    private float _yMaxCoord = 0f;
    private float _yMaxVerticalCoord = 0f;

    protected override void InitializeData()
    {
        _screenSystem = (ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys);
        _ufoManagerSystem = (UFOManagerSystem)_systemInitializer.GetSystem(SystemType.UFOManagerSys);
        _ufo = _ufoManagerSystem.UFOCharacter;

        _xMaxCoord = _screenSystem.XMaxCoord;
        _yMaxCoord = _screenSystem.YMaxCoord;

        _yMaxVerticalCoord = _yMaxCoord * _verticalOffset;
    }

    public override void AdditionalInitialize()
    {
        _ufo.Constructor(_ufoManagerSystem);
        UFOInitialize();
    }

    public void UFOInitialize()
    {
        _ufo.SetPosition(GetSpacePosition());
        StartCoroutine(WaitingTimer());
    }

    private IEnumerator WaitingTimer()
    {
        float time = Random.Range(_leftTimeBorder, _rightTimeBorder);
        yield return new WaitForSeconds(1f);

        _ufo.Activate();
        _ufo.Move(GetWayMovement());
    }

    private Vector3 GetSpacePosition()
    {
        SpawnAxis axis = GetSpawnAxis();
        return GetPosition(axis);
    }
    private SpawnAxis GetSpawnAxis()
    {
        SpawnAxis axis = SpawnAxis.None;

        string[] spawnAxis = Enum.GetNames(typeof(SpawnAxis));
        int index = Random.Range(0, spawnAxis.Length - 1);

        Enum.TryParse(spawnAxis[index], out axis);

        if (axis == SpawnAxis.None)
            Debug.LogError($"UFOSpawnSystem.GetSpawnAxis: Axis is abscent ({ axis }) ");

        return axis;
    }
    private Vector3 GetPosition(SpawnAxis axis)
    {
        float xCoord = 0f;
        float yCoord = GetRandomVerticalCoord();

        switch (axis)
        {

            case SpawnAxis.HorizontalRight:
                {
                    xCoord = _xMaxCoord + _sideSpawnOffset;
                    _currentDirection = _fromRightDirection;

                    break;
                }

            case SpawnAxis.HorizontalLeft:
                {
                    xCoord = -_xMaxCoord - _sideSpawnOffset;
                    _currentDirection = _fromLeftDirection;

                    break;
                }
        }

        return new Vector3(xCoord, yCoord, 0f);
    }
    private float GetRandomVerticalCoord()
    {
        return Random.Range(-_yMaxVerticalCoord, _yMaxVerticalCoord);
    }

    private Vector3 GetWayMovement()
    { 
        return _currentDirection * GetSpeed();
    }
    private float GetSpeed()
    {
        return (_xMaxCoord * _screenSizeMultiplayer) / _ufoManagerSystem.TimeToCrossScreen;
    }
}
