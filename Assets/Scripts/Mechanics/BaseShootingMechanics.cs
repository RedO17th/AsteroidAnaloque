using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMechanic : MonoBehaviour
{
    protected SystemInitializer _systemInitializer;

    public virtual void Constructor(SystemInitializer systemInitializer)
    {
        _systemInitializer = systemInitializer;
    }

    public virtual void TurnOffMechanics() { }
}

public class BaseShootingMechanics : BaseMechanic
{
    [SerializeField] protected Transform _bulletSpawner;
    [SerializeField] protected BaseBullet _bulletPrefab;
    [SerializeField] protected int _amountBullets;

    public float BulletSpeed => _bulletSpeed;
    public float MaxWayLength { get; private set; } = 0;

    public List<BaseBullet> _bullets = new List<BaseBullet>();
    private const float _amountScreenParts = 2f;

    protected float _bulletSpeed = 0f;

    public override void Constructor(SystemInitializer systemInitializer)
    {
        base.Constructor(systemInitializer);

        GetMaxWayLength((ScreenSystem)_systemInitializer.GetSystem(SystemType.ScreenSys));

        _bulletSpeed = systemInitializer.Data.PlayerData.BulletSpeed;
    }

    protected void GetMaxWayLength(ScreenSystem screenSystem)
    {
        MaxWayLength = screenSystem.XMaxCoord * _amountScreenParts;
        if(MaxWayLength == 0)
            Debug.LogError($"BaseShootingMechanics.GetMaxWayLength: MaxWayLength = 0");
    }

    public void InitializePoolBullets()
    {
        for (int i = 0; i < _amountBullets; i++)
        {
            BaseBullet newBullet = CreateBullet();
            SetBullet(newBullet);
        }
    }

    protected BaseBullet CreateBullet()
    {
        BaseBullet newBullet = Instantiate(_bulletPrefab, _bulletSpawner.position, _bulletSpawner.rotation, _bulletSpawner);
        newBullet.Constructor(this);
        newBullet.Active(false);

        return newBullet;
    }

    private void SetBullet(BaseBullet bullet)
    {
        _bullets.Add(bullet);
    }

    public virtual void Shoot()
    {
        if (CheckBulletsStorage())
            InitializeBullet();
    }

    protected bool CheckBulletsStorage()
    {
        return _bullets.Count != 0;
    }

    protected void InitializeBullet()
    {
        BaseBullet bullet = GetCurrentBullet();
        bullet.Active();
        bullet.SetMovement();
    }

    protected BaseBullet GetCurrentBullet()
    {
        BaseBullet bullet = _bullets[0];
        _bullets.Remove(bullet);
        SetBullet(bullet);

        return bullet;
    }
}
