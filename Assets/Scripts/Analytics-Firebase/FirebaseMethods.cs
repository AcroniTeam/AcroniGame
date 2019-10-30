using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class FirebaseMethods : MonoBehaviour
{
    public static FirebaseMethods firebaseMethods = new FirebaseMethods();
    public FirebaseMethods getFireBaseMethodsInstance()
    {
        return firebaseMethods;
    }
    DatabaseReference database;
    public void InitializeFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://analytics-7777.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void IncrementQttItems(string item)
    {
        IncrementQttPlayed(item, "itens");
    }
    public void IncrementQttPlayed(string fase,string path = "fases")
    {
        List<object> valorAtual;
        database.Child("sample").Child("-LrQ39Kn5629t6fgDYpA").Child("game").Child(path).RunTransaction((mutableData => {
            Dictionary<string, object> translater = new Dictionary<string, object>();
            if (mutableData.Value == null)
            {
                valorAtual = new List<object>();
                translater[fase] = 1;
            }
            else
            {
                valorAtual = mutableData.Value as List<object>;
                int atualValue = System.Convert.ToInt32(mutableData.Child(fase).Value.ToString()); //não funciona se não for assim, sério, tentei 943842 vezes e gastei 434234 minutos por causa disso
                translater[fase] = atualValue + 1;               
            }
            mutableData.Value = translater;
            return TransactionResult.Success(mutableData);
        }));
        //database.Child("sample").Child("-LrQ39Kn5629t6fgDYpA").Child("game").Child("fases").Child("Fase Aérea").SetValueAsync();
    }
}
