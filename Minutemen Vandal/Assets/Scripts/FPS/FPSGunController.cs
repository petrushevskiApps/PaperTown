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
    [SerializeField]
    private ColorSwitchUI[] _actionBarButtons;
    [SerializeField]
    private RecoilController _recoil;
    [SerializeField]
    private Transform _aimOrientation;
    [SerializeField]
    private LineRenderer _laserLinerednerer;


    private List<Color> _availableColor;

    [Header("Bullet Data")]
    [SerializeField]
    private float _bulletSpeed = 20;

    private Color _paintColor
    {
        get
        {
            return _availableColor[_currentColorIndex];
        }
    }
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
    private int _currentColorIndex = 0;
    private bool isLevelActive = false;

    private void Awake()
    {
        GameManager.Instance.OnLevelStarted.AddListener(OnLevelStart);

        GameManager.Instance.OnLevelExited.AddListener(OnLevelExited);
        GameManager.Instance.OnLevelCompleted.AddListener(OnLevelExited);
        GameManager.Instance.OnLevelFailed.AddListener(OnLevelExited);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted.RemoveListener(OnLevelStart);
        GameManager.Instance.OnLevelExited.RemoveListener(OnLevelExited);
        GameManager.Instance.OnLevelCompleted.RemoveListener(OnLevelExited);
        GameManager.Instance.OnLevelFailed.RemoveListener(OnLevelExited);
    }

    private void OnLevelStart()
    {
        _availableColor = GameManager.Instance.GetLevelAvailableColors();
        _laserLinerednerer.material.color = _paintColor;
        isLevelActive = true;
    }
    private void OnLevelExited()
    {
        isLevelActive = false;
    }

    private void Start()
    {
        for (int i = 0; i < _actionBarButtons.Length; i++)
        {
            _actionBarButtons[i].SetUI(_availableColor[i], i == 0, (i+1).ToString());
        }
    }

    private void Update()
    {
        HandleActionBarInput();
        if (Input.GetMouseButton(0) && isLevelActive)
        {
            if(_recoil)
                _recoil.StartRecoil(_shootTime, 10, 10, 10);
            if (_lastShotTime + _shootTime < Time.time)
            {
                AudioManager.Instance.OnFire(transform.position);
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

    private void LateUpdate()
    {
        HandleLaserPointer();
    }

    private void HandleActionBarInput()
    {
        if (!isLevelActive) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveColor(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveColor(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveColor(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetActiveColor(4);
        }
    }

    private void HandleLaserPointer()
    {
        _laserLinerednerer.SetPosition(0, _laserLinerednerer.transform.position);
        var laserEndPos = _aimOrientation.position;
        RaycastHit hit;
        Physics.Raycast(_bulletSpawnPosition.position, (_aimOrientation.position - _laserLinerednerer.transform.position).normalized, out hit);
        {
            if (hit.collider != null)
            {
                laserEndPos = hit.point;
            }
        }
        _laserLinerednerer.SetPosition(1, laserEndPos);
    }

    private void SetActiveColor(int index)
    {
        if (index > _actionBarButtons.Length &&  index >= _availableColor.Count) return;

        _currentColorIndex = index;
        _laserLinerednerer.material.color = _paintColor;

        for (int i = 0; i < _actionBarButtons.Length; i++)
        {
            _actionBarButtons[i].SetActive(i == index);
        }
    }

}
