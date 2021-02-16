using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [Header("Player Colours")]
    [SerializeField] Color missScarlettColour;
    [SerializeField] Color profPlumColour;
    [SerializeField] Color colMustardColour;
    [SerializeField] Color mrsPeacockColour;
    [SerializeField] Color revGreenColour;
    [SerializeField] Color mrsWhiteColour;
    [Space]
    [Header("Tile Colour")]
    [SerializeField] Color generalTileColour;
    [Space]
    [Header("Player Piece Prefab")]
    [SerializeField] GameObject playerPiecePrefab;
    [Header("Tile Prefabs")]
    [SerializeField] GameObject generalTilePrefab;
    [SerializeField] GameObject startingTilePrefab;
    [SerializeField] GameObject roomEntryTilePrefab;
    [SerializeField] GameObject shortcutTilePrefab;
    [Space]
    [Header("RoomPrefabs")]
    [SerializeField] GameObject StudyPrefab;
    [SerializeField] GameObject HallPrefab;
    [SerializeField] GameObject LoungePrefab;
    [SerializeField] GameObject LibraryPrefab;
    [SerializeField] GameObject CentrePrefab;
    [SerializeField] GameObject DiningRoomPrefab;
    [SerializeField] GameObject BilliardRoomPrefab;
    [SerializeField] GameObject ConservatoryPrefab;
    [SerializeField] GameObject BallroomPrefab;
    [SerializeField] GameObject KitchenPrefab;

    private BoardManager boardManager;
    private string[][] boardStringArray;
    private BoardTileScript[][] boardManagerArray;

    // Start is called before the first frame update
    void Start()
    {
        boardManager = GetComponentInParent<BoardManager>();
        boardStringArray = transform.parent.GetComponent<BoardArrayGenerator>().GetBoardArray();
        boardManagerArray = new BoardTileScript[boardStringArray.Length][];
        BuildBoard();
    }

    void BuildBoard()
    {
        int x = -1;
        int z = -1;

        for(int row = boardStringArray.Length -1; row >= 0; row--)
        {
            BoardTileScript[] rowArray = new BoardTileScript[boardStringArray[row].Length];
            z++;
            x = -1;
            for(int col = 0; col < boardStringArray[row].Length; col++)
            {
                x++;
                string[] square = boardStringArray[row][col].Split(':');
                switch (square[0])
                {
                    case "X":
                        rowArray[col] = CreateBoardTile(x, z).GetComponent<BoardTileScript>();
                        break;
                    case "S":
                        rowArray[col] = CreateStartTile(x, z, square[1]).GetComponent<BoardTileScript>();
                        break;
                    case "E":
                        rowArray[col] = CreateRoomEntranceTile(x, z, square[1], square[2]).GetComponent<BoardTileScript>();
                        break;
                    case "R":
                        CreateRoom(x, z, square[1]);
                        rowArray[col] = null;
                        break;
                    case "SC":
                        CreateShortcutTile(x, z, square[1], square[2]);
                        rowArray[col] = null;
                        break;
                    default:
                        rowArray[col] = null;
                        break;
                }
            }
            boardManagerArray[row] = rowArray;
        }
        boardManager.boardTileArray = boardManagerArray;
    }

    private GameObject CreateShortcutTile(int x, int z, string room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject shortcutTile = GameObject.Instantiate(this.shortcutTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , transform);
        shortcutTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        return shortcutTile;
        //todo set room
    }

    private GameObject CreateRoom(int x, int z, string roomName)
    {
        GameObject room = null;
        switch (roomName)
        {
            case "Study":
                room = GameObject.Instantiate(StudyPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Hall":
                room = GameObject.Instantiate(HallPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Lounge":
                room = GameObject.Instantiate(LoungePrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Library":
                room = GameObject.Instantiate(LibraryPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Centre":
                room = GameObject.Instantiate(CentrePrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Dining":
                room = GameObject.Instantiate(DiningRoomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Billiard":
                room = GameObject.Instantiate(BilliardRoomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Conservatory":
                room = GameObject.Instantiate(ConservatoryPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Ballroom":
                room = GameObject.Instantiate(BallroomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Kitchen":
                room = GameObject.Instantiate(KitchenPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            default:
                break;
        }
        return room;
    }

    private GameObject CreateRoomEntranceTile(int x, int z, string Room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject roomEntryTile = GameObject.Instantiate(roomEntryTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , transform);
        roomEntryTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        roomEntryTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        return roomEntryTile;
        //todo set room
    }

    private GameObject CreateStartTile(int x, int z, string player)
    {
        
        Color playerColour;
        string name = "";
        switch (player)
        {
            case "missScarlett":
                playerColour = missScarlettColour;
                name = "Miss\nScarlett";
                break;
            case "profPlum":
                playerColour = profPlumColour;
                name = "Prof\nPlum";
                break;
            case "colMustard":
                playerColour = colMustardColour;
                name = "Col\nMustard";
                break;
            case "mrsPeacock":
                playerColour = mrsPeacockColour;
                name = "Mrs\nPeacock";
                break;
            case "revGreen":
                playerColour = revGreenColour;
                name = "Rev\nGreen";
                break;
            case "mrsWhite":
                playerColour = mrsWhiteColour;
                name = "Mrs\nWhite";
                break;
            default:
                playerColour = Color.white;
                name = "";
                break;
        }
        GameObject startingTile = GameObject.Instantiate(startingTilePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        startingTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", playerColour);
        startingTile.GetComponentInChildren<StartSpace>().SetTileText(name);
        startingTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        GameObject playerPiece = GameObject.Instantiate(playerPiecePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        playerPiece.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", playerColour);
        return startingTile;
    }

    private GameObject CreateBoardTile(int x, int z)
    {
        GameObject boardTile = GameObject.Instantiate(generalTilePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        boardTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        boardTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        return boardTile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
