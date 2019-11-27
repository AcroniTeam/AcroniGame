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
        if (collider2D.gameObject.tag.Equals("Player"))
        {
            tileMap.SetTile(new Vector3Int(25, -6, 0), tile);
            tileMap.SetTile(new Vector3Int(27, -4, 0), tile);
        }
    }
}
