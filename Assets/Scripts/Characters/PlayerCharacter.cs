using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Character
{
    [SerializeField] float walkSpeed = 6;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float sprintSpeed = 12;
    [SerializeField] TargetingSystem _targeting;

    float _speed;

    CharacterController _characterController;
    Vector3 _velocity;
    bool _isJumping;
    Vector2 _input;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _controller = new PlayerController(this);
        _controller.Enable();
        _speed = walkSpeed;
    }

    public void Move(Vector2 input)
    {
        if (!ApplicationFocusManager.IsFocus) _input = Vector2.zero;
        else _input = input;
    }

    public void Sprint(bool enable)
    {
        print("Player Sprint " + enable);
        _speed = enable ? sprintSpeed : walkSpeed;
    }

    public void Jump()
    {
        print("Player Jump");
        if (!_isJumping)
        {
            _isJumping = true;
            _velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }

    public void Interact()
    {
        print("Player Interact " + (_targeting.GetInteractTarget() != null ? _targeting.GetInteractTarget() : "[null]"));

        // 近くにマウントがいたら乗る
        if (_targeting.TryGetComponentInInteractTarget<IRidable>(out var ridable))
        {
            Mount(ridable);
        }
    }

    private void Mount(IRidable mount)
    {
        print("Player Mount");
        // 騎乗処理
        mount.Mount(this);
        _characterController.enabled = false;
        enabled = false;
    }

    public void Dismount()
    {
        print("Player Dismount");
        _characterController.enabled = true;
        enabled = true;
        _velocity = Vector3.zero;
    }

    public override void UpdatePhysics()
    {
        // inputから速度を決定する
        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(_input.x, 0, _input.y);
        if (moveDir.sqrMagnitude > 0) transform.forward = moveDir;

        var newVel = moveDir * _speed;
        newVel.y = _velocity.y;
        _velocity = newVel;

        // 速度に基づいて移動
        _characterController.Move(_velocity * Time.deltaTime);

        // 接地判定
        if (_characterController.isGrounded)
        {
            _isJumping = false;
            _velocity.y = Mathf.MoveTowards(_velocity.y, 0, Time.deltaTime);
        }
        // 重力加速
        else _velocity.y -= gravity * Time.deltaTime;
    }
}