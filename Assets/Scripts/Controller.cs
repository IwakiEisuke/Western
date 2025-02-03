using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] PlayerInput _defaultPlayer;
    [SerializeField] CinemachineCamera _camera;

    public static PlayerInput DefaultPlayer { get; private set; }
    public static PlayerInput Player { get; private set; }
    static new CinemachineCamera camera;

    private void Start()
    {
        camera = _camera;
        DefaultPlayer = _defaultPlayer;
        SetPlayer(_defaultPlayer);
    }

    public static void SetPlayer(PlayerInput p)
    {
        if (Player)
        {
            Player.enabled = false;
        }

        Player = p;
        Player.enabled = true;
        camera.Follow = Player.transform;
    }
}