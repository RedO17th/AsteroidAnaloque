using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShootingMechanics : BaseShootingMechanics
{
    private Coroutine _timer;

    private float _shotFrequencyMin = 2f;
    private float _shotFrequencyMax = 5f;

    public override void Constructor(SystemInitializer systemInitializer)
    {
        base.Constructor(systemInitializer);

        _shotFrequencyMin = systemInitializer.Data.UFOData.ShotFrequencyMin;
        _shotFrequencyMax = systemInitializer.Data.UFOData.ShotFrequencyMax;
    }

    public override void Shoot()
    {
        if (CheckBulletsStorage())
        {
            InitializeBullet();
            _timer = StartCoroutine(FrequencyTimer());
        }
    }

    private IEnumerator FrequencyTimer()
    {
        float time = Random.Range(_shotFrequencyMin, _shotFrequencyMax);
        yield return new WaitForSeconds(time);

        Shoot();
    }

    private void OnDisable()
    {
        if(_timer != null)
            StopCoroutine(_timer);
    }

    public override void TurnOffMechanics()
    {
        if (_timer != null) StopCoroutine(_timer);
    }
}
