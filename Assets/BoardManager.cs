using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public BoardTileScript[][] boardTileArray { get => boardTileArray; set => boardTileArray = value; }
    // Start is called before the first frame update
    void Start()
    {
        boardTileArray = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
