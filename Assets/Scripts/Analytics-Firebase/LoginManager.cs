using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField senha;
    bool fez;
    public void Enter()
    {
        if (Regex.IsMatch(login.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            fez = FirebaseMethods.firebaseMethods.Login(login.text,senha.text);
        if (fez)
        {
            Debug.Log("fez né vei");
            SceneManager.LoadScene("Menu_principal");
        }
    }
    public void Create()
    {
        FirebaseMethods.firebaseMethods.InitializeFirebase();
        if (Regex.IsMatch(login.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            fez = FirebaseMethods.firebaseMethods.SignUp(login.text, senha.text);
        if (fez)
        {
            Debug.Log("fez né vei");
            SceneManager.LoadScene("Menu_principal");
        }
    }
    public void AttDiscount() //provisório,teste
    {
        FirebaseMethods.firebaseMethods.AttDiscount(0.05);
    }
    public void Exit()
    {
        SceneManager.LoadScene("Login");
    }
}
