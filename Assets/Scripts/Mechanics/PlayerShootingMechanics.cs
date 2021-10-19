using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanics : BaseShootingMechanics
{
    [SerializeField] protected float _shotFrequency = 0f;

    private bool _isCanShoot = true;

    public override void Shoot()
    {
        if (_isCanShoot && CheckBulletsStorage())
        {
            _isCanShoot = false;

            InitializeBullet();
            StartCoroutine(FrequencyTimer());
        }
    }

    private IEnumerator FrequencyTimer()
    {
        yield return new WaitForSeconds(_shotFrequency);
        _isCanShoot = true;
    }
}
