using UnityEngine;

public class Asteroid : BasicMovingCharacter
{
    [SerializeField] private AsteroidsSizeType _typeSize;

    public AsteroidsSizeType TypeSize => _typeSize;

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private Rigidbody _rbAsteroid;
    private AsteroidView _asteroidView;

    private void Awake()
    {
        _rbAsteroid = GetComponent<Rigidbody>();
    }

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

    public void Activate(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void SetMovement(Vector3 way)
    {
        _rbAsteroid.AddForce(way, ForceMode.VelocityChange);
    }
   
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
        return _rbAsteroid.velocity;
    }

    protected override void SetDeath()
    {
        _asteroidsManagerSystem.SetAsteroidDeathEvent(this);

        Activate(false);
        SetStopMovement();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetStopMovement()
    {
        _rbAsteroid.velocity = Vector3.zero;
    }
}
