using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRain : MonoBehaviour
{
    public static void setCanDo(bool cando)
    {
        canDo = cando;
    }
    public GameObject pinha;
    // Start is called before the first frame update

    private static bool canDo = true;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(canDo)
        if (collider.gameObject.tag.Equals("Player"))
        {
                canDo = false;
                for (int i = 50;i<54;i++)
            Instantiate(pinha, new Vector3(i, 6, 0), Quaternion.identity);
        }
    }
    // Update is called once per frame

}
