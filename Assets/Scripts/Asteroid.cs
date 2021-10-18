using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : BaseCharacter
{
    [SerializeField] private AsteroidsSize _size;

    private AsteroidsManagerSystem _asteroidsManagerSystem;
    private Rigidbody _rbAsteroid;

    public AsteroidsSize Size => _size;

    private void Awake()
    {
        _rbAsteroid = GetComponent<Rigidbody>();
    }

    public void Constructor(AsteroidsManagerSystem manager)
    {
        _asteroidsManagerSystem = manager;
    }

    public void Activate(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void SetMovement(Vector3 way)
    {
        _rbAsteroid.AddForce(way, ForceMode.VelocityChange);
    }

    public Vector3 GetVelocity()
    {
        return _rbAsteroid.velocity;
    }

    protected override void SetDeath()
    {
        Activate(false);
        _asteroidsManagerSystem.SetAsteroidDeathEvent(this);

        SetStopMovement();
    }

    private void SetStopMovement()
    {
        _rbAsteroid.velocity = Vector3.zero;
    }
}
