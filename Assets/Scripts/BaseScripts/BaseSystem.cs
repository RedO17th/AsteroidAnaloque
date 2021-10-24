using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSystem : MonoBehaviour
{
    [SerializeField] protected SystemType _type = SystemType.None;

    protected SystemInitializer _systemInitializer;

    public SystemType Type => _type;
    public SystemInitializer SystemInitializer => _systemInitializer;

    public AppData Data { get; private set; }

    public virtual void Constructor(SystemInitializer initializer)
    {
        _systemInitializer = initializer;

        Data = initializer.Data;
        InitializeData();
    }

    protected virtual void InitializeData() { }

    public virtual void AdditionalInitialize() { }

    public virtual void OffSystem() { }
}

