using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SystemType { None = -1, PlayerManagerSys, InputSys, ScreenSys, AsteroidsManagerSys, SpawnAsteroidSys, 
                        AsteroidObjPoolSys, FractureMechSys, UFOManagerSys, UFOSpawner, ScoringSys,
                        UISys }

public class SystemInitializer : MonoBehaviour
{
    [SerializeField] private List<BaseSystem> _baseSystems;

    private void Awake() => InitializeSystems();
    private void InitializeSystems()
    {
        for (int i = 0; i < _baseSystems.Count; i++)
            _baseSystems[i].Constructor(this);
    }


    private void Start() => AdditionalInitialize(); 
    private void AdditionalInitialize()
    {
        for (int i = 0; i < _baseSystems.Count; i++)
            _baseSystems[i].AdditionalInitialize();
    }


    public BaseSystem GetSystem(SystemType type)
    {
        BaseSystem system = null;
        for (int i = 0; i < _baseSystems.Count; i++)
        {
            if (_baseSystems[i].Type == type)
                system = _baseSystems[i];
        }

        return system;
    }
    



}
