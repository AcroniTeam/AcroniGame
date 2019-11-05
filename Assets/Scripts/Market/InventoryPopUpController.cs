using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUpController : MonoBehaviour
{
    private static InventoryPopUpController instance;

    public Button openButton;
    public SwitchController popUp1item;
    public SwitchController popUp2item;

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
        if (disponibleSlots[1])
        {
            if (!popUp2item.IsOpen())
                popUp2item.Open();
            else
                popUp2item.Close();
        }else if (disponibleSlots[0])
        {
            if (!popUp1item.IsOpen())
                popUp1item.Open();
            else
                popUp1item.Close();
        }
    }

    public void Block()
    {
        openButton.gameObject.SetActive(false);
    }

    bool[] disponibleSlots = new bool[] { false,false };
    public void Release(int indicator)
    {
        disponibleSlots[indicator] = true;
        openButton.gameObject.SetActive(true);
    }

    public void BlockInventory(int indicator)
    {
        disponibleSlots[indicator] = false;
    }
}
