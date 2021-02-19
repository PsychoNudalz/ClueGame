using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool testKeysActive;
    Animator doorAnimator;
    Keyboard kb;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        kb = InputSystem.GetDevice<Keyboard>();
    }

    private void Update()
    {
        if (testKeysActive)
        {
            if (kb.spaceKey.wasPressedThisFrame)
            {
                ToggleDoorClose();
            }
           
        }
    }

    public void ToggleDoorClose()
    {
        doorAnimator.SetBool("DoorOpen", !doorAnimator.GetBool("DoorOpen"));
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool("DoorOpen", true);
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool("DoorOpen", false);
    }
}
