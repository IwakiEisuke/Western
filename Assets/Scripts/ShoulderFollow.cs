using Unity.Cinemachine;
using UnityEngine;

public class ShoulderFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    public Transform lookAt;
    [SerializeField] Vector3 _shoulderOffset;
    [SerializeField] CinemachineCamera _camera;

    private void OnValidate()
    {
        if (_target)
            transform.position = _target.TransformPoint(_shoulderOffset);
    }

    void Update()
    {
        transform.position = _target.position + Quaternion.AngleAxis(Camera.main.transform.rotation.y, Vector3.up) * _shoulderOffset;
        transform.rotation = Quaternion.LookRotation(lookAt.position - _target.position);
    }
}
