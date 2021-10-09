using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGunController : MonoBehaviour
{
    [SerializeField]
    private FPSBulletController _bulletPrefab;
    [SerializeField]
    private Transform _bulletSpawnPosition;
    [SerializeField]
    private int _maxRoundsPerSecond = 10;
    [SerializeField]
    private int _shotgunNumberOfBullets;
    [SerializeField]
    private float _shotgunSpreadAngle = 3;

    [Header("Bullet Data")]
    [SerializeField]
    private float _bulletSpeed = 20;
    [SerializeField]
    Color _paintColor;
    [SerializeField]
    private float _destroyAfterTime = 3;
    [SerializeField]
    private float _radius = 1;
    [SerializeField]
    private float _strength = 1;
    [SerializeField]
    private float _hardness = 1;

    private float _shootTime => 1f / (float)_maxRoundsPerSecond;
    private float _lastShotTime = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_lastShotTime + _shootTime < Time.time)
            {
                for (int j = 0; j < _shotgunNumberOfBullets; j++)
                {
                    var bullet = Instantiate(_bulletPrefab);
                    bullet.transform.position = _bulletSpawnPosition.position;
                    bullet.transform.forward = transform.forward;
                    var randomSpray = bullet.transform.localEulerAngles + new Vector3(Random.Range(-_shotgunSpreadAngle, _shotgunSpreadAngle), Random.Range(-_shotgunSpreadAngle, _shotgunSpreadAngle), Random.Range(-_shotgunSpreadAngle, _shotgunSpreadAngle));
                    bullet.transform.localEulerAngles = randomSpray;
                    bullet.SetBulletData(_paintColor, _bulletSpeed, _destroyAfterTime, _radius, _strength, _hardness);
                }
                _lastShotTime = Time.time;
            }
        }
    }

}
