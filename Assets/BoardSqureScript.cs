using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSqureScript : MonoBehaviour
{
    [SerializeField] Vector2 gridPosition;
    // Start is called before the first frame update
    void Start()
    {
        gridPosition = new Vector2(transform.position.x, transform.position.z);
    }

    
    Vector2 GetSquarePos()
    {
        return gridPosition;
    }
}
