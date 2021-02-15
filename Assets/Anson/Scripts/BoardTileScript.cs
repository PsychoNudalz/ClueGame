using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTileScript : MonoBehaviour
{
    [SerializeField] Vector2 gridPosition;

    public Vector2 GridPosition { get => gridPosition; set => gridPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        //gridPosition = new Vector2(transform.position.x, transform.position.z);
    }

    
    Vector2 GetSquarePos()
    {
        return gridPosition;
    }

    public virtual void ClearTile()
    {
        print(this + " cleared");
    }
    public virtual void SelectTile()
    {
        print(this + " select");
    }
}
