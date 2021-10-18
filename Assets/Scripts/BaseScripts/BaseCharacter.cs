using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected BaseObjectView _view;

    public BaseObjectView ViewObject => _view;

    public int _amountHealth = 1;

    public Vector3 Position
    {
        get => transform.position;
        protected set => transform.position = value;
    }
    public void SetPosition(Vector3 newPosition)
    {
        Position = newPosition;
    }

    public void TakeDamage(int amountDamage)
    {
        _amountHealth -= amountDamage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_amountHealth <= 0)
            SetDeath();
    }

    protected virtual void SetDeath()
    {
        Debug.Log($"BaseCharacter.SetDeath");
    }
}
