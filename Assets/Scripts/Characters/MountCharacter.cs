using UnityEngine;
using UnityEngine.Events;

public class MountCharacter : Character, IRidable
{
    [SerializeField] float speed = 6;
    [SerializeField] float gravity = 9.8f;
    [SerializeField, Range(0, 90)] float groundedAngle = 45;

    [SerializeField] UnityEvent _onMountEvent;
    [SerializeField] UnityEvent _onDismountEvent;
    [SerializeField] GameObject _fakePlayer;

    Vector3 _velocity;
    Rigidbody _rigidbody;

    bool _isGrounded = false;

    PlayerCharacter _rider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _controller = new MountController(this);
    }

    /// <summary>
    /// ïœë¨Ç∆ê˘âÒ
    /// </summary>
    public void Move(Vector2 input)
    {
        if (!ApplicationFocusManager.IsFocus) input = Vector3.zero;

        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(input.x, 0, input.y);

        var newVel = moveDir * speed;
        newVel.y = _velocity.y;
        _velocity = newVel;
    }

    /// <summary>
    /// èPï‡
    /// </summary>
    public void Dash()
    {
        print("Dash");
    }

    public void Jump()
    {
        print("Jump");
    }

    public void Drift()
    {
        print("Drift");
    }

    public void Interact()
    {

    }

    public void Mount(PlayerCharacter rider)
    {
        _rider = rider;
        _rider.SetController(null);
        _onMountEvent.Invoke();
    }

    public void Dismount()
    {
        _rider.SetController(new PlayerController(_rider));
        _rider = null;
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
        if (_isGrounded)
        {
            _velocity.y = 0;
        }
        else _velocity.y -= gravity * Time.deltaTime;

        _rigidbody.linearVelocity = _velocity;
    }
}
