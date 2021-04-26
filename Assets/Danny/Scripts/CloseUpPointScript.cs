using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script for camera close up point Gameobject
/// </summary>
public class CloseUpPointScript : MonoBehaviour
{
    [SerializeField] private CameraTarget closeUpPointName;

    /// <summary>
    /// Set the close up point
    /// </summary>
    /// <param name="pointName">Enum representing focus point</param>
    public void SetCloseUpPointName(CameraTarget pointName)
    {
        closeUpPointName = pointName;
    }

    /// <summary>
    /// Get the close up point
    /// </summary>
    /// <returns>Enum representing the focus point</returns>
    public CameraTarget GetCloseUpPointName()
    {
        return closeUpPointName;
    }
    /// <summary>
    /// Get the camera position (transform position)
    /// </summary>
    /// <returns>position for the camera close up</returns>
    public Vector3 GetCameraPosition()
    {
        return transform.position;
    }
}
