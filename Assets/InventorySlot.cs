using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    //EXTERNAL REFENRENCES
    public Animator imageAnimator;
    public TextMeshProUGUI item_quantity;
    public Image selectedImage;

    string slot_item;
    bool isEmpty = true;
    bool isEnabled = true;
    [HideInInspector]
    public InventoryItem item_reference;

    #region SlotController Methods

    public void Fill(InventoryItem slot)
    {
        slot_item = slot.GetName();

        imageAnimator.SetBool("BeDefault", false);
        imageAnimator.SetTrigger(slot_item);

        item_quantity.text = slot.GetQuantity().ToString();
        item_reference = slot;

        isEmpty = false;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public void DecreaseQuantity()
    {
        if (item_quantity.text.Equals("0"))
            Clear();
        else if (Int16.Parse(item_quantity.text) > 0)
        {
            int i = item_reference.DecreaseQuantity();
            if(i == 0)
            item_quantity.text = i.ToString();
        }
    }

    public bool Equals(InventoryItem item)
    {
        return item.GetName() == slot_item;
    }

    public int GetQuantity()
    {
        return Int16.Parse(item_quantity.text);
    }

    static bool isBeingHeld = false;
    static SlotType HeldType;

    public void Clear()
    {
        imageAnimator.SetBool("BeDefault", true);
        imageAnimator.SetTrigger("default");
        item_quantity.text = "0";
        isEmpty = true;
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
    }

    public void SetSelected(bool value)
    {
        selectedImage.enabled = value;
    }

    public bool isSelected()
    {
        return selectedImage.enabled;
    }

    public void OnClick()
    {
        InventoryController.GetInventoryController().SetSelected(item_reference);
    }
    #endregion

    private void Awake()
    {
        imageAnimator = GetComponentInChildren<Animator>();
        selectedImage = Array.Find(GetComponentsInChildren<Image>(), image => image.name.Equals("SelectedItem"));
        item_quantity = GetComponentInChildren<TextMeshProUGUI>();
        selectedImage.enabled = false;
    }
}
