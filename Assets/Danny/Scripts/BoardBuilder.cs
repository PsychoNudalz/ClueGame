using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    private TextAsset boardCSV;
    //Prefabs
    //Player Token
    private GameObject playerPiecePrefab;
    //Board Tiles
    private GameObject generalTilePrefab;
    private GameObject startingTilePrefab;
    private GameObject roomEntryTilePrefab;
    private GameObject shortcutTilePrefab;
    //Rooms
    private GameObject BallroomPrefab;
    private GameObject BilliardRoomPrefab;
    private GameObject CentrePrefab;
    private GameObject ConservatoryPrefab;
    private GameObject DiningRoomPrefab;
    private GameObject HallPrefab;
    private GameObject KitchenPrefab;
    private GameObject LibraryPrefab;
    private GameObject LoungePrefab;
    private GameObject StudyPrefab;

    private BoardManager boardManager;
    private string[][] boardStringArray;
    private GameObject players;
    private GameObject startTiles;
    private GameObject roomEntryTiles;
    private GameObject rooms;
    private GameObject tiles;
    private GameObject shortcuts;
    int boardWidth;
    int boardHeight;

    void Awake()
    {
        Initialise();
    }

    private void Initialise()
    {
        LoadResources();
        GenerateBoardArrayFromCSV();
        boardHeight = boardStringArray.Length;
        boardWidth = boardStringArray[0].Length;
        BuildBoard();
        boardManager = GetComponentInParent<BoardManager>();
        boardManager.CreateBoardArray(tiles.GetComponentsInChildren<BoardTileScript>(), boardHeight, boardWidth);
    }

    private void LoadResources()
    {
        //Load board csv file
        boardCSV = Resources.Load("Danny/BoardLayout") as TextAsset;
        //Load prefabs from resources
        //Player token
        playerPiecePrefab = Resources.Load("Danny/Prefabs/PlayerToken/PlayerToken") as GameObject;
        //Tiles
        generalTilePrefab = Resources.Load("Danny/Prefabs/Tiles/(General)BoardTile") as GameObject;
        startingTilePrefab = Resources.Load("Danny/Prefabs/Tiles/(Start)BoardTile") as GameObject;
        roomEntryTilePrefab = Resources.Load("Danny/Prefabs/Tiles/(RoomEntry)BoardTile") as GameObject;
        shortcutTilePrefab = Resources.Load("Danny/Prefabs/Tiles/(Shortcut)BoardTile") as GameObject;
        //Rooms
        BallroomPrefab = Resources.Load("Danny/Prefabs/Rooms/Ballroom") as GameObject;
        BilliardRoomPrefab = Resources.Load("Danny/Prefabs/Rooms/BilliardRoom") as GameObject;
        CentrePrefab = Resources.Load("Danny/Prefabs/Rooms/Centre") as GameObject;
        ConservatoryPrefab = Resources.Load("Danny/Prefabs/Rooms/Conservatory") as GameObject;
        DiningRoomPrefab = Resources.Load("Danny/Prefabs/Rooms/DiningRoom") as GameObject;
        HallPrefab = Resources.Load("Danny/Prefabs/Rooms/Hall") as GameObject;
        KitchenPrefab = Resources.Load("Danny/Prefabs/Rooms/Kitchen") as GameObject;
        LibraryPrefab = Resources.Load("Danny/Prefabs/Rooms/Library") as GameObject;
        LoungePrefab = Resources.Load("Danny/Prefabs/Rooms/Lounge") as GameObject;
        StudyPrefab = Resources.Load("Danny/Prefabs/Rooms/Study") as GameObject;
    }

    private void GenerateBoardArrayFromCSV()
    {
        
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

    private void BuildBoard()
    {
        tiles = new GameObject("Tiles");
        tiles.transform.parent = this.transform;
        players = new GameObject("Player Tokens");
        players.transform.parent = this.transform;
        startTiles = new GameObject("Start Tiles");
        startTiles.transform.parent = this.transform;
        roomEntryTiles = new GameObject("Room Entry Tiles");
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
                        CreateBoardTile(x, z, rowObject);
                        break;
                    case "S":
                        CreateStartTile(x, z, square[1], rowObject);
                        break;
                    case "E":
                        CreateRoomEntranceTile(x, z, square[1], square[2], rowObject);
                        break;
                    case "SC":
                        CreateShortcutTile(x, z, square[1], square[2]);
                        break;
                    case "R":
                        CreateRoom(x, z, square[1]);
                        break;
                    default:
                        break;
                }
            }
            if (rowObject.transform.childCount.Equals(0))
            {
                GameObject.Destroy(rowObject);
            }
        }
    }

    private void CreateShortcutTile(int x, int z, string room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject shortcutTile = GameObject.Instantiate(this.shortcutTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , shortcuts.transform);
        ShortcutBoardTileScript tileScript = shortcutTile.GetComponent<ShortcutBoardTileScript>();
        tileScript.GridPosition = new Vector2(x, z);
        switch (room)
        {
            case "Kitchen":
                {
                    shortcutTile.name = "Study ==> Kitchen - Shortcut Tile";
                    tileScript.SetShortcutRooms(Room.Study, Room.Kitchen);
                    break;
                }
            case "Study":
                {
                    shortcutTile.name = "Kitchen ==> Study - Shortcut Tile";
                    tileScript.SetShortcutRooms(Room.Kitchen, Room.Study);
                    break;
                }
            case "Lounge":
                {
                    shortcutTile.name = "Conservatory ==> Lounge - Shortcut Tile";
                    tileScript.SetShortcutRooms(Room.Conservatory, Room.Lounge);
                    break;
                }
            case "Conservatory":
                {
                    shortcutTile.name = "Lounge ==> Conservatory - Shortcut Tile";
                    tileScript.SetShortcutRooms(Room.Lounge, Room.Conservatory);
                    break;
                }
        }
    }

    private void CreateRoomEntranceTile(int x, int z, string Room, string rotation, GameObject parent)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject roomEntryTile = GameObject.Instantiate(roomEntryTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , parent.transform);
        roomEntryTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
        //todo set room
    }

    private void CreateStartTile(int x, int z, string player, GameObject parent)
    {
        string name;
        CameraTarget playerCameraTarget;
        Character character = Character.MissScarlett;
        switch (player)
        {
            case "missScarlett":
                name = "Miss Scarlett";
                playerCameraTarget = CameraTarget.MissScarlett;
                character = Character.MissScarlett;
                break;
            case "profPlum":
                name = "Prof Plum";
                playerCameraTarget = CameraTarget.ProfPlum;
                character = Character.ProfPlum;
                break;
            case "colMustard":
                name = "Col Mustard";
                playerCameraTarget = CameraTarget.ColMustard;
                character = Character.ColMustard;
                break;
            case "mrsPeacock":
                name = "Mrs Peacock";
                playerCameraTarget = CameraTarget.MrsPeacock;
                character = Character.MrsPeacock;
                break;
            case "revGreen":
                name = "Rev Green";
                playerCameraTarget = CameraTarget.RevGreen;
                character = Character.RevGreen;
                break;
            case "mrsWhite":
                name = "Mrs White";
                playerCameraTarget = CameraTarget.MrsWhite;
                character = Character.MrsWhite;
                break;
            default:
                name = "";
                playerCameraTarget = CameraTarget.Initial;
                break;
        }
        GameObject startingTile = GameObject.Instantiate(startingTilePrefab, new Vector3(x, 0, z), transform.rotation, startTiles.transform);
        startingTile.name = name + " - Start Space";
        GameObject playerToken = GameObject.Instantiate(playerPiecePrefab, new Vector3(x, 0, z), transform.rotation, players.transform);
        playerToken.name = name + " - Player Token";
        StartTileScript tilescript = startingTile.GetComponent<StartTileScript>();
        tilescript.SetCharacter(character, name);
        tilescript.GridPosition = new Vector2(x, z);
        playerToken.GetComponentInChildren<PlayerTokenScript>().SetCharacter(character,tilescript);
        playerToken.GetComponentInChildren<CloseUpPointScript>().SetCloseUpPointName(playerCameraTarget);
        /*
         * ----To do---- 
         * set tile colour from playerPiece.GetComponent<PlayerTokenScript>().GetCharacterColour());
         */
    }

    private void CreateBoardTile(int x, int z, GameObject parent)
    {
        GameObject boardTile = GameObject.Instantiate(generalTilePrefab, new Vector3(x, 0, z), transform.rotation, parent.transform);
        boardTile.name = "Board Tile - ( "+ x + " : " + z + " )";
        boardTile.GetComponent<BoardTileScript>().GridPosition = new Vector2(x, z);
    }
    
    private void CreateRoom(int x, int z, string roomName)
    {
        GameObject room = null;
        switch (roomName)
        {
            case "Study":
                room = GameObject.Instantiate(StudyPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Study;
                break;
            case "Hall":
                room = GameObject.Instantiate(HallPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Hall;
                break;
            case "Lounge":
                room = GameObject.Instantiate(LoungePrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Lounge;
                break;
            case "Library":
                room = GameObject.Instantiate(LibraryPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Library;
                break;
            case "Centre":
                room = GameObject.Instantiate(CentrePrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Centre;
                break;
            case "Dining":
                room = GameObject.Instantiate(DiningRoomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.DiningRoom;
                break;
            case "Billiard":
                room = GameObject.Instantiate(BilliardRoomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.BilliardRoom;
                break;
            case "Conservatory":
                room = GameObject.Instantiate(ConservatoryPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Conservatory;
                break;
            case "Ballroom":
                room = GameObject.Instantiate(BallroomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Ballroom;
                break;
            case "Kitchen":
                room = GameObject.Instantiate(KitchenPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                room.GetComponent<RoomScript>().Room = Room.Kitchen;
                break;
            default:
                break;
        }
        room.transform.parent = rooms.transform;
    }

}
