using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Transform interactTarget;
    public bool isLockOn;

    [SerializeField] Transform _user;
    [SerializeField] float _lockOnRange = 20;
    [SerializeField] LayerMask _layerMask;
    
    readonly Collider[] _hits = new Collider[10];

    public Transform GetInteractTarget()
    {
        var size = Physics.OverlapSphereNonAlloc(_user.position, _lockOnRange, _hits, _layerMask);
        var nearest = _hits.Take(size).OrderBy(c => (c.transform.position - _user.position).sqrMagnitude).FirstOrDefault();

        if (nearest != null)
        {
            interactTarget = nearest.transform;
        }

        return interactTarget;
    }

    private void Update()
    {
        if (interactTarget != null)
            if ((_user.position - interactTarget.position).sqrMagnitude > _lockOnRange * _lockOnRange)
            {
                interactTarget = null;
            }

        GetInteractTarget();
    }

    public bool TryGetComponentInTarget<T>(out T component)
    {
        if (interactTarget)
        {
            return interactTarget.TryGetComponent(out component);
        }

        component = default;
        return false;
    }
}
