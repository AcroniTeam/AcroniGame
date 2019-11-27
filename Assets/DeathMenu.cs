using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    static DeathMenu instance;

    Animator anim;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public static DeathMenu GetInstance()
    {
        return instance;
    }

    public void MainMenu()
    {
        GameManager.GetInstance().BuildScene(1);
    }

    public void Retry()
    {
        GameManager.GetInstance().RebuildCurrentScene();
    }

    public void Open()
    {
        anim.SetTrigger("CanAnimate");
        ChangeColorScript.getInstance().Animate("red");
        anim.SetBool("isOpen",true);
        MenuController.GetInstance().StopAllButtons();
    }
}
