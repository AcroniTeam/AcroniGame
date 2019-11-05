using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int scenePlayerStopped = 1;
    public int money;
    public int discount = 3;

    public PlayerData(Player player)
    {
        scenePlayerStopped = GameManager.GetInstance().getCurrentSceneIndex();
        discount = GameManager.GetInstance().EvaluateDiscount();
    }

    public PlayerData()
    {
        scenePlayerStopped = 1;
        money = 0;
        discount = 0;
    }
}
