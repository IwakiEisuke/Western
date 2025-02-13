using UnityEngine;

public class PlayerController : IController
{
    PlayerCharacter _target;
    InputSystem_Actions _actions = new();
    bool _enable;

    public PlayerController(PlayerCharacter character)
    {
        _target = character;
        _actions.Player.Interact.performed += _ => _target.Interact();
        _actions.Player.Jump.performed += _ => _target.Jump();
        _actions.Player.Sprint.performed += _ => _target.Sprint(true);
        _actions.Player.Sprint.canceled += _ => _target.Sprint(false);
        _actions.Player.Move.performed += c => _target.Move(c.ReadValue<Vector2>());
        _actions.Player.Move.canceled += _ => _target.Move(Vector2.zero);
    }

    public void HandleInput()
    {
        
    }

    public void Enable()
    {
        _actions.Player.Enable();
        _enable = true;
    }

    public void Disable()
    {
        _actions.Player.Disable();
        _enable = false;
    }
}