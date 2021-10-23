using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectView : MonoBehaviour
{
    public BasicCharacter Character => _character;
    public Collider Collider { get; private set; }

    public BasicCharacter _character;

    public virtual void Initialize(BasicCharacter character) 
    {
        _character = character;
    }

    public void ActiveCollider(bool state = true)
    {
        Collider.enabled = state;
    }

    protected void GetCollider<T>() where T : Collider
    {
        Collider = GetComponent<T>();
    }

    public void Activate(bool state = true)
    {
        gameObject.SetActive(state);
    }
}
