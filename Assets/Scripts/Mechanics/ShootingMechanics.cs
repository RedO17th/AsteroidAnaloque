using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanics : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _amountBullets;

    [SerializeField] private float _shotFrequency = 0f;
    [SerializeField] private float _bulletSpeed = 0f;

    public float BulletSpeed => _bulletSpeed;
    public float MaxWayLength { get; private set; } = 0;

    private SystemInitializer _systemInitializer;
    private BasicMovingCharacter _character;

    public List<Bullet> _bullets = new List<Bullet>();
    private const float _amountScreenParts = 2f;
    private bool _isCanShoot = true;

    public void Constructor(PlayerManagerSystem playerManager)
    {
        _systemInitializer = playerManager.SystemInitializer;
        _character = playerManager.Player;

        GetMaxWayLength((ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys));
        InitializePoolBullets();
    }

    public void GetMaxWayLength(ScreenSystem screenSystem)
    {
        MaxWayLength = screenSystem.XMaxCoord * _amountScreenParts;
        if(MaxWayLength == 0)
            Debug.LogError($"BaseShootingMechanics.GetMaxWayLength: MaxWayLength = 0");
    }

    private void InitializePoolBullets()
    {
        for (int i = 0; i < _amountBullets; i++)
        {
            Bullet newBullet = CreateBullet();
            SetBullet(newBullet);
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation, _bulletSpawner);
        newBullet.Constructor(this);
        newBullet.Active(false);

        SetIgnoreCollisionWithObject(_character.ViewObject.Collider, newBullet.ViewObject.Collider);

        return newBullet;
    }

    public void SetIgnoreCollisionWithObject(Collider firstObj, Collider secondObj)
    {
        Physics.IgnoreCollision(firstObj, secondObj);
    }

    public void SetBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }

    public void Shoot()
    {
        if (_isCanShoot && CheckBulletsStorage())
        {
            _isCanShoot = false;

            InitializeBullet();
            StartCoroutine(FrequencyTimer());
        }
    }

    private bool CheckBulletsStorage()
    {
        return _bullets.Count != 0;
    }

    private void InitializeBullet()
    {
        Bullet bullet = GetCurrentBullet();
        bullet.Active();
        bullet.SetMovement();
    }

    private Bullet GetCurrentBullet()
    {
        Bullet bullet = _bullets[0];
        _bullets.Remove(bullet);

        return bullet;
    }

    private IEnumerator FrequencyTimer()
    {
        yield return new WaitForSeconds(_shotFrequency);
        _isCanShoot = true;
    }
}
