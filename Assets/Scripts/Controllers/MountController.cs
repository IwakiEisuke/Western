using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MountController : IController
{
    Vector2 _input;

    private void OnMove(InputValue value)
    {
        _input = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            
        }
    }

    public void HandleInput()
    {
        throw new System.NotImplementedException();
    }
}
