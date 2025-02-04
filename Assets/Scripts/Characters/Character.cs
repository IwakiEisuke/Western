using UnityEngine;

public class Character : MonoBehaviour
{
    protected IController _controller;

    public void SetController(IController newController)
    {
        _controller = newController;
    }
}
