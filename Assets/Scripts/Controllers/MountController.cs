using UnityEngine;

public class MountController : IController
{
    MountCharacter _target;
    InputSystem_Actions _actions = new();
    bool _enable;

    public MountController(MountCharacter mountCharacter)
    {
        _target = mountCharacter;
    }


    public void HandleInput()
    {
        if (!_enable) return;

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

    public void Enable()
    {
        _actions.Mount.Enable();
        _enable = true;
    }

    public void Disable()
    {
        _actions.Mount.Disable();
        _enable = false;
    }
}
