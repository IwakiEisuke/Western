using System.Threading.Tasks;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class MountCharacter : Character, IRidable
{
    [Header("Movements")]
    [SerializeField] float accel = 6;
    [SerializeField] float maxSpeed = 10;
    [SerializeField] float dashSpeed = 15;
    [SerializeField] float dashDuration = 1;
    [SerializeField] float turnSpeed = 180;
    [SerializeField] float dismountedSpeedReduction = 10;
    [SerializeField] float canInteractMaxSpeed = 3;

    [Header("Others")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField, Range(0, 90)] float groundedAngle = 45;

    [SerializeField] UnityEvent _onMountEvent;
    [SerializeField] UnityEvent _onDismountEvent;
    [SerializeField] Transform _PlayerPos;

    float _currentSpeed;
    Rigidbody _rigidbody;
    Vector3 _velocity;

    bool _isGrounded = false;
    bool _isDash = false;

    PlayerCharacter _rider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controller = new MountController(this);
        _controller.Disable();
    }

    /// <summary>
    /// 変速と旋回
    /// </summary>
    public void Move(Vector2 input)
    {
        if (!ApplicationFocusManager.IsFocus) input = Vector3.zero;

        _rigidbody.rotation *= (Quaternion.AngleAxis(input.x * turnSpeed * Time.deltaTime, Vector3.up));

        if (!_isDash) // ダッシュ中は速度固定
        {
            _currentSpeed += input.y * accel * Time.deltaTime;
            if (_currentSpeed > maxSpeed)
            {
                _currentSpeed = maxSpeed;
            }
        }

        var newVel = transform.forward * _currentSpeed;
        newVel.y = _velocity.y;
        _velocity = newVel;
    }

    /// <summary>
    /// 襲歩
    /// </summary>
    async void Dash()
    {
        print("Mount Dash");
        _currentSpeed = dashSpeed;
        _isDash = true;
        await Task.Delay((int)(dashDuration * 1000));
        _isDash = false;
    }

    public void Jump()
    {
        print("Mount Jump");
    }

    public void Drift()
    {
        print("Mount Drift");
    }

    private void Interact()
    {

    }

    public void InteractOrDash()
    {
        print("Mount InteractOrDash");
        if (_currentSpeed < canInteractMaxSpeed)
        {
            Interact();
        }
        else
        {
            Dash();
        }
    }

    public void Mount(PlayerCharacter rider)
    {
        print("Mount Mount");
        _rider = rider;
        _rider.SetController(null);
        _controller.Enable();
        _rider.transform.position = _PlayerPos.position;
        _rider.transform.parent = transform;
        gameObject.layer = 0;
        _onMountEvent.Invoke();
    }

    public void Dismount()
    {
        print("Mount Dismount");
        _rider.transform.parent = null;
        _rider.SetController(new PlayerController(_rider));
        _rider.Dismount();
        _rider = null;
        this.SetController(null);
        gameObject.layer = 7;
        _onDismountEvent.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = Vector3.Dot(collision.contacts[0].normal, Vector3.up) > Mathf.Cos(Mathf.Deg2Rad * groundedAngle);
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    public override void UpdatePhysics()
    {
        if (!_rider) // 誰も乗っていなかったら水平速度を落とす
        {
            _currentSpeed = Mathf.MoveTowards(_currentSpeed, 0, dismountedSpeedReduction * Time.deltaTime);
            var newVel = transform.forward * _currentSpeed;
            newVel.y = _velocity.y;
            _velocity = newVel;
        }

        if (_isGrounded)
        {
            _velocity.y = 0;
        }
        else _velocity.y -= gravity * Time.deltaTime;

        _rigidbody.linearVelocity = _velocity;
    }
}
