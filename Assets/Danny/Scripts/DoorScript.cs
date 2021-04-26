using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Script for the door Gameobject
/// </summary>
public class DoorScript : MonoBehaviour
{
    [SerializeField] private bool testKeysActive;
    private Animator doorAnimator;
    private bool isOpen;
    private Keyboard kb;

    /// <summary>
    /// Set Required variables
    /// </summary>
    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        kb = InputSystem.GetDevice<Keyboard>();
        isOpen = false;
    }

    /// <summary>
    /// If test keys active 0 key opens / closes the door
    /// </summary>
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
    /// <summary>
    /// Open the door
    /// </summary>
    public void OpenDoor()
    {
        isOpen = true;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }
    /// <summary>
    /// Close the door
    /// </summary>
    public void CloseDoor()
    {
        isOpen = false;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }
    /// <summary>
    /// Toggle door Open / Close
    /// </summary>
    public void ToggleDoorOpenClose()
    {
        isOpen = !isOpen;
        doorAnimator.SetBool("DoorOpen", isOpen);
    }
}
