using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    static InventoryController inventoryController;
    public SlotController main_slot;
    public InventorySlot[] slots;

    public void AddItem(InventoryItem item)
    {        
        foreach(InventorySlot slot in slots)
        {
            if(slot.Equals(item) || slot.IsEmpty())
            {
                slot.Fill(item);
                if (main_slot.IsEmpty() || main_slot.Equals(item))
                {
                    slot.SetSelected(true);
                    main_slot.Fill(item);
                }
                break;
            }
        }
    }

    public void UpdateUI()
    {
        foreach(InventorySlot ins in slots)
        {
            if (ins.GetQuantity() > 0)
            {
                if (ins.Equals(main_slot.item_reference))
                {
                    ins.DecreaseQuantity();
                    if (ins.GetQuantity() == 0)
                        ins.Clear();
                    break;
                }
            }
        }

        if(main_slot.GetQuantity() == 0 || main_slot.IsEmpty())
        {
            main_slot.Clear();
            int i;
            for(i = 0; i < 3; i++)
            {
                if(slots[i].GetQuantity() == 0)
                {
                    if(slots[i+1].GetQuantity() > 0)
                    {
                        slots[i].Fill(slots[i + 1].item_reference);
                        slots[i + 1].Clear();
                        slots[i].SetSelected(true);
                    }else if(i != 0 && slots[i-1].GetQuantity() > 0)
                    {
                        slots[i - 1].SetSelected(true);
                        slots[i - 1].Fill(slots[i].item_reference);
                        slots[i].Clear();
                        slots[i].SetSelected(false);
                    } 
                }
            }

            main_slot.Fill(System.Array.Find(slots, slot => slot.isSelected()).item_reference);
        }

        if (slots[0].GetQuantity() <= 0)
        {
            slots[0].SetSelected(false);
            main_slot.Clear();
        }
            
        foreach(InventorySlot s in slots)
        {
            if(s.GetQuantity() == 0 || s.IsEmpty())
            {
                s.Clear();
                s.SetSelected(false);
            }
        }
    }

    public void SetSelected(InventoryItem item)
    {
        foreach(InventorySlot invSlot in slots)
        {
            if (invSlot.item_reference == null)
                continue;

            if (invSlot.item_reference.Equals(item))
                invSlot.SetSelected(true);
            else
                invSlot.SetSelected(false);
        }

        main_slot.Fill(item);
    }

    public static InventoryController GetInventoryController()
    {
        return inventoryController;
    }

    public BoxCollider2D GetCollider()
    {
        return GetComponent<BoxCollider2D>();
    }

    ////vector3 lastposition = new vector3(0,0,0);
    ////private void update()
    ////{
    ////    if (!interactible)
    ////        return;

    ////    if (input.touchcount == 0)
    ////        return;

    ////    vector3 inputposition = camera.main.screentoworldpoint(input.gettouch(0).position);
    ////    if (getcomponent<boxcollider2d>().overlappoint(inputposition))
    ////    {
    ////        //debug.log(lastposition.x > inputposition.x);

    ////        if (input.gettouch(0).phase.equals(touchphase.moved))
    ////            lastposition = inputposition;
    ////    }
    ////}

    bool interactible;
    public void SetInteractible(bool interacbility)
    {
        interactible = interacbility;
        main_slot.SetEnabled(interacbility);
    }

    void Start()
    {
        inventoryController = this;
    }
}
