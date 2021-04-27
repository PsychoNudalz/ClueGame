using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTokenScript : MonoBehaviour
{
    [SerializeField] private WeaponEnum weaponType;
    private float moveSpeed = 50f;
    private RoomScript currentRoom;
    private Animator animator;
    private Vector3 targetPosition;
    private bool isMoving;
    /// <summary>
    /// Getter and setter for tokens weapon type
    /// </summary>
    public WeaponEnum WeaponType { get => weaponType;}
    /// <summary>
    /// Getter and setter for tokens current room
    /// </summary>
    public RoomScript CurrentRoom { get => currentRoom; set => currentRoom = value; }

    /// <summary>
    /// Set required variables
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Move token if required
    /// </summary>
    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
                animator.SetTrigger("LowerToken");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
    /// <summary>
    /// Set room slot to move token to and start lift animation
    /// </summary>
    /// <param name="slotPosition"></param>
    internal void MoveToken(Vector3 slotPosition)
    {
        targetPosition = slotPosition;
        StartCoroutine(LiftTokenDelay());
    }

    /// <summary>
    /// Trigger lift animation then wait and start moving to new location
    /// </summary>
    /// <returns></returns>
    IEnumerator LiftTokenDelay()
    {
        animator.SetTrigger("LiftToken");
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
    }

    /// <summary>
    /// Method to get a weapon enum
    /// </summary>
    /// <param name="weapon">Weapon string for the WeaponEnum to return</param>
    /// <returns>WeaponEnum based on string entered or throws exception if not found</returns>
    public static WeaponEnum GetWeaponEnumFromString(String weapon)
    {
        switch (weapon)
        {
            case "Dagger":
                return WeaponEnum.Dagger;
            case "Candle Stick":
                return WeaponEnum.CandleStick;
            case "Revolver":
                return WeaponEnum.Revolver;
            case "Rope":
                return WeaponEnum.Rope;
            case "Lead Pipe":
                return WeaponEnum.LeadPipe;
            case "Spanner":
                return WeaponEnum.Spanner;
            default:
                throw new Exception("Weapon enum not found");
        }
    }
}
