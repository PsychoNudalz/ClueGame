using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTokenScript : MonoBehaviour
{
    [Header("Chracter Token Stuff")]
    [SerializeField] private CharacterEnum character;
    [SerializeField] private Color characterColour;
    [SerializeField] private string characterName;
    private StartTileScript startTile;
    private PlayerMasterController controller;

    [Header("Movement")]
    [SerializeField] Animator animator;
    [SerializeField] AnimationCurve movementGraph;
    [SerializeField] float timeToMove = 2;
    [SerializeField] float timeToDistance = .25f;
    [SerializeField] float startMoveTime;
    [SerializeField] bool isMove = false;
    [SerializeField] BoardTileScript targetTile;
    
    [Header("Tile")]
    [SerializeField] BoardTileScript currentTile;

    private RoomEntryPoint currentEntryPoint;
    private RoomScript currentRoom;
    private RoomEntryBoardTileScript currentExitPoint;
    private BoardTileScript roomExitTileTarget;
    private BoardManager boardManager;

    //Getters and Setters
    public CharacterEnum Character { get => character; }
    public Color CharacterColour { get => characterColour; set => characterColour = value; }
    public string CharacterName { get => characterName; set => characterName = value; }
    public BoardTileScript CurrentTile { get => currentTile; set => currentTile = value; }
    public RoomScript CurrentRoom { get => currentRoom; set => currentRoom = value; }


    // Start is called before the first frame update
    void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            UpdateTokenMovement();
        }
        else
        {

            if (currentEntryPoint != null)
            {
                if(Vector3.Distance(transform.position, currentEntryPoint.transform.position) == 0f)
                {
                    currentEntryPoint.RoomScript.AddPlayer(controller);
                    currentEntryPoint = null;
                    currentTile.GetComponent<BoardTileScript>().PlayerToken = null;
                    currentTile = null;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentEntryPoint.transform.position, Time.deltaTime);
                }
            }
            if (currentExitPoint != null)
            {
                if (Vector3.Distance(transform.position, currentExitPoint.transform.position) == 0f)
                {
                    currentTile = currentExitPoint;
                    currentExitPoint = null;
                    roomExitTileTarget = null;
                    MoveToken(roomExitTileTarget);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentExitPoint.transform.position, Time.deltaTime);
                }
            }
        }
    }

    internal PlayerMasterController GetController()
    {
        return controller;
    }

    public bool IsInRoom()
    {
        return currentRoom != null;
    }

    public void SetCharacter(CharacterEnum setCharacter, StartTileScript tile)
    {
        startTile = tile;
        currentTile = tile;
        //TODO Set start tile colour.
        SetCharacter(setCharacter);
    }

    public void SetCharacter(CharacterEnum setCharacter)
    {
        switch (setCharacter)
        {
            case CharacterEnum.MissScarlett:
                character = setCharacter;
                characterColour = new Color(255, 0, 0);
                characterName = "Miss Scarlett";
                break;
            case CharacterEnum.ColMustard:
                character = setCharacter;
                characterColour = new Color(255, 255, 0);
                characterName = "Col Mustard";
                break;
            case CharacterEnum.ProfPlum:
                character = setCharacter;
                characterColour = new Color(255, 0, 255);
                characterName = "Prof Plum";
                break;
            case CharacterEnum.RevGreen:
                character = setCharacter;
                characterColour = new Color(0, 255, 0);
                characterName = "Rev Green";
                break;
            case CharacterEnum.MrsPeacock:
                character = setCharacter;
                characterColour = new Color(0, 150, 255);
                characterName = "Mrs Peacock";
                break;
            case CharacterEnum.MrsWhite:
                character = setCharacter;
                characterColour = new Color(255, 255, 255);
                characterName = "Mrs White";
                break;
        }
        AssignToPlayerMaster();

        GetComponentInChildren<Renderer>().material.SetColor("_MainColour", characterColour);
        
    }

    public Color GetCharacterColour()
    {
        return characterColour;
    }

    public Vector2 GetGridPosition()
    {
        return currentTile.GridPosition;
    }
    /// <summary>
    /// assigns token to the correct player
    /// </summary>
    /// <returns> returns true if it can assign to the player, false if it can't find a matching Player </returns>
    bool AssignToPlayerMaster()
    {
        PlayerMasterController[] allPlayer = FindObjectsOfType<PlayerMasterController>();
        foreach (PlayerMasterController p in allPlayer)
        {
            if (character.Equals(p.GetCharacter()))
            {
                p.PlayerTokenScript = this;
                controller = p;
                return true;
            }
        }
        return false;
    }


    public void MoveToken(BoardTileScript newTile)
    {

        currentEntryPoint = null;
        currentExitPoint = null;
        targetTile = newTile;
        
        targetTile.SetToken(gameObject);
        isMove = true;
        startMoveTime = Time.time;
        timeToMove = (targetTile.GridPosition - currentTile.GridPosition).magnitude * timeToDistance;
        animator.SetTrigger("Lift");
    }

    public void MoveToken(Vector3 v)
    {
        isMove = true;
        startMoveTime = Time.time;
        timeToMove = (v - transform.position).magnitude * timeToDistance;
        animator.SetTrigger("Lift");
    }

    void UpdateTokenMovement()
    {
        if (Time.time > (startMoveTime + timeToMove))
        {
            currentTile = targetTile;
            transform.position = currentTile.transform.position;
            isMove = false;
            animator.SetTrigger("Place");
            currentRoom = null;
        }
        else
        {
            float currentPoint = movementGraph.Evaluate((Time.time- startMoveTime) / timeToMove);
            //print(currentPoint);
            transform.position = (currentPoint *(targetTile.transform.position - currentTile.transform.position))+ currentTile.transform.position;
        }
    }

    public void EnterRoom(RoomEntryPoint entryPoint)
    {
        currentEntryPoint = entryPoint;
    }

    internal void ExitRoom(RoomEntryBoardTileScript roomEntryBoardTileScript, BoardTileScript targetTile)
    {
        roomExitTileTarget = targetTile;
        currentExitPoint = roomEntryBoardTileScript;
    }

    public bool CanTakeShortcut()
    {
        return (currentRoom != null && currentRoom.HasShortcut());
    }

    public bool TakeShortcut()
    {
        if (CanTakeShortcut())
        {
            
            foreach(ShortcutBoardTileScript tile in boardManager.Shortcuts)
            {
                if (tile.ShortcutTo.Equals(currentRoom.Room)){
                    StartCoroutine(ShortcutMovement(tile));
                }
            }
        }
        return false;
    }

    IEnumerator ShortcutMovement(ShortcutBoardTileScript shortcutEnd)
    {
        //currentTile = currentRoom.ShortcutTile;
        transform.position = currentRoom.ShortcutTile.transform.position;
        animator.SetTrigger("StartShortcut");
        
        yield return new WaitForSeconds(2f);
        currentRoom = null;
        transform.position = shortcutEnd.transform.position;
        animator.SetTrigger("EndShortcut");
        yield return new WaitForSeconds(1.2f);
        currentTile = shortcutEnd;
        ShortcutBoardTileScript currentShortcut = currentTile.GetComponent<ShortcutBoardTileScript>();
        currentShortcut.RoomScript.AddPlayer(this.transform.GetComponent<PlayerMasterController>());
    }
}
