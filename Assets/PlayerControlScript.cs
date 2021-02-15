using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlScript : MonoBehaviour
{
    Mouse mouse;
    [SerializeField] GameObject cursor;
    [SerializeField] float yOffset = 1f;
    private void Awake()
    {
        //mouse = GetComponent<Mouse>();
    }

    private void FixedUpdate()
    {
        print(Mouse.current.position.ReadValue());
    }
}
