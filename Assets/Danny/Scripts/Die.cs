using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Die : MonoBehaviour
{
    [SerializeField] private Transform closeUpCameraPosition;

    private DieSide[] dieSides;
    private Rigidbody dieRigidbody;
    private bool hasLanded;
    private bool isThrown;
    private Vector3 initialPosition;
    private int dieValue = -1;
    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    private Vector3 closeUpOffset;
    private bool setCloseUpCamera;

    private void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        initialCameraPosition = mainCamera.transform.position;
        setCloseUpCamera = false;
        closeUpOffset = transform.position - closeUpCameraPosition.position;
        dieSides = GetComponentsInChildren<DieSide>();
        dieRigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        dieRigidbody.useGravity = false;
    }

    private void Update()
    {
        
        if (setCloseUpCamera)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, transform.position - closeUpOffset, 3f * Time.deltaTime);
        }
        else
        {
            if(mainCamera.transform.position != initialCameraPosition)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initialCameraPosition, 5f * Time.deltaTime);
            }
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
        GetComponent<MeshRenderer>().enabled = true;
        isThrown = true;
        dieRigidbody.useGravity = true;
        dieRigidbody.AddTorque(Random.Range(100, 1000), Random.Range(100, 1000), Random.Range(100, 1000));
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
