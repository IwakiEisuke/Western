using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 6;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpHeight = 2f;

    CharacterController _controller;

    Vector3 _velocity;
    float _verticalVelocity;

    Vector2 _input;

    bool _isJumping;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!ApplicationFocusManager.IsFocus) _input = Vector3.zero;

        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(_input.x, 0, _input.y);

        var newVel = moveDir * speed;
        newVel.y = _velocity.y;
        _velocity = newVel;
        _controller.Move(_velocity * Time.deltaTime);

        if (_controller.isGrounded)
        {
            _velocity.y = Mathf.MoveTowards(_velocity.y, 0, Time.deltaTime);
        }
        else _velocity.y -= gravity * Time.deltaTime;

    }

    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            _isJumping = true;
            _velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }
}