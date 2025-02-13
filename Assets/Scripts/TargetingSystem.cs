using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] Transform _interactTarget;
    [SerializeField] Transform _lockOnTarget;
    [SerializeField] Transform _user;
    [SerializeField] float _lockOnRange = 20;
    [SerializeField] float _interactRange = 5;
    [SerializeField] LayerMask _layerMask;

    readonly Collider[] _hits = new Collider[10];

    private void GetTarget(float targetRange)
    {
        // Trigger‚ÌEnter,Exit‚Å—v‘fŠÇ—‚µ‚½‚Ù‚¤‚ªŒy‚»‚¤
        var size = Physics.OverlapSphereNonAlloc(_user.position, targetRange, _hits, _layerMask);
        var orderByAngle = _hits.Take(size).Select(c => (c.transform, angle: Vector3.Angle(Camera.main.transform.forward, (c.transform.position - Camera.main.transform.position).normalized))).OrderBy(tp => tp.angle).ToArray();

        _interactTarget = orderByAngle.FirstOrDefault().transform;
        _lockOnTarget = orderByAngle.FirstOrDefault().transform;
    }

    public Transform GetLockOnTarget()
    {
        return _lockOnTarget;
    }

    public Transform GetInteractTarget()
    {
        return _interactTarget;
    }

    private void Update()
    {
        GetTarget(_lockOnRange);

        if (_lockOnTarget)
            Debug.DrawLine(transform.position, _lockOnTarget.position, Color.red);
        if (_interactTarget)
            Debug.DrawLine(transform.position, _interactTarget.position, Color.green);
    }

    private bool TryGetComponentInTarget<T>(Transform target, out T component)
    {
        if (target == null)
        {
            component = default;
            return false;
        }

        return target.TryGetComponent(out component);
    }

    public bool TryGetComponentInInteractTarget<T>(out T component)
    {
        return TryGetComponentInTarget(_interactTarget, out component);
    }

    public bool TryGetComponentInLockOnTarget<T>(out T component)
    {
        return TryGetComponentInTarget(_lockOnTarget, out component);
    }
}
