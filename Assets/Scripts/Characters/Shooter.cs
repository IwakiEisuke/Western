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
    [SerializeField] float _shootSpeed;
    [SerializeField] TargetingSystem _targeting;

    [SerializeField] Transform _lockOnTarget;

    InputSystem_Actions _actions;

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
        Debug.Log("LockOn");
        _lockOnTarget = _targeting.GetLockOnTarget();
    }

    public void Unlock(InputAction.CallbackContext obj)
    {
        Debug.Log("Unlock");
        _lockOnTarget = null;
    }
}