using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _muzzle;
    [SerializeField] float _shootSpeed;
    [SerializeField] TargetingSystem _targeting;

    InputSystem_Actions _actions;

    private void Awake()
    {
        _actions = new();
        _actions.Shoot.Lockon.performed += LockOn;
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
        _targeting.GetInteractTarget();
    }
}