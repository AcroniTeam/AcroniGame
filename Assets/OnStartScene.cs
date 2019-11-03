using TMPro;
using UnityEngine;

public class OnStartScene : MonoBehaviour
{
    public string MusicName;
    public TMP_Text welcome_username;
    void Start()
    {
        if(MusicName == null)
            Debug.LogError("Faltando o nome da música na cena!");
        else
            AudioManager.GetInstance().Play(MusicName);
        if (FirebaseMethods.firebaseMethods.getFirebaseUser().Email != string.Empty)
            welcome_username.text = "Welcome, " + FirebaseMethods.firebaseMethods.getFirebaseUser().Email;
        //provisório tbm
    }
}
