using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BoardArrayGenerator : MonoBehaviour
{
    /*
    private string[][] boardStringArray;

    private void Awake()
    {
        GenerateBoardArrayFromCSV();
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
        
    }

    public string[][] GetBoardArray()
    {
        return boardStringArray;
    }
*/
}
