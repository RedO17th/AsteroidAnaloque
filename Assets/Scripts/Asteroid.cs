using UnityEngine;

public class Asteroid : MovementCharacter
{
    [SerializeField] private AsteroidsSizeType _typeSize;

    public AsteroidsSizeType TypeSize => _typeSize;
    public SoundSystem.SoundType SoundType; //{ get; private set; } = SoundSystem.SoundType.BigAsteroid;

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private AsteroidView _asteroidView;

    public void Constructor(AsteroidsManagerSystem manager)
    {
        _asteroidsManagerSystem = manager;
        InitializeData();
    }

    private void InitializeData()
    {
        _view.Initialize(this);
        _asteroidView = (AsteroidView)_view;
        _baseCollisionMechanics.Constructor(this);
    }

    public override void Activate(bool state = true)
    {
        base.Activate(state);
        _amountHealth = _asteroidsManagerSystem.SystemInitializer.Data.AsteroidData.MaxHealth;
    }

    public override void Move(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.VelocityChange);
    }
    public override void Rotate(Vector3 direction) { }
   
    public void SetValueSize(float value)
    {
        _asteroidView.SetScale(value);
    }
    public void SetTypeSize(AsteroidsSizeType type)
    {
        _typeSize = type;
    }
    public void SetTypeSound(SoundSystem.SoundType type)
    {
        SoundType = type;
    }

    public Vector3 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    public override void SetDeath()
    {
        SoundSystem.PlaySound(SoundType);
        _asteroidsManagerSystem.SetAsteroidDeathEvent(this);

        base.SetDeath();
        Activate(false);
        SetStopMovement();
    }

    private void SetStopMovement()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
