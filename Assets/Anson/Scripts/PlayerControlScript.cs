using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlScript : MonoBehaviour
{
    [Header("Cursor Control")]
    Mouse mouse;
    [SerializeField] GameObject cursor;
    [SerializeField] float yOffset = .2f;
    [SerializeField] bool isMouse = true;
    [SerializeField] bool isController = false;
    [SerializeField] Camera camera;
    [SerializeField] LayerMask layerMask;

    [Header("Optimisation")]
    [SerializeField] Vector3 targetPos;
    [SerializeField] float refereshRate;
    [SerializeField] float timeNow_refereshRate;

    [Header("Other Player Components")]
    [SerializeField] PlayerMasterController playerMasterController;


    public GameObject Cursor { get => cursor; set => cursor = value; }
    public PlayerMasterController PlayerMasterController { get => playerMasterController; set => playerMasterController = value; }

    private void Awake()
    {
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }
        //mouse = GetComponent<Mouse>();
    }

    private void FixedUpdate()
    {
        if (isMouse && Time.time- timeNow_refereshRate > refereshRate)
        {
            timeNow_refereshRate = Time.deltaTime;
            CastUpdateWithMouse();
        }

        UpdateCursor();
    }

    public void MoveWithMouse(InputAction.CallbackContext inputAction)
    {
        isMouse = true;
        isController = false;
    }

    public void MoveWithAxis(InputAction.CallbackContext inputAction)
    {
        isMouse = false;
        isController = true;
    }


    public void MovePlayerToken(InputAction.CallbackContext inputAction)
    {
        if (inputAction.performed)
        {
            playerMasterController.MovePlayer();
        }
    }

    void CastUpdateWithMouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, yOffset)) - camera.transform.position, out hit, 500f, layerMask))
        {
            //print(Mouse.current.position.ReadValue());
            targetPos = hit.point + new Vector3(0, yOffset);

        }
        Debug.DrawRay(camera.transform.position, (camera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, yOffset)) - camera.transform.position) * 500f, Color.green);

    }

    void UpdateCursor()
    {
        cursor.transform.position = Vector3.Lerp(cursor.transform.position, targetPos, 40 * Time.deltaTime);
    }
}
