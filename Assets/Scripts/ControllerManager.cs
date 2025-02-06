using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] Character _defaultPlayer;
    [SerializeField] CinemachineCamera _camera;

    public static Character DefaultPlayer { get; private set; }
    public static Character Player { get; private set; }
    static new CinemachineCamera camera;

    private void Start()
    {
        camera = _camera;
        DefaultPlayer = _defaultPlayer;
        SwitchController(_defaultPlayer);
    }

    public static void SwitchController(Character con)
    {
        if (Player)
        {
            Player.enabled = false;
        }

        Player = con;
        camera.Follow = Player.transform;
    }
}