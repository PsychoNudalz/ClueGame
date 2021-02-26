using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSide : MonoBehaviour
{
    [SerializeField]
    private int value;
    private bool isOnGround;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            isOnGround = false;
        }
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    public int GetSideValue()
    {
        return value;
    }
}
