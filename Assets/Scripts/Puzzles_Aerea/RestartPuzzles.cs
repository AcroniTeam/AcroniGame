using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RestartPuzzles : MonoBehaviour
{
    // Start is called before the first frame update
    public Tile tile;
    public Tilemap tilemap;
    void OnTriggerEnter2D(Collider2D collider)
    {
        BombRain.setCanDo(true);
        tilemap.SetTile(new Vector3Int(63, -4, 0), null);
        tilemap.SetTile(new Vector3Int(67, -3, 0), null);
        tilemap.SetTile(new Vector3Int(61, -2, 0), tile);
    }

    // Update is called once per frame

}
