using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour
{
    [SerializeField] protected ScoringSystem.CharacterType _type;

    [SerializeField] protected BaseObjectView _view;
    [SerializeField] protected BaseCollisionMechanics _baseCollisionMechanics;

    [SerializeField] protected int _amountCollisionDamage;

    public BaseObjectView ViewObject => _view;
    public int AmountCollisionDamage => _amountCollisionDamage;
    public BasicCharacter Killer { get; private set; }
    public ScoringSystem.CharacterType CharacterType => _type;

    protected int _amountHealth;

    public void SetKiller(BasicCharacter character)
    {
        Killer = character;
    }
    public void SetAmountHealth(int maxHealth)
    {
        _amountHealth = maxHealth;
    }

    protected virtual void Awake()
    {
        _view.Initialize(this);
    }

    public virtual void TakeDamage(int amountDamage)
    {
        _amountHealth -= amountDamage;
        CheckHealth();
    }
    protected void CheckHealth()
    {
        if (_amountHealth <= 0)
            SetDeath();
    }

    public virtual void SetDeath()
    {
        Killer = null;
    }
}

public class SpatialCharacter : BasicCharacter
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

public abstract class MovementCharacter : SpatialCharacter
{
    protected Rigidbody _rigidbody;

    public bool IsActivate { get; protected set; } = false; 

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Activate(bool state = true)
    {
        IsActivate = state;
        gameObject.SetActive(state);
    }

    public abstract void Move(Vector3 direction);
    public abstract void Rotate(Vector3 direction);
}
