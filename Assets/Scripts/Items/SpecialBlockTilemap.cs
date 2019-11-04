using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpecialBlockTilemap : MonoBehaviour
{
    private static SpecialBlockTilemap instance;
    Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        tilemap = GetComponentInParent<Tilemap>();
    }

    public static SpecialBlockTilemap GetSpecialBlockTilemap()
    {
        return instance;
    }

    public Tilemap GetTilemap()
    {
        return tilemap;
    }

}
