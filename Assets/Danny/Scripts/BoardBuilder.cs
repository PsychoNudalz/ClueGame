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
    [SerializeField] GameObject BallRoomPrefab;
    [SerializeField] GameObject KitchenPrefab;


    private string[][] boardArray;

    // Start is called before the first frame update
    void Start()
    {
        boardArray = transform.parent.GetComponent<BoardArrayGenerator>().GetBoardArray();
        BuildBoard();
    }

    void BuildBoard()
    {
        int x = -1;
        int z = -1;

        for(int row = boardArray.Length -1; row >= 0; row--)
        {
            z++;
            x = -1;
            for(int col = 0; col < boardArray[row].Length; col++)
            {
                x++;
                string[] square = boardArray[row][col].Split(':');
                switch (square[0])
                {
                    case "X":
                        CreateBoardTile(x, z);
                        break;
                    case "S":
                        CreateStartTile(x, z, square[1]);
                        break;
                    case "E":
                        CreateRoomEntranceTile(x, z, square[1], square[2]);
                        break;
                    case "R":
                        CreateRoom(x, z, square[1]);
                        break;
                    case "SC":
                        CreateShortcutTile(x, z, square[1], square[2]);
                        break;
                    default:
                        
                        break;
                }
            }
        }

    }

    private void CreateShortcutTile(int x, int z, string room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject shortcutTile = GameObject.Instantiate(this.shortcutTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , transform);
        shortcutTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        //todo set room
    }

    private void CreateRoom(int x, int z, string roomName)
    {
        GameObject room;
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
                room = GameObject.Instantiate(BallRoomPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            case "Kitchen":
                room = GameObject.Instantiate(KitchenPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            default:
                break;
        }
    }

    private void CreateRoomEntranceTile(int x, int z, string Room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject roomEntryTile = GameObject.Instantiate(roomEntryTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , transform);
        roomEntryTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
        //todo set room
    }

    private void CreateStartTile(int x, int z, string player)
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
        GameObject playerPiece = GameObject.Instantiate(playerPiecePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        playerPiece.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", playerColour);
    }

    private void CreateBoardTile(int x, int z)
    {
        GameObject boardTile = GameObject.Instantiate(generalTilePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        boardTile.GetComponentInChildren<Renderer>().material.SetColor("_MainColour", generalTileColour);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
