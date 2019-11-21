using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryController : MonoBehaviour
{
    static InventoryController inventoryController;

    public SlotController visibleSlot;
    public SlotController[] inventorySlots;

    int selectedSlot = 0;

    void Start()
    {
        inventoryController = this;
    }

    public void AddItem(InventoryItem item)
    {
        foreach(SlotController s in inventorySlots)
        {
            if(s.IsEmpty() || s.Equals(item))
            {
                s.Fill(item);
                break;
            }
        }

        if (visibleSlot.IsEmpty() || visibleSlot.Equals(item))
            visibleSlot.Fill(item);
    }

    public void UpdateUI()
    {
        //for (int i = 0; i < inventorySlots.Length; i++)
        //{
        //    if (inventorySlots[i].Equals(visibleSlot.Item_reference))
        //    {
        //        inventorySlots[i].DecreseQuantityFromSlot();
        //        break;
        //    }
        //}

        //if (visibleSlot.GetQuantity() > 0)
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (inventorySlots[i].GetQuantity() == 0)
        //        {
        //            if(!inventorySlots[i].IsEmpty())
        //            {
        //                inventorySlots[i].Fill(inventorySlots[i + 1].Item_reference);
        //                inventorySlots[i].SetSelected(inventorySlots[i + 1].IsSelected());
        //                inventorySlots[i + 1].Clear();
        //            }else
        //            {
        //                inventorySlots[i].Clear();
        //                inventorySlots[i + 1].Clear();
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (inventorySlots[i].GetQuantity() == 0)
        //        {
        //            if (!inventorySlots[i].IsEmpty())
        //            {
        //                inventorySlots[i].Fill(inventorySlots[i + 1].Item_reference);
        //                inventorySlots[i].SetSelected(inventorySlots[i + 1].IsSelected());
        //                inventorySlots[i + 1].Clear();
        //            }
        //            else
        //            {
        //                inventorySlots[i].Clear();
        //                inventorySlots[i + 1].Clear();
        //            }
        //        }
        //    }
        //    if (inventorySlots[0].GetQuantity() > 0)
        //    {
        //        visibleSlot.Fill(inventorySlots[0].Item_reference);
        //        inventorySlots[0].SetSelected(true);
        //    }
        //}

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Equals(visibleSlot.Item_reference) && i != selectedSlot)
            {
                inventorySlots[i].DecreseQuantityFromSlot();
                break;
            }
        }

        if (visibleSlot.GetQuantity() == 0)
        {
            foreach (SlotController slot in inventorySlots)
            {
                if (slot.GetQuantity() > 0)
                {
                    visibleSlot.Fill(slot.Item_reference);
                    Debug.Log("Cleaned " + slot.Item_reference.GetName());
                    slot.Clear();
                    slot.SetSelected(false);
                    break;
                }
                visibleSlot.Clear();
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (inventorySlots[i].GetQuantity() == 0)
            {
                if (inventorySlots[i + 1].GetQuantity() > 0)
                {
                    inventorySlots[i].Fill(inventorySlots[i + 1].Item_reference);
                    inventorySlots[i].SetSelected(true);
                    Debug.Log("Cleaned " + inventorySlots[i + i].Item_reference.GetName());
                    inventorySlots[i + 1].Clear();
                    inventorySlots[i + 1].SetSelected(false);
                }
            }
        }

        foreach (SlotController sl in inventorySlots)
        {
            if (sl.GetQuantity() == 0)
            {
                Debug.Log("Cleaned " + sl.Item_reference.GetName());
                sl.Clear();
                sl.SetSelected(false);
            }
        }
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
        visibleSlot.SetEnabled(interacbility);
    }

    public void SetVisibleSlotItem(InventoryItem Item_reference)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].Equals(Item_reference))
            {
                selectedSlot = i;
                inventorySlots[i].SetSelected(true);
                break;
            }
            else if (inventorySlots[i].IsSelected() && i != selectedSlot)
                inventorySlots[i].SetSelected(false);
        }
        visibleSlot.Fill(Item_reference);
    }
}