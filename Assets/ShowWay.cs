using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShowWay : MonoBehaviour
{
    public Tile tile;
    public Tilemap tileMap;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        tileMap.SetTile(new Vector3Int(18, -8, 0), tile);
        tileMap.SetTile(new Vector3Int(23, -7, 0), tile);
        tileMap.SetTile(new Vector3Int(27, -6, 0), tile);
        tileMap.SetTile(new Vector3Int(28, -4, 0), tile);
    }
}
