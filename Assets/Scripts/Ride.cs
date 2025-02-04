using UnityEngine;
using UnityEngine.InputSystem;

public class Ride : MonoBehaviour
{
    bool inTrigger;
    MountCharacter mount;

    
    private void Start()
    {
        mount = GetComponentInParent<MountCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


    void OnInteract(InputValue value)
    {
        print(value.isPressed);

        if (inTrigger && value.isPressed)
        {
            ControllerManager.Player.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
