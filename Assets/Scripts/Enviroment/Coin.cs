using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int coinValue = 1;
    bool HasTaken = false;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (HasTaken)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            HasTaken = true;
            int current = Player.getInstance().AddPlayerCurrency(coinValue);
            Store.GetInstance().SetCurrencyDisplay(current);
            LevelCompletedMenu.GetInstance().SetMoneyText(current);
            AudioManager.GetInstance().Play("sfx-coin");
            Destroy(gameObject);
        }
    }
}
