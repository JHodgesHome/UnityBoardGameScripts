using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;
    void OnMouseUp()
    {
        Debug.Log("Tile Coords: " + tileX + ", " + tileY);

        map.GeneratePathTo(tileX, tileY);
    }
}
