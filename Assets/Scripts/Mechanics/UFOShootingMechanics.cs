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
            Debug.Log($"UFOShootingMechanics.Shoot");
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
        StopCoroutine(_timer);
    }
}
