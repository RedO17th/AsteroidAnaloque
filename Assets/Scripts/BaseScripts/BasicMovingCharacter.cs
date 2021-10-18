using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    [SerializeField] protected BaseObjectView _view;
    [SerializeField] protected int _amountHealth;

    public BaseObjectView ViewObject => _view;

    private void Awake()
    {
        _view.Initialize(this);
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

public class BasicMovingCharacter : BasicCharacter
{
    public Vector3 Position
    {
        get => transform.position;
        protected set => transform.position = value;
    }
    public void SetPosition(Vector3 newPosition)
    {
        Position = newPosition;
    }
}
