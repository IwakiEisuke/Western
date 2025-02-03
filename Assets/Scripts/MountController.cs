using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MountController : MonoBehaviour
{
    [SerializeField] float speed = 6;
    [SerializeField] float gravity = 9.8f;
    [SerializeField, Range(0, 90)] float groundedAngle = 45;

    [SerializeField] UnityEvent _onMountEvent;
    [SerializeField] UnityEvent _onDismountEvent; 
    [SerializeField] GameObject _fakePlayer;

    Vector3 _velocity;
    float _verticalVelocity;
    Rigidbody _rigidbody;

    Vector2 _input;
    bool _isGrounded = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!ApplicationFocusManager.IsFocus) _input = Vector3.zero;

        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(_input.x, 0, _input.y);

        var newVel = moveDir * speed;
        newVel.y = _velocity.y;
        _velocity = newVel;
        _rigidbody.linearVelocity = _velocity;

        if (_isGrounded)
        {
            _velocity.y = 0;
        }
        else _velocity.y -= gravity * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = Vector3.Dot(collision.contacts[0].normal, Vector3.up) > Mathf.Cos(Mathf.Deg2Rad * groundedAngle);
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Dismount();
        }
    }

    public void Mount()
    {
        Controller.SetPlayer(GetComponent<PlayerInput>());
        _onMountEvent.Invoke();
    }

    public void Dismount()
    {
        Controller.DefaultPlayer.GetComponent<CharacterController>().transform.position = transform.position + Vector3.up;
        Controller.DefaultPlayer.gameObject.SetActive(true);
        Controller.SetPlayer(Controller.DefaultPlayer);
        _onDismountEvent.Invoke();
    }
}
