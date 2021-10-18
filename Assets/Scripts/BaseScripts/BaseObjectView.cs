using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectView : MonoBehaviour
{
    public Collider Collider { get; private set; }

    public abstract void Initialize();

    protected void GetCollider<T>() where T : Collider
    {
        Collider = GetComponent<T>();
    }

    public void Activate(bool state = true)
    {
        gameObject.SetActive(state);
    }
}
