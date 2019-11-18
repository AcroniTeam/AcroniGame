using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int coinValue = 1;
    bool HasTaken;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (HasTaken)
            return;

        HasTaken = true;

        if (other.gameObject.CompareTag("Player"))
        {
      
            int current = Player.getInstance().AddPlayerCurrency(coinValue);
            Debug.Log(current);
            Store.GetInstance().SetCurrencyDisplay(current);
            Destroy(gameObject);
        }
    }
}
