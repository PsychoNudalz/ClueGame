using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    [SerializeField] bool testKeysActive;
    Animator doorAnimator;
    bool isOpen;
    Keyboard kb;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        kb = InputSystem.GetDevice<Keyboard>();
        isOpen = false;
    }

    private void Update()
    {
        if (testKeysActive)
        {
            if (kb.digit0Key.wasReleasedThisFrame)
            {
                ToggleDoorOpenClose();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }

    private void CloseDoor()
    {
        isOpen = false;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }

    public void ToggleDoorOpenClose()
    {
        isOpen = !isOpen;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }
}
