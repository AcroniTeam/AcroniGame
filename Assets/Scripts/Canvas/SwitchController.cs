using UnityEngine;

public class SwitchController : MonoBehaviour
{
    Animator inventory;

    bool isOpen = false;

    private void Start()
    {
        inventory = GetComponent<Animator>();
    }

    public void Open()
    {
        inventory.SetBool("isOpen", true);
        inventory.SetTrigger("Slide");
        isOpen = true;
    }

    public void Close()
    {
        inventory.SetBool("isOpen", false);
        inventory.SetTrigger("Slide");
        isOpen = false;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
