using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemFactory : MonoBehaviour
{

    //ITEMS    
    public GameObject bomba;
    public GameObject trampoline;
    public GameObject timeDecelerator;
    public Sprite specialBlock;

    public static ItemFactory factory;

    private void Start()
    {
        factory = this;
        DontDestroyOnLoad(factory);
    }

    public static ItemFactory GetFactory()
    {
        return factory;
    }

    public GameObject ProduceItem(string name)
    {
        GameObject returnGO = new GameObject();
        switch(name)
        {
            case "Bomba": returnGO = bomba;
                break;
            case "Trampolim": returnGO = trampoline;
                break;
            case "Controlador Temporal": returnGO = timeDecelerator;
                break;
        }
        return returnGO;
    }

    public Tile ProduceSpecialBlock()
    {
        Tile returnTile = ScriptableObject.CreateInstance<Tile>();
        returnTile.sprite = specialBlock;
        return returnTile;
    }
}
