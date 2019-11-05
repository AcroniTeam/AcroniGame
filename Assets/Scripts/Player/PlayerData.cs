using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int scenePlayerStopped;
    public int money;
    public int discount;

    public PlayerData(Player player)
    {
        scenePlayerStopped = GameManager.GetInstance().getCurrentSceneIndex();
        money = player.GetPlayerCurrency();
        discount = GameManager.GetInstance().EvaluateDiscount();
    }
}
