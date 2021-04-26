using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Script to control the camera close up shots
/// </summary>
public class CameraCloseUp : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private bool testKeys;
    private Keyboard kb;
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

    /// <summary>
    /// If test keys = true - numpad / numbers control close ups
    /// lerp camera position towards close up point set
    /// </summary>
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

    /// <summary>
    /// Get the camera location target from point
    /// </summary>
    /// <param name="currentTarget">Target to get position for</param>
    /// <returns>Location of target close up</returns>
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

    /// <summary>
    /// Set close up point
    /// </summary>
    /// <param name="target">Enum representing close up point</param>
    public void SetCloseUp(CameraTarget target)
    {
        currentCameraTarget = target;
    }

    /// <summary>
    /// Clear camera close up
    /// </summary>
    /// <param name="delay">Add a delay</param>
    public void ClearCloseUp(float delay)
    {
        StartCoroutine(ClearCloseUpDelay(delay));
    }

    /// <summary>
    /// Clear camera close up
    /// </summary>
    public void ClearCloseUp()
    {
        currentCameraTarget = CameraTarget.Initial;
    }

    /// <summary>
    /// Delay then clear close up
    /// </summary>
    /// <param name="delay">Delay to wait for</param>
    IEnumerator ClearCloseUpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentCameraTarget = CameraTarget.Initial;
    }

    /// <summary>
    /// For testing camera close ups
    /// </summary>
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

    /// <summary>
    /// Set camera close up to room
    /// </summary>
    /// <param name="room">Room for close up</param>
    internal void SetRoomCloseUp(RoomEnum room)
    {
        foreach (CameraTarget target in Enum.GetValues(typeof(CameraTarget)))
        {
            if (target.ToString().Equals(room.ToString()))
            {
                SetCloseUp(target);
            }
        }
    }

    /// <summary>
    /// Set camera close up to character
    /// </summary>
    /// <param name="character">Character for close up</param>
    public void SetCharacterCloseUp(CharacterEnum character)
    {
        foreach (CameraTarget target in Enum.GetValues(typeof(CameraTarget)))
        {
            if (target.ToString().Equals(character.ToString()))
            {
                SetCloseUp(target);
            }
        }
    }
}
