using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpPointScript : MonoBehaviour
{
    [SerializeField] CameraTarget closeUpPointName;

    public void SetCloseUpPointName(CameraTarget pointName)
    {
        closeUpPointName = pointName;
    }

    public CameraTarget GetCloseUpPointName()
    {
        return closeUpPointName;
    }

    public Vector3 GetCameraPosition()
    {
        return transform.position;
    }
}
