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
    private GameObject players;
    private GameObject rooms;
    private GameObject tiles;
    private GameObject shortcuts;
    int boardWidth;
    int boardHeight;

    void Awake()
    {
        GenerateBoardArrayFromCSV();
        boardHeight = boardStringArray.Length;
        boardWidth = boardStringArray[0].Length;
        BuildBoard();
        boardManager = GetComponentInParent<BoardManager>();
        boardManager.CreateBoardArray(tiles.GetComponentsInChildren<BoardTileScript>(),boardHeight,boardWidth);
    }

    private void GenerateBoardArrayFromCSV()
    {
        TextAsset boardCSV = Resources.Load("BoardLayout") as TextAsset;
        string[] boardRows = boardCSV.text.TrimEnd().Split('\n');
        List<string[]> boardList = new List<string[]>();
        for (int i = 0; i < boardRows.Length; i++)
        {
            boardList.Add(boardRows[i].Split(','));
        }

        boardStringArray = boardList.ToArray();
        /*
        //Print Array for testing
        for (int i = 0; i < boardArray.Length; i++)
        {
            for (int j = 0; j < boardArray[i].Length; j++)
            {
                print("row - " + i + " col - " + j + " - " + boardArray[i][j].ToString());
            }
        }
        */
    }

    void BuildBoard()
    {

        tiles = new GameObject("Tiles");
        tiles.transform.parent = this.transform;
        players = new GameObject("Player Tokens");
        players.transform.parent = this.transform;
        rooms = new GameObject("Rooms");
        rooms.transform.parent = this.transform;
        shortcuts = new GameObject("Shortcuts");
        shortcuts.transform.parent = this.transform;
        

        int x = -1;
        int z = -1;
        


        for (int row = boardStringArray.Length -1; row >= 0; row--)
        {
            GameObject rowObject = new GameObject("Row - " + (boardStringArray.Length - row - 1));
            rowObject.transform.parent = tiles.transform;
            z++;
            x = -1;
            for(int col = 0; col < boardStringArray[row].Length; col++)
            {
                x++;
                string[] square = boardStringArray[row][col].Split(':');
                switch (square[0])
                {
                    case "X":
                        CreateBoardTile(x, z, rowObject).GetComponent<BoardTileScript>();
                        break;
                    case "S":
                        CreateStartTile(x, z, square[1], rowObject).GetComponent<BoardTileScript>();
                        break;
                    case "E":
                        CreateRoomEntranceTile(x, z, square[1], square[2], rowObject).GetComponent<BoardTileScript>();
                        break;
                    case "SC":
                        CreateShortcutTile(x, z, square[1], square[2]).GetComponent<BoardTileScript>();
                        break;
                    case "R":
                        CreateRoom(x, z, square[1]);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private GameObject CreateShortcutTile(int x, int z, string room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject shortcutTile = GameObject.Instantiate(this.shortcutTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , shortcuts.transform);
        shortcutTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        shortcutTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        return shortcutTile;
        //todo set room
    }


    private GameObject CreateRoomEntranceTile(int x, int z, string Room, string rotation, GameObject parent)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject roomEntryTile = GameObject.Instantiate(roomEntryTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , parent.transform);
        roomEntryTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        roomEntryTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        return roomEntryTile;
        //todo set room
    }

    private GameObject CreateStartTile(int x, int z, string player, GameObject parent)
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
        GameObject startingTile = GameObject.Instantiate(startingTilePrefab, new Vector3(x, 0, z), transform.rotation, parent.transform);
        startingTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", playerColour);
        startingTile.GetComponentInChildren<StartSpace>().SetTileText(name);
        startingTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        GameObject playerPiece = GameObject.Instantiate(playerPiecePrefab, new Vector3(x, 0, z), transform.rotation, players.transform);
        playerPiece.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", playerColour);
        return startingTile;
    }

    private GameObject CreateBoardTile(int x, int z, GameObject parent)
    {
        GameObject boardTile = GameObject.Instantiate(generalTilePrefab, new Vector3(x, 0, z), transform.rotation, parent.transform);
        boardTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        boardTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        return boardTile;
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
        room.transform.parent = rooms.transform;
        return room;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
