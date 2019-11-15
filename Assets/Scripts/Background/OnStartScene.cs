using TMPro;
using UnityEngine;

public class OnStartScene : MonoBehaviour
{
    public string MusicName;
    public TMP_Text welcome_username;
    public SceneType SceneType;
    public GameObject canvas;

    public static SceneType sceneType;
    private void Awake()
    {
        
    }
    void Start()
    {
        sceneType = SceneType;
        if (GameManager.GetInstance() == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("loading_scene");
            return;
        }
        if (CountdownTimer.getInstance() != null)
            CountdownTimer.getInstance().StartTimer();

        Time.timeScale = 1;
        AudioManager.GetInstance().AcelerateSfx();

        if(canvas!= null)
            canvas.GetComponent<Canvas>().enabled = true;

        if (MusicName == null)
            Debug.LogError("Faltando o nome da música na cena!");
        else if (!MusicName.Equals(AudioManager.GetInstance().GetCurrentBGM()))
        {
            AudioManager.GetInstance().Stop(AudioManager.GetInstance().GetCurrentBGM());
            AudioManager.GetInstance().Play(MusicName);
        }
            

        try
        {
            InventoryPopUpController.GetPopUpController().Block();
        }
        catch { }     
    }

    bool stop = false;
    private void Update()
    {
        if (stop)
            return;
        if (FirebaseMethods.firebaseMethods.getFirebaseUser().Email != string.Empty)
        {
            try
            {
                welcome_username.text = "Welcome, " + FirebaseMethods.firebaseMethods.getFirebaseUser().Email.Split('@')[0];
            }
            catch (System.Exception) { }
            stop = true;
            IOManager.SaveProgress();
            FirebaseMethods.firebaseMethods.AttDiscount(GameManager.GetInstance().EvaluateDiscount());
        }
    }
}


public enum SceneType
{
    MEMORY_PUZZLE, ITEM_PUZZLE
}
