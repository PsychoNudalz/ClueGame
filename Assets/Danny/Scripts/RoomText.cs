using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomText : MonoBehaviour
{
    [SerializeField] bool lookAtCamera;
    private Camera mainCamera;
    private Vector3 initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtCamera)
        {
            Vector3 cameraDirection = transform.position - mainCamera.transform.position;
            Vector3 rotation = new Vector3();
            transform.LookAt(mainCamera.transform);
        }
    }
}
