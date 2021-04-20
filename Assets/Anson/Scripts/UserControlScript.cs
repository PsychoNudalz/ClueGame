using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// this is for handling the cursor controls
/// </summary>
public class UserControlScript : MonoBehaviour
{
    [Header("Cursor Control")]
    Mouse mouse;
    [SerializeField] GameObject cursor;
    [SerializeField] float yOffset = .2f;
    [SerializeField] bool isMouse = true;
    [SerializeField] bool isController = false;
    [SerializeField] Camera cameraObject;
    [SerializeField] LayerMask layerMask;

    [Header("Optimisation")]
    [SerializeField] Vector3 targetPos;
    [SerializeField] float refereshRate;
    [SerializeField] float timeNow_refereshRate;

    [Header("Other Player Components")]
    [SerializeField] RoundManager roundManager;
    [SerializeField] UserSelectionScript userSelectionScript;


    public GameObject Cursor { get => cursor; set => cursor = value; }
    public RoundManager RoundManager { get => roundManager; set => roundManager = value; }

    private void Awake()
    {
        if (cameraObject == null)
        {
            cameraObject = FindObjectOfType<Camera>();
        }
        if (!roundManager)
        {
            roundManager = FindObjectOfType<RoundManager>();
        }
        if (!userSelectionScript)
        {
            userSelectionScript = GetComponentInChildren<UserSelectionScript>();
        }
        //mouse = GetComponent<Mouse>();
    }

    private void FixedUpdate()
    {
        if (isMouse && Time.time - timeNow_refereshRate > refereshRate)
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
            BoardTileScript newTile = userSelectionScript.GetCurrentTile();
            if (newTile != null)
            {
                RoundManager.MovePlayer(newTile);
            }
        }
    }

    void CastUpdateWithMouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, yOffset)) - cameraObject.transform.position, out hit, 500f, layerMask))
        {
            //print(Mouse.current.position.ReadValue());
            targetPos = hit.point + new Vector3(0, yOffset);

        }
        Debug.DrawRay(cameraObject.transform.position, (cameraObject.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, yOffset)) - cameraObject.transform.position) * 500f, Color.green);

    }

    void UpdateCursor()
    {
        if (!cursor)
        {
            Debug.LogError("SDFGHJK");
        }
        cursor.transform.position = Vector3.Lerp(cursor.transform.position, targetPos, 40 * Time.deltaTime);
    }

}
