using UnityEngine;

public class PopUpInventory : MonoBehaviour
{
    Animator inventory;

    public SlotController[] slots;

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

    public SlotController GetSlot(int index)
    {
        return slots[index];
    }

    public void FillSlots(InventoryItem item)
    {
        if (slots.Length == 0)
            return;

        foreach (SlotController slt in slots)
        {
            if (slt.IsEmpty() || slt.Equals(item))
            {
                slt.Fill(item);
                return;
            }
        }
    }
}
