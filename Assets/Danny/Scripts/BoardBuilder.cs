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
    [SerializeField] Color missPeacockColour;
    [SerializeField] Color revGreenColour;
    [SerializeField] Color mrsWhiteColour;
    [Space]
    [SerializeField] Color generalTileColour;
    [Space]
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
        boardArray = GetComponent<BoardArrayGenerator>().GetBoardArray();
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
        shortcutTile.GetComponent<Renderer>().material.SetColor("_MainColour", generalTileColour);
        //todo set room
    }

    private void CreateRoom(int x, int z, string roomName)
    {
        switch (roomName)
        {
            case "Study":
                GameObject room = GameObject.Instantiate(StudyPrefab, new Vector3(x, 0, z), transform.rotation, transform);
                break;
            default:
                break;
        }
    }

    private void CreateRoomEntranceTile(int x, int z, string Room, string rotation)
    {
        int arrowRotation = int.Parse(rotation);
        GameObject roomEntryTile = GameObject.Instantiate(roomEntryTilePrefab, new Vector3(x, 0, z), Quaternion.Euler(0,arrowRotation,0) , transform);
        roomEntryTile.GetComponent<Renderer>().material.SetColor("_MainColour", generalTileColour);
        //todo set room
    }

    private void CreateStartTile(int x, int z, string player)
    {
        
        Color tileColour;
        switch (player)
        {
            case "missScarlett":
                tileColour = missScarlettColour;
                break;
            case "profPlum":
                tileColour = profPlumColour;
                break;
            case "colMustard":
                tileColour = colMustardColour;
                break;
            case "missPeacock":
                tileColour = missPeacockColour;
                break;
            case "revGreen":
                tileColour = revGreenColour;
                break;
            case "mrsWhite":
                tileColour = mrsWhiteColour;
                break;
            default:
                tileColour = Color.white;
                break;
        }
        GameObject startingTile = GameObject.Instantiate(startingTilePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        startingTile.GetComponent<Renderer>().material.SetColor("_MainColour", tileColour);
    }

    private void CreateBoardTile(int x, int z)
    {
        GameObject boardTile = GameObject.Instantiate(generalTilePrefab, new Vector3(x, 0, z), transform.rotation, transform);
        boardTile.GetComponent<Renderer>().material.SetColor("_MainColour", generalTileColour);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
