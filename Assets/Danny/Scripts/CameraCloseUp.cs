using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCloseUp : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Vector3 initialCameraPosition;
    private Vector3 currentCameraTarget;
    private bool IsCloseUp;

    // Start is called before the first frame update
    void Start()
    {
        initialCameraPosition = transform.position;
        IsCloseUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCloseUp)
        {
            transform.position = Vector3.Lerp(transform.position, currentCameraTarget, 3f * Time.deltaTime);
        }
        else
        {
            if (transform.position != initialCameraPosition)
            {
               transform.position = Vector3.Lerp(transform.position, initialCameraPosition, 5f * Time.deltaTime);
            }
        }
    }

    public void SetCloseUp(Vector3 target)
    {
        currentCameraTarget = target + offset;
        IsCloseUp = true;
    }

    public void ClearCloseUp()
    {
        IsCloseUp = false;
    }
}
