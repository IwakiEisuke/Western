using UnityEngine;

/// <summary>
/// 追従させるときにジッターが発生します
/// </summary>
public class Test_Camera : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _follow;

    [Header("Movement")]
    [SerializeField] float _baseRadius = 10;
    [SerializeField] float _minRadius = 10;

    [SerializeField] float _angleClamp = 80;
    [SerializeField] float _xSensitivity = 10;
    [SerializeField] float _ySensitivity = 10;

    [Header("Collision")]
    [SerializeField] float _collisionRadius;
    [SerializeField] LayerMask _layerMask;

    InputSystem_Actions _actions;
    float _rotAngle;
    float _heightAngle;
    Vector3 _cameraOffset;

    float _currentRadius;

    private void Awake()
    {
        _actions = new();
        _actions.Camera.Enable();
        _actions.Camera.Look.performed += Look_performed;

        _currentRadius = _baseRadius;
        _camera.transform.position = _follow.position + (-_camera.transform.forward * _currentRadius);
    }

    private void Update()
    {
        var isHit = Physics.SphereCast(_follow.position, _collisionRadius, _cameraOffset, out var hit, _currentRadius + _collisionRadius, _layerMask);

        if (isHit)
        {
            hit.point += hit.normal * _collisionRadius;
        }
        else
        {
            hit.point = _follow.position + _cameraOffset;
        }

        _camera.transform.position = hit.point;
        _camera.transform.LookAt(_follow.transform);
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var input = obj.ReadValue<Vector2>();
        _rotAngle += input.x * _xSensitivity * Time.deltaTime;
        _heightAngle = Mathf.Clamp(_heightAngle + input.y * _ySensitivity * Time.deltaTime, -_angleClamp, _angleClamp);
        _cameraOffset = new Vector3(Mathf.Sin(_rotAngle * Mathf.Deg2Rad), -Mathf.Sin(_heightAngle * Mathf.Deg2Rad), Mathf.Cos(_rotAngle * Mathf.Deg2Rad)) * _currentRadius;
    }

    private void OnDrawGizmos()
    {
        var isHit = Physics.SphereCast(_follow.position, _collisionRadius, _cameraOffset, out var hit, _currentRadius + _collisionRadius, _layerMask);

        if (isHit)
        {
            Gizmos.DrawLine(hit.point, hit.point + hit.normal * _collisionRadius);
            hit.point += hit.normal * _collisionRadius;
        }
        else
        {
            hit.point = _follow.position + _cameraOffset;
        }

        Gizmos.color = isHit ? Color.green : Color.red;
        Gizmos.DrawWireSphere(hit.point, _collisionRadius);
    }
}
