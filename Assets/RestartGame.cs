using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        GameManager.GetInstance().RestartGame();
    }
}
