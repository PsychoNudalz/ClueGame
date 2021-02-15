using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionScript : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
