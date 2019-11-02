using UnityEngine;

public class OnStartScene : MonoBehaviour
{
    public string MusicName;
    public SceneType SceneType;

    public static SceneType sceneType;

    void Start()
    {
        sceneType = SceneType;
        if (GameManager.GetInstance() == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu_Principal");
            return;
        }

        if(MusicName == null)
            Debug.LogError("Faltando o nome da música na cena!");
        else
            AudioManager.GetInstance().Play(MusicName);
    }
}

public enum SceneType
{
    MEMORY_PUZZLE, ITEM_PUZZLE
}
