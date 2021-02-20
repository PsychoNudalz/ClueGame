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
    private CameraCloseUp mainCamera;
    private bool setCloseUpCamera;

    private void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>().GetComponent<CameraCloseUp>();
        dieSides = GetComponentsInChildren<DieSide>();
        dieRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        dieRigidbody.useGravity = false;
    }

    private void Update()
    {
        
        if (setCloseUpCamera)
        {
            mainCamera.SetCloseUp(CameraTarget.Centre);
        }
                
        if(dieRigidbody.IsSleeping() && !hasLanded && isThrown)
        {
            hasLanded = true;
            dieRigidbody.useGravity = false;
            CheckValue();
        }
        else if (dieRigidbody.IsSleeping() && dieValue == 0)
        {
            RollAgain();
        }
    }

    private void RollAgain()
    {
        Reset();
        Roll();
    }

    public void Roll()
    {
        if (hasLanded)
        {
            Reset();
        }
        
        if (!isThrown && !hasLanded)
        {
            GetComponent<MeshRenderer>().enabled = true;
            setCloseUpCamera = true;
            isThrown = true;
            dieRigidbody.useGravity = true;
            dieRigidbody.AddTorque(Random.Range(100, 500), Random.Range(100, 500), Random.Range(100, 500));
        }
        
    }

    public bool IsReadyToRoll()
    {
        return !isThrown || hasLanded;
    }

    public bool CanReset()
    {
        return hasLanded && isThrown;
    }

    public void Reset()
    {
        GetComponent<MeshRenderer>().enabled = false;
        transform.position = initialPosition;
        dieRigidbody.useGravity = false;
        isThrown = false;
        hasLanded = false;
        setCloseUpCamera = false;
        mainCamera.ClearCloseUp();
    }

    private void CheckValue()
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

    public int GetValue()
    {
        if (hasLanded)
        {
            return dieValue;
        }
        else return 0;
    }
}
