using UnityEngine;

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

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        SetController(new PlayerController(this));
        _speed = walkSpeed;
    }

    public void Move(Vector2 input)
    {
        if (!ApplicationFocusManager.IsFocus) input = Vector3.zero;

        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(input.x, 0, input.y);
        if (moveDir.sqrMagnitude > 0) transform.forward = moveDir;

        var newVel = moveDir * _speed;
        newVel.y = _velocity.y;
        _velocity = newVel;
    }

    public void Sprint(bool enable)
    {
        print("Player Sprint " + enable);
        _speed = enable ? sprintSpeed : walkSpeed;
    }

    public void Jump()
    {
        print("Player Jump");
        _isJumping = true;
        _velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void Interact()
    {
        print("Player Interact " + (_targeting.GetInteractTarget() != null ? _targeting.GetInteractTarget() : "[null]"));

        // ãﬂÇ≠Ç…É}ÉEÉìÉgÇ™Ç¢ÇΩÇÁèÊÇÈ
        if (_targeting.TryGetComponentInInteractTarget<IRidable>(out var ridable))
        {
            Mount(ridable);
        }
    }

    private void Mount(IRidable mount)
    {
        print("Player Mount");
        // ãRèÊèàóù
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
        _characterController.Move(_velocity * Time.deltaTime);

        if (_characterController.isGrounded && !_isJumping)
        {
            _isJumping = false;
            _velocity.y = Mathf.MoveTowards(_velocity.y, 0, Time.deltaTime);
        }
        else _velocity.y -= gravity * Time.deltaTime;
    }
}