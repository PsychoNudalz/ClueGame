using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField] GameObject boardTile;
    [SerializeField] GameObject startTile;
    [SerializeField] GameObject roomEntryTile;
    [SerializeField] GameObject roomFloorTile;
    [SerializeField] GameObject shortcutTile;

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
                        CreateRoomEntranceTile(x, z, square[1]);
                        break;
                    case "R":
                        CreateRoomTile(x, z, square[1]);
                        break;
                    case "SC":
                        CreateShortcutTile(x, z, square[1]);
                        break;
                    default:
                        
                        break;
                }
            }
        }

    }

    private void CreateShortcutTile(int x, int z, string v)
    {
        GameObject startingTile = GameObject.Instantiate(shortcutTile, new Vector3(x, 0, z), transform.rotation, transform);
        //todo set room
    }

    private void CreateRoomTile(int x, int z, string v)
    {
        GameObject startingTile = GameObject.Instantiate(roomFloorTile, new Vector3(x, 0, z), transform.rotation, transform);
        //todo set room
    }

    private void CreateRoomEntranceTile(int x, int z, string Room)
    {
        GameObject startingTile = GameObject.Instantiate(roomEntryTile, new Vector3(x, 0, z), transform.rotation, transform);
        //todo set room
    }

    private void CreateStartTile(int x, int z, string Colour)
    {
        
        Color tileColour;
        switch (Colour)
        {
            case "Red":
                tileColour = Color.red;
                break;
            case "Yellow":
                tileColour = Color.yellow;
                break;
            case "Purple":
                tileColour = Color.magenta;
                break;
            case "White":
                tileColour = Color.white;
                break;
            case "Green":
                tileColour = Color.green;
                break;
            case "Blue":
                tileColour = Color.blue;
                break;
            default:
                tileColour = Color.white;
                break;
        }
        GameObject startingTile = GameObject.Instantiate(startTile, new Vector3(x, 0, z), transform.rotation, transform);
        startingTile.GetComponent<Renderer>().material.SetColor("_MainColour", tileColour);
    }

    private void CreateBoardTile(int x, int z)
    {
        GameObject.Instantiate(boardTile, new Vector3(x, 0, z), transform.rotation, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
