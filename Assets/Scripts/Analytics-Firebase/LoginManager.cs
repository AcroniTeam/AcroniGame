using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField senha;
    public string scene = "";
    bool fez;
    void Update()
    {
        if (FirebaseMethods.firebaseMethods.isLogged && scene.Equals("Login"))
        {
            Debug.Log("hasjkfhdsa");
            SceneManager.LoadScene("Menu_principal");
        }
    }
    public void EnterAsGuest()
    {
        SceneManager.LoadScene("Menu_principal");
    }
    public void Enter()
    {
        if (Regex.IsMatch(login.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            fez = FirebaseMethods.firebaseMethods.Login(login.text, senha.text);
    }
    public void Create()
    {
        FirebaseMethods.firebaseMethods.InitializeFirebase();
        if (Regex.IsMatch(login.text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            fez = FirebaseMethods.firebaseMethods.SignUp(login.text, senha.text);
    }
    public void AttDiscount() //provisório,teste
    {
        FirebaseMethods.firebaseMethods.AttDiscount(0.05);
    }
    public void Exit()
    {
        FirebaseMethods.firebaseMethods = new FirebaseMethods();
        SceneManager.LoadScene("Login");
    }
}