using UnityEngine;
using UnityEngine.UI;

public class InventoryPopUpController : MonoBehaviour
{
    private static InventoryPopUpController instance;

    public Button openButton;
    public PopUpInventory popUp1item;
    public PopUpInventory popUp2item;
    public PopUpInventory pupUp3item;

    private void Start()
    {
        instance = this;
        //openButton.gameObject.SetActive(false);
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

    public bool IsOpen()
    {
        return (popUp2item.IsOpen() || popUp1item.IsOpen());
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
        if (indicator == 0)
            openButton.gameObject.SetActive(false);
    }

    public void SwitchInventories()
    {
        if (popUp2item.IsOpen())
        {
            popUp2item.Close();
            popUp1item.Open();
        }
        else if (popUp1item.IsOpen())
        {
            popUp1item.Close();
        }
    }
}
