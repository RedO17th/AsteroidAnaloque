using UnityEngine;

public class Asteroid : MovementCharacter
{
    [SerializeField] private AsteroidsSizeType _typeSize;

    public AsteroidsSizeType TypeSize => _typeSize;

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private AsteroidView _asteroidView;

    public void Constructor(AsteroidsManagerSystem manager)
    {
        _asteroidsManagerSystem = manager;
        InitializeView();
    }

    private void InitializeView()
    {
        _view.Initialize(this);
        _asteroidView = (AsteroidView)_view;
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
        
    public Vector3 GetVelocity()
    {
        return _rigidbody.velocity;
    }

    public override void SetDeath()
    {
        _asteroidsManagerSystem.SetAsteroidDeathEvent(this);

        base.SetDeath();
        Activate(false);
        SetStopMovement();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetStopMovement()
    {
        _rigidbody.velocity = Vector3.zero;
    }



   
}
