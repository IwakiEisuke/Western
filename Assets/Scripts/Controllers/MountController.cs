using UnityEngine;
using UnityEngine.InputSystem;

public class MountController : IController
{
    InputSystem_Actions _actions = new();
    MountCharacter _target;

    public MountController(MountCharacter character)
    {
        _target = character;
        _actions.Mount.Enable();
        _actions.Shoot.Enable();
    }

    public void HandleInput()
    {
        var move = _actions.Mount.Move.ReadValue<Vector2>();
        _target.Move(move);

        if (_actions.Mount.Jump.WasPerformedThisFrame())
        {
            _target.Jump();
        }

        if (_actions.Mount.Interact.IsPressed())
        {
            _target.Dash();
        }

        if (_actions.Mount.Drift.IsPressed())
        {
            _target.Drift();
        }

        if (_actions.Mount.Dismount.WasPerformedThisFrame())
        {
            _target.Dismount();
        }
    }
}
