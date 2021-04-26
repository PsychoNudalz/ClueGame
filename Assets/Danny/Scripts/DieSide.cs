using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for die side Gameobject
/// </summary>
public class DieSide : MonoBehaviour
{
    [SerializeField] private int value;
    private bool isOnGround;

    /// <summary>
    /// Set on ground = true if trigger is colliding with board
    /// </summary>
    /// <param name="other">Other collider to check</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            isOnGround = true;
        }
    }

    /// <summary>
    /// Set on ground = false if trigger is not colliding with board
    /// </summary>
    /// <param name="other">Other collider to check</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            isOnGround = false;
        }
    }

    /// <summary>
    /// Is the die side in contact with the board
    /// </summary>
    /// <returns>true if in contact with board, false if not</returns>
    public bool IsOnGround()
    {
        return isOnGround;
    }

    /// <summary>
    /// Get value on the opposite side of the die
    /// </summary>
    /// <returns>Value from opposite side of die</returns>
    public int GetSideValue()
    {
        return value;
    }
}
