﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;


public class InventoryController : MonoBehaviour
{
    static InventoryController inventoryController;
    public Animator imageAnimator;

    public SlotController previous_slot;
    public SlotController current_slot;
    public SlotController next_slot;
    public SlotController next_controller_copy;

    public void AddItem(InventoryItem item)
    {
        if (current_slot.IsEmpty() || current_slot.Equals(item))
        {
            current_slot.Fill(item);
            return;
        }else if (next_slot.IsEmpty() || next_slot.Equals(item))
        {
            InventoryPopUpController.GetPopUpController().Release(0);
            next_slot.Fill(item);
            next_controller_copy.Fill(item);
            return;
        }else if (previous_slot.IsEmpty() || previous_slot.Equals(item))
        {
            InventoryPopUpController.GetPopUpController().Release(1);
            previous_slot.Fill(item);
            return;
        }
    }

    public void UpdateUI()
    {
        if(current_slot.GetQuantity() == 0)
        {
            if (!next_slot.IsEmpty())
            {
                current_slot.Fill(next_slot.item_reference);

                if(!previous_slot.IsEmpty())
                {
                    next_slot.Fill(previous_slot.item_reference);
                    
                    if (Player.getInstance().GetPlayerInventory().HasItemAt(3))
                    {
                        previous_slot.Fill(Player.getInstance().GetPlayerInventory().GetItemAt(3));
                        Player.getInstance().GetPlayerInventory().RemodelList();
                    }
                    else
                    {
                        next_controller_copy.Fill(previous_slot.item_reference);
                        previous_slot.Clear();
                        InventoryPopUpController.GetPopUpController().BlockInventory(1);
                    }   
                }
                else
                {
                    next_slot.Clear();
                    next_controller_copy.Clear();
                    InventoryPopUpController.GetPopUpController().Block();
                }
            }
            else
                current_slot.Clear();
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
        current_slot.SetEnabled(interacbility);
    }

    void Start()
    {
        inventoryController = this;
    }
}
