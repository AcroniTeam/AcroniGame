using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUpController : MonoBehaviour
{
    private static InventoryPopUpController instance;

    public Button openButton;
    public SwitchController popUp;

    private void Start()
    {
        instance = this;
    }

    public static InventoryPopUpController GetPopUpController()
    {
        return instance;
    }

    public void OpenOrClose()
    {

    }

    public void Block()
    {
        popUp.Disable();
        openButton.enabled = false;
    }

    public void Release()
    {
        Debug.Log("Liberei aqui");
        popUp.Enable();
        openButton.enabled = true;
    }
}
