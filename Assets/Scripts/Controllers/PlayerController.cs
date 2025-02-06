using UnityEngine;

public class PlayerController : IController
{
    InputSystem_Actions _actions = new();
    PlayerCharacter _target;

    public PlayerController(PlayerCharacter character)
    {
        _target = character;
        _actions.Player.Enable();
        _actions.Shoot.Enable();
    }

    public void HandleInput()
    {
        // Move
        var move = _actions.Player.Move.ReadValue<Vector2>();
        Debug.Log(move);
        _target.Move(move);

        // Jump
        if (_actions.Player.Jump.WasPerformedThisFrame())
        {
            _target.Jump();
        }

        // Ride
        if (_actions.Player.Interact.WasPerformedThisFrame())
        {
            Debug.Log("Ride");
        }
    }
}