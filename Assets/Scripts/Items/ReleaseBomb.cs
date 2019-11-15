using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseBomb : MonoBehaviour
{
    public GameObject bomb_reference;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bomb_reference != null)
            bomb_reference.GetComponent<Bomba>().Release();
    }
}
