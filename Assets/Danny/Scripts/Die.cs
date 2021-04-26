using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// Script for the Die Gameobject
/// </summary>
public class Die : MonoBehaviour
{
    
    private DieSide[] dieSides;
    private Rigidbody dieRigidbody;
    private bool hasLanded;
    private bool isThrown;
    private Vector3 initialPosition;
    private int dieValue = -1;

    // Start is called before the first frame update
    private void Start()
    {
        Initialise();
    }

    /// <summary>
    /// Initialise Die
    /// </summary>
    private void Initialise()
    {
        dieSides = GetComponentsInChildren<DieSide>();
        dieRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        dieRigidbody.useGravity = false;
    }

    /// <summary>
    /// If die has been thrown, landed and stopped moving
    /// then update die value. If die stops on edge then
    /// roll again
    /// </summary>
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

    /// <summary>
    /// Roll again in event of die getting stuck on edge
    /// </summary>
    private void RollAgain()
    {
        ResetDie();
        RollDie();
    }

    /// <summary>
    /// Roll the die
    /// </summary>
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

    /// <summary>
    /// Is die ready to roll
    /// </summary>
    /// <returns>true if ready to roll, false if not</returns>
    public bool IsReadyToRoll()
    {
        return !isThrown || hasLanded;
    }

    /// <summary>
    /// Can the die be reset
    /// </summary>
    /// <returns>trueif can be reset, false if not</returns>
    public bool CanResetDie()
    {
        return hasLanded && isThrown;
    }

    /// <summary>
    /// Reset Die to original position
    /// </summary>
    public void ResetDie()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.position = initialPosition;
        dieRigidbody.useGravity = false;
        isThrown = false;
        hasLanded = false;
    }

    /// <summary>
    /// Set the current value of the die if on ground 0 if not.
    /// </summary>
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

    /// <summary>
    /// Get the value of the die
    /// </summary>
    /// <returns>Current value of the die, 0 if not valid</returns>
    public int GetValueDie()
    {
        if (hasLanded)
        {
            return dieValue;
        }
        else return 0;
    }
}
