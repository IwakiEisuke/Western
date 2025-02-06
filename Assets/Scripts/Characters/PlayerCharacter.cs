using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : Character
{
    [SerializeField] float speed = 6;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpHeight = 2f;

    CharacterController _characterController;
    Vector3 _velocity;
    bool _isJumping;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 input)
    {
        if (!ApplicationFocusManager.IsFocus) input = Vector3.zero;

        var cameraLook = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var moveDir = cameraLook * new Vector3(input.x, 0, input.y);

        var newVel = moveDir * speed;
        newVel.y = _velocity.y;
        _velocity = newVel;
    }

    public void Jump()
    {
        _isJumping = true;
        _velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void Interact()
    {
        // ãﬂÇ≠Ç…É}ÉEÉìÉgÇ™Ç¢ÇΩÇÁèÊÇÈ
        if (true)
        {
            Mount();
        }
    }

    private void Mount()
    {
        // ãRèÊèàóù
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
