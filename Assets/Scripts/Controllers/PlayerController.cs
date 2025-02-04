using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : IController
{
    InputSystem_Actions _actions = new();

    PlayerCharacter _character;
    Vector2 _input;

    public PlayerController(PlayerCharacter character)
    {
        _character = character;
        _actions.Player.Enable();
    }

    public void HandleInput()
    {
        // Move
        var move = _actions.Player.Move.ReadValue<Vector2>();
        Debug.Log(move);
        _character.Move(move);

        // Jump
        if (_actions.Player.Jump.IsPressed())
        {
            _character.Jump();
        }

        // Ride

    }
}