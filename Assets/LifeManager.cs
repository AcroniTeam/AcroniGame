using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public Sprite[] images;

    public void SetHearts(bool[] lifes)
    {
        int i;
        for (i = 2; i >= 0; i--)
        {
            if (lifes[i])
                break;
        }
        
        GetComponent<Image>().sprite = images[i];
    }
}
