using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Die : MonoBehaviour
{
    //[SerializeField] private Transform closeUpCameraPosition;

    private DieSide[] dieSides;
    private Rigidbody dieRigidbody;
    private bool hasLanded;
    private bool isThrown;
    private Vector3 initialPosition;
    private int dieValue = -1;

    private void Start()
    {
        dieSides = GetComponentsInChildren<DieSide>();
        dieRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        dieRigidbody.useGravity = false;
    }

    private void Update()
    {
        if(dieRigidbody.IsSleeping() && !hasLanded && isThrown)
        {
            hasLanded = true;
            dieRigidbody.useGravity = false;
            CheckValueDie();
        }
        else if (dieRigidbody.IsSleeping() && dieValue == 0)
        {
            RollAgain();
        }
    }

    private void RollAgain()
    {
        ResetDie();
        RollDie();
    }

    public void RollDie()
    {
        if (hasLanded)
        {
            ResetDie();
        }
        
        if (!isThrown && !hasLanded)
        {
            GetComponent<MeshRenderer>().enabled = true;
            
            isThrown = true;
            dieRigidbody.useGravity = true;
            dieRigidbody.AddTorque(Random.Range(500, 1000), Random.Range(500, 1000), Random.Range(500, 1000));
        }
    }

    public bool IsReadyToRoll()
    {
        return !isThrown || hasLanded;
    }

    public bool CanResetDie()
    {
        return hasLanded && isThrown;
    }

    public void ResetDie()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.position = initialPosition;
        dieRigidbody.useGravity = false;
        isThrown = false;
        hasLanded = false;
    }

    private void CheckValueDie()
    {
        dieValue = 0;
        foreach(DieSide side in dieSides)
        {
            if (side.IsOnGround())
            {
                dieValue = side.GetSideValue();
            }
        }
    }

    public int GetValueDie()
    {
        if (hasLanded)
        {
            return dieValue;
        }
        else return 0;
    }
}
