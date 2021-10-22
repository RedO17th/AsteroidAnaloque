using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    protected UIManager _uiManager;

    public virtual void Constructor(UIManager uIManager)
    {
        _uiManager = uIManager;
    }

    public virtual void Activate(bool state)
    {
        gameObject.SetActive(state);
    }
}
