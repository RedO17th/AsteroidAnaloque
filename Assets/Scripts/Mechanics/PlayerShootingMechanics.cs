using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanics : BaseShootingMechanics
{
    [SerializeField] protected float _shotFrequency = 0f;

    private bool _isCanShoot = true;

    private Coroutine _frequencyTimer;

    public override void Shoot()
    {
        Debug.Log($"PlayerShootingMechanics.Shoot");

        if (_isCanShoot && CheckBulletsStorage())
        {
            _isCanShoot = false;

            InitializeBullet();
            _frequencyTimer = StartCoroutine(FrequencyTimer());
        }
    }

    private IEnumerator FrequencyTimer()
    {
        yield return new WaitForSeconds(_shotFrequency);
        _isCanShoot = true;
    }

    public override void TurnOffMechanics()
    {
        if (_frequencyTimer != null) StopCoroutine(_frequencyTimer);

        _isCanShoot = true;
    }
}
