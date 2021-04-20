using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponEnum {Dagger, CandleStick, Revolver, Rope, LeadPipe, Spanner}

public class WeaponTokenScript : MonoBehaviour
{
    [SerializeField] private WeaponEnum weaponType;
    private float moveSpeed = 50f;
    private RoomScript currentRoom;
    private Animator animator;

    private Vector3 targetPosition;
    private bool isMoving;

    public WeaponEnum WeaponType { get => weaponType;}
    public RoomScript CurrentRoom { get => currentRoom; set => currentRoom = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    internal void MoveToken(Vector3 slotPosition)
    {
        targetPosition = slotPosition;
        StartCoroutine(LiftTokenDelay());
    }

    IEnumerator LiftTokenDelay()
    {
        animator.SetTrigger("LiftToken");
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
    }

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
