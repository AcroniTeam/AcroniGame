using UnityEngine;

public class ChangeColorScript : MonoBehaviour
{
    static ChangeColorScript instance;

    Animator anim;

    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
        anim.SetBool("red",false);
        anim.SetBool("black",false);
    }

    public static ChangeColorScript getInstance()
    {
        return instance;
    }

    public void Animate(string color)
    {
        if(color.Equals("red"))
        {
            anim.SetBool("black", false);
            anim.SetBool("red", true);
        }
        else
        {
            anim.SetBool("red",false);
            anim.SetBool("black", anim.GetBool("black")?false:true);
        }
        anim.SetTrigger("CanAnimate");
    }
}