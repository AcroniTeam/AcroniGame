using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawn : MonoBehaviour
{
    private bool isSetted = false;

    public bool isFinalRespawn = false;
    public CameraBoundsManager boundsManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Ada") && !isSetted)
        {
            
            try
            {
                boundsManager.NextBound();
            }
            catch { }
            isSetted = true;

            Player.getInstance().NextSpawn();
            if (isFinalRespawn)
                GameManager.GetInstance().EndLevel();
        } 
    }
}
