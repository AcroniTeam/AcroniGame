using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase.Auth;
using System.Threading.Tasks;

public class FirebaseMethods : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser currentUser;
    public static FirebaseMethods firebaseMethods = new FirebaseMethods();
    public FirebaseMethods()
    {
        auth = FirebaseAuth.DefaultInstance;
    }
    public void Logout()
    {
        
    }
    public FirebaseMethods getFireBaseMethodsInstance()
    {
        return firebaseMethods;
    }
    public FirebaseUser getFirebaseUser()
    {
        firebaseMethods.InitializeFirebase();
        return currentUser;
    }
    DatabaseReference database;
    public void InitializeFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://analytics-7777.firebaseio.com/");
        database = FirebaseDatabase.DefaultInstance.RootReference;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    public void AttDiscount(double desconto)
    {
        firebaseMethods.InitializeFirebase();
        database.Child("sample").Child("-LrQ39Kn5629t6fgDYpA").Child("game").Child("descontos").Child(currentUser.Email.Replace(".", ",")).Child("qtdDesconto").SetValueAsync(desconto);
    }
    private void AttUser(Task<FirebaseUser> taskbob,string signed_or_created)
    {
        Debug.LogFormat("User {0} in successfully: {1} ({2})",
            signed_or_created, taskbob.Result.DisplayName, taskbob.Result.Email);
    }
    public bool SignUp(string email, string senha)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, senha).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return false;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return false;
            }
            AttUser(task, "created");
            Dictionary<string, object> translater = new Dictionary<string, object>();
            translater["qtdDesconto"] = 0.03;
            translater["wasUsed"] = false;
            database.Child("sample").Child("-LrQ39Kn5629t6fgDYpA").Child("game").Child("descontos").Child(email.Replace(".", ",")).UpdateChildrenAsync(translater);
            //não pode '.' em nome de node no firebase, ai vai ter q substituir no ASP para achar por ,
            return Login(email, senha);

        });
        return Login(email, senha);
    }

    public bool Login(string email,string senha)
    {
        auth.SignInWithEmailAndPasswordAsync(email, senha).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return false;
            }
            if (task.IsFaulted)
            {
                Debug.Log("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return false;
            }
            AttUser(task, "signed");
            return true;
        });
        return true;
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
    

// Track state changes of the auth object.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
    if (auth.CurrentUser != currentUser)
        {
            bool signedIn = currentUser != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && currentUser != null)
            {
              Debug.Log("Signed out " + currentUser.UserId);
            }
            currentUser = auth.CurrentUser;
            if (signedIn)
            {
            Debug.Log("Signed in " + currentUser.UserId);
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

}
