using UnityEngine;
using UnityEngine.InputSystem;

public class Ride : MonoBehaviour
{
    bool inTrigger;
    MountController controller;

    private void Start()
    {
        controller = GetComponentInParent<MountController>();
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
            Controller.Player.gameObject.SetActive(false);
            controller.Mount();
            gameObject.SetActive(false);
        }
    }
}
