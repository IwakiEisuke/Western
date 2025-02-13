using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 射撃の仮実装クラス。
/// AIで制御できるよう、入力を分離
/// </summary>
public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _muzzle;
    [SerializeField] Transform _gun;
    [SerializeField] float _shootSpeed;
    [SerializeField] TargetingSystem _targeting;

    [SerializeField] Transform _lockOnTarget;
    [SerializeField] CinemachineCamera _camera;

    [SerializeField] float _lockOnSpeed = 10;
    [SerializeField] float _lockOnWeight = 4;
    [SerializeField] Vector3 _lockOnOffset;

    [SerializeField] CinemachineCamera _lockOnCamera;
    [SerializeField] Transform _lockOnCameraLookAt;
    [SerializeField] float _tt = 10;
    Transform _prevLockOnTarget;

    InputSystem_Actions _actions;
    bool _isLockOn;

    private void Awake()
    {
        _actions = new();
        _actions.Shoot.Lockon.performed += LockOn;
        _actions.Shoot.Lockon.canceled += Unlock;
        _actions.Shoot.Fire.performed += Fire;
    }

    void OnEnable()
    {
        _actions.Shoot.Enable();
    }

    void OnDisable()
    {
        _actions.Shoot.Disable();
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire");
        var proj = Instantiate(_projectile, _muzzle.position, _muzzle.rotation);
        proj.GetComponent<Rigidbody>().linearVelocity = proj.transform.forward * _shootSpeed;
    }

    public void LockOn(InputAction.CallbackContext obj)
    {
        if (LockOn())
        {
            _lockOnCameraLookAt.position = _lockOnTarget.position;
        }
    }

    bool LockOn()
    {
        Debug.Log("LockOn");

        _lockOnTarget = _targeting.GetLockOnTarget();
        if (_lockOnTarget)
        {
            _isLockOn = true;
            _lockOnCamera.Priority.Value = 1;
            return true;
        }
        return false;
    }

    public void Unlock(InputAction.CallbackContext obj)
    {
        Debug.Log("Unlock");
        _isLockOn = false;
        _gun.localRotation = Quaternion.identity;

        _lockOnCamera.Priority.Value = -1;
        _camera.ForceCameraPosition(_lockOnCamera.transform.position, _lockOnCamera.transform.rotation);
    }

    private void Update()
    {
        if (_isLockOn)
        {
            if (!_lockOnTarget.gameObject.activeInHierarchy)
            {
                LockOn();
                
                if (!_lockOnTarget)
                {
                    _isLockOn = false;
                    return;
                }
            }

            _gun.LookAt(_lockOnTarget);
            _lockOnCameraLookAt.position = Vector3.Lerp(_lockOnCameraLookAt.position, _lockOnTarget.position, _tt * Time.deltaTime);
            //var forward = Vector3.RotateTowards(_camera.transform.forward, _lockOnTarget.position - (_camera.transform.position + _lockOnOffset), Time.deltaTime, Time.deltaTime);
            //_camera.ForceCameraPosition(transform.position + -forward * _orbitalFollow.Radius, Quaternion.LookRotation(forward));
        }
    }
}