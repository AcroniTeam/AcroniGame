using TMPro;
using UnityEngine;

public class OnStartScene : MonoBehaviour
{
    public string MusicName;
    public TMP_Text welcome_username;
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

        IOManager.SaveProgress();
        FirebaseMethods.firebaseMethods.AttDiscount(GameManager.GetInstance().EvaluateDiscount());

        if (MusicName == null)
            Debug.LogError("Faltando o nome da música na cena!");
        else
            AudioManager.GetInstance().Play(MusicName);

        try
        {
            InventoryPopUpController.GetPopUpController().Block();
        }
        catch { }

        try
        {
            if (FirebaseMethods.firebaseMethods.getFirebaseUser().Email != string.Empty)
                welcome_username.text = "Welcome, " + FirebaseMethods.firebaseMethods.getFirebaseUser().Email;
        }
        catch { }
        
        
        //provisório tbm
    }
}

public enum SceneType
{
    MEMORY_PUZZLE, ITEM_PUZZLE
}
