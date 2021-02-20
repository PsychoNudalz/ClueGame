using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraTarget
{
    Study, Hall, Lounge, Library, Centre, DiningRoom, BilliardRoom, Conservatory, Ballroom, Kitchen,
    MissScarlett, ProfPlum, ColMustard, MrsPeacock, RevGreen, MrsWhite, Initial
};

public class CameraCloseUp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool testKeys;
    Keyboard kb;
    private Vector3 initialCameraPosition;
    private CameraTarget currentCameraTarget;
    private CloseUpPointScript[] closeUpPoints;
    // Start is called before the first frame update
    void Start()
    {
        currentCameraTarget = CameraTarget.Initial;
        initialCameraPosition = transform.position;
        closeUpPoints = FindObjectsOfType<CloseUpPointScript>();
        kb = InputSystem.GetDevice<Keyboard>();
        /*
         * For Testing
         */
        /*
        foreach(CloseUpPointScript point in closeUpPoints)
        {
            print(point.GetCloseUpPointName());
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (testKeys)
        {
            TestSetCamera();
        }
        if (transform.position != GetCameraPosition(currentCameraTarget))
            {
               transform.position = Vector3.Lerp(transform.position, GetCameraPosition(currentCameraTarget), moveSpeed * Time.deltaTime);
            }
    }


    Vector3 GetCameraPosition(CameraTarget currentTarget)
    {
        foreach(CloseUpPointScript point in closeUpPoints)
        {
            if (point.GetCloseUpPointName().Equals(currentTarget))
            {
                return point.GetCameraPosition();
            }
        }
        return initialCameraPosition;
    }

    public void SetCloseUp(CameraTarget target)
    {
        currentCameraTarget = target;
    }

    public void ClearCloseUp()
    {
        currentCameraTarget = CameraTarget.Initial;
    }

    /*
     * For testing camera close ups
     */
    private void TestSetCamera()
    {
        if (kb.numpad7Key.isPressed)
        {
            SetCloseUp(CameraTarget.Study);
        }
        else if (kb.numpad8Key.isPressed)
        {
            SetCloseUp(CameraTarget.Hall);
        }
        else if (kb.numpad9Key.isPressed)
        {
            SetCloseUp(CameraTarget.Lounge);
        }
        else if (kb.numpad4Key.isPressed)
        {
            SetCloseUp(CameraTarget.Library);
        }
        else if (kb.numpad5Key.isPressed)
        {
            SetCloseUp(CameraTarget.Centre);
        }
        else if (kb.numpad6Key.isPressed)
        {
            SetCloseUp(CameraTarget.DiningRoom);
        }
        else if (kb.numpad1Key.isPressed)
        {
            SetCloseUp(CameraTarget.BilliardRoom);
        }
        else if (kb.numpad2Key.isPressed)
        {
            SetCloseUp(CameraTarget.Ballroom);
        }
        else if (kb.numpad3Key.isPressed)
        {
            SetCloseUp(CameraTarget.Kitchen);
        }
        else if (kb.numpad0Key.isPressed)
        {
            SetCloseUp(CameraTarget.Conservatory);
        }
        else if (kb.digit1Key.isPressed)
        {
            SetCloseUp(CameraTarget.MissScarlett);
        }
        else if (kb.digit2Key.isPressed)
        {
            SetCloseUp(CameraTarget.ProfPlum);
        }
        else if (kb.digit3Key.isPressed)
        {
            SetCloseUp(CameraTarget.ColMustard);
        }
        else if (kb.digit4Key.isPressed)
        {
            SetCloseUp(CameraTarget.MrsPeacock);
        }
        else if (kb.digit5Key.isPressed)
        {
            SetCloseUp(CameraTarget.RevGreen);
        }
        else if (kb.digit6Key.isPressed)
        {
            SetCloseUp(CameraTarget.MrsWhite);
        }
        else if (kb.numpadPeriodKey.isPressed)
        {
            SetCloseUp(CameraTarget.Initial);
        }
    }
}
