using UnityEngine;

public class MountController : IController
{
    MountCharacter _target;
    InputSystem_Actions _actions = new();
    bool _enable;

    public MountController(MountCharacter mountCharacter)
    {
        _target = mountCharacter;
        _actions.Mount.Jump.performed += _ => _target.Jump();
        _actions.Mount.Interact.performed += _ => _target.InteractOrDash();
        _actions.Mount.Drift.performed += _ => _target.Drift();
        _actions.Mount.Dismount.performed += _ => _target.Dismount();
    }


    public void HandleInput()
    {
        if (!_enable) return;

        var move = _actions.Mount.Move.ReadValue<Vector2>();
        _target.Move(move);
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
