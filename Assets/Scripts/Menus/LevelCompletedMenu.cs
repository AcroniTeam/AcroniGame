using TMPro;
using UnityEngine;

public class LevelCompletedMenu : MonoBehaviour
{
    static LevelCompletedMenu instance;

    public TextMeshProUGUI life_text;
    public TextMeshProUGUI money_text;

    Animator anim;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public static LevelCompletedMenu GetInstance()
    {
        return instance;
    }

    public void OnClick()
    {
        GameManager.GetInstance().BuildNextScene();
    }

    public void SetMoneyText(int m_text)
    {
        money_text.text = m_text.ToString();
    }

    public void SetLifeText(int l_text)
    {
        life_text.text = l_text.ToString();
    }

    public void Open()
    {
        anim.SetTrigger("CanAnimate");
        MenuController.GetInstance().StopAllButtons();
        ChangeColorScript.getInstance().Animate("black");
        anim.SetBool("isOpen", true);
    }
}
