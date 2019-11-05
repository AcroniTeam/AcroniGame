using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public Animator bombExplosion;
    
    public bool waitResponse = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!waitResponse)
            bombExplosion.SetBool("Touched", true);
    }

    public void Release()
    {
        waitResponse = false;
        bombExplosion.SetBool("Touched", true);
    }
}
