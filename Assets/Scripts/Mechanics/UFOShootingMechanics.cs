using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOShootingMechanics : BaseShootingMechanics
{
    [SerializeField] protected float _shotFrequencyMin = 2f;
    [SerializeField] protected float _shotFrequencyMax = 5f;

    private Coroutine _timer;

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
