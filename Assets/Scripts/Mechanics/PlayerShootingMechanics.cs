using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingMechanics : BaseShootingMechanics
{
    private Coroutine _frequencyTimer;

    private bool _isCanShoot = true;
    private float _shotFrequency = 0f;

    public override void Constructor(SystemInitializer systemInitializer)
    {
        base.Constructor(systemInitializer);
        _shotFrequency = _systemInitializer.Data.PlayerData.ShotFrequency;
    }

    public override void Shoot()
    {
        if (_isCanShoot && CheckBulletsStorage())
        {
            _isCanShoot = false;
          
            InitializeBullet();
            SoundSystem.PlaySound(SoundSystem.SoundType.Shot);

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
