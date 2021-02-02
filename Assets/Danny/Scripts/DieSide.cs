using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSide : MonoBehaviour
{
    [SerializeField]
    private int value;
    private bool onGround;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            onGround = false;
        }
    }

    public bool IsOnGround()
    {
        return onGround;
    }

    internal int GetSideValue()
    {
        return value;
    }
}
