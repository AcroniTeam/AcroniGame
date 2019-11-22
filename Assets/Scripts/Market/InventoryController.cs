using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    static InventoryController inventoryController;

    public PopUpInventory popUp1item;
    public PopUpInventory popUp2item;
    public PopUpInventory popUp3item;

    public void AddItem(InventoryItem item)
    {
        popUp1item.FillSlots(item);
        popUp2item.FillSlots(item);
        //popUp3item.FillSlots(item);
        if(!popUp1item.GetSlot(0).IsEmpty())
        {
            InventoryPopUpController.GetPopUpController().Release(0);
        }
    }

    public void UpdateUI()
    {
        //if (current_slot.GetQuantity() == 0)
        //{
        //    if (next_slot.GetQuantity() > 0 && previous_slot.GetQuantity() > 0)
        //    {
        //        next_controller_copy.Fill(previous_slot.item_reference);
        //        InventoryPopUpController.GetPopUpController().SwitchInventories();
        //        InventoryPopUpController.GetPopUpController().BlockInventory(1);
        //        current_slot.Fill(next_slot.item_reference);
        //        next_slot.Clear();
        //        previous_slot.Clear();
        //    }
        //    else if (next_controller_copy.GetQuantity() > 0)
        //    {
        //        InventoryPopUpController.GetPopUpController().SwitchInventories();
        //        InventoryPopUpController.GetPopUpController().BlockInventory(0);
        //        current_slot.Fill(next_controller_copy.item_reference);
        //        next_controller_copy.Clear();
        //    } else
        //    {
        //        next_controller_copy.Clear();
        //        InventoryPopUpController.GetPopUpController().BlockInventory(0);
        //        current_slot.Clear();
        //    }
        //}
        //else if (next_slot.GetQuantity() > 0 || previous_slot.GetQuantity() > 0)
        //{
        //    if (next_slot.GetQuantity() > 0 && previous_slot.GetQuantity() > 0)
        //    {
        //        InventoryPopUpController.GetPopUpController().Release(1);
        //    }
        //    else if (next_slot.GetQuantity() == 0 && previous_slot.GetQuantity() > 0)
        //    {
        //        next_controller_copy.Fill(previous_slot.item_reference);
        //        InventoryPopUpController.GetPopUpController().BlockInventory(1);
        //        InventoryPopUpController.GetPopUpController().SwitchInventories();
        //        next_slot.Clear();
        //        previous_slot.Clear();
        //    }
        //    else if (next_slot.GetQuantity() > 0 && previous_slot.GetQuantity() == 0)
        //    {
        //        next_controller_copy.Fill(next_slot.item_reference);
        //        InventoryPopUpController.GetPopUpController().BlockInventory(1);
        //        InventoryPopUpController.GetPopUpController().SwitchInventories();
        //        previous_slot.Clear();
        //        next_slot.Clear();
        //    }
        //} else if (next_controller_copy.GetQuantity() == 0) {
        //    InventoryPopUpController.GetPopUpController().BlockInventory(0);
        //    InventoryPopUpController.GetPopUpController().SwitchInventories();
        //    next_controller_copy.Clear();
        //}
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
        //current_slot.SetEnabled(interacbility);
    }

    void Start()
    {
        inventoryController = this;
    }
}
