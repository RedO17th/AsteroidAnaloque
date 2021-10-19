using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BasicCharacter
{
    //--------------------------> [TODO][FIX]
    private int _amountDamage = 1;

    public Vector3 LocalPosition
    {
        get => transform.localPosition;
        private set => transform.localPosition = value;
    }
    public Quaternion LocalRotation
    {
        get => transform.localRotation;
        private set => transform.localRotation = value;
    }

    private ShootingMechanics _shootingMechanics;

    private Rigidbody _playersRigidbody;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private float _maxWayLength = 0;
    private float _speed = 0f;

    public void Constructor(ShootingMechanics shootingMechanics)
    {
        _shootingMechanics = shootingMechanics;
        _speed = _shootingMechanics.BulletSpeed;
        _maxWayLength = _shootingMechanics.MaxWayLength;

        _startPosition = LocalPosition;
        _startRotation = LocalRotation;
        _playersRigidbody = GetComponent<Rigidbody>();

        _view.Initialize(this);
    }

    public void Active(bool state = true)
    {
        gameObject.SetActive(state);
    }

    public void SetMovement()
    {
        _playersRigidbody.AddForce(transform.forward * _speed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        CheckOutOfScreen();
    }
    private void CheckOutOfScreen()
    {
        if (Vector3.Distance(_startPosition, LocalPosition) >= _maxWayLength)
            DisactivateObject();
    }

    private void DisactivateObject()
    {
        _playersRigidbody.velocity = Vector3.zero;
        LocalPosition = _startPosition;
        LocalRotation = _startRotation;
        _shootingMechanics.SetBullet(this);
        Active(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BasicCharacter character = collision.collider.GetComponent<BaseObjectView>().Character;
        if (character)
        {
            character.TakeDamage(_amountDamage);
            DisactivateObject();
        }
    }
}
