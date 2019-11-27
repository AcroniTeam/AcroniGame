using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IfTouchSeFerraste : MonoBehaviour
{
    public Tile tile;
    public Tilemap tilemap;
    // Start is called before the first frame update

    IEnumerator Wait(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            Vector3Int pos = new Vector3Int(59, -2, 0);
            switch (gameObject.name)
            {
                case "ifTouchSeFerraste":
                    pos = new Vector3Int(61, -2, 0);
                    break;
                case "ifTouchSeFerraste (1)":
                    pos = new Vector3Int(63, -4, 0);
                    break;
                case "ifTouchSeFerraste (2)":
                    pos = new Vector3Int(67, -3, 0);
                    break;

            }
            yield return new WaitForSeconds((float)0.3);
            tilemap.SetTile(pos, null);
            yield return new WaitForSeconds((float)0.3);
            switch (gameObject.name)
            {
                case "ifTouchSeFerraste":
                    pos = new Vector3Int(63, -4, 0);
                    break;
                case "ifTouchSeFerraste (1)":
                    pos = new Vector3Int(67, -3, 0);
                    break;

            }
            if (!tilemap.HasTile(pos))
                if (!gameObject.name.Contains("2"))
                tilemap.SetTile(pos, tile);
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        StartCoroutine(Wait(collider));

    }
    // Update is called once per frame

}
