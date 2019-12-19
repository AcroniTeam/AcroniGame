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

    #region InventorySlot Methods

    public void Fill(InventoryItem slot)
    {
        slot_item = slot.GetName();

        imageAnimator.SetBool("BeDefault", false);
        imageAnimator.SetTrigger(slot_item);

        item_quantity.text = slot.GetQuantity().ToString();
        item_reference = slot;
        quantity = slot.GetQuantity();
        isEmpty = false;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    int quantity = 0;
    public void DecreaseQuantity()
    {
        quantity--;
        item_reference.DecreaseQuantity();
        Debug.Log(quantity);
        item_quantity.text = quantity.ToString();
    }

    public bool Equals(InventoryItem item)
    {
        return (item.GetName() == slot_item);
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public void Clear()
    {
        quantity = 0;
        imageAnimator.SetBool("BeDefault", true);
        imageAnimator.SetTrigger("default");
        item_quantity.text = quantity.ToString();
        isEmpty = true;
    }

    public void SetEnabled(bool enabled)
    {
        isEnabled = enabled;
    }

    public void SetSelected(bool value)
    {
        if (GetQuantity() > 0)
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

    private void Start()
    {
        imageAnimator = GetComponentInChildren<Animator>();
        selectedImage = Array.Find(GetComponentsInChildren<Image>(), image => image.name.Equals("SelectedItem"));
        item_quantity = GetComponentInChildren<TextMeshProUGUI>();
        selectedImage.enabled = false;
    }

    private void Update()
    {
        if (quantity <= 0)
        {
            Clear();
            return;
        }
    }
}
