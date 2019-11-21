﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[System.Serializable]
public class SlotController : MonoBehaviour
{
    [Header("External References")]
    public Animator imageAnimator;
    public TextMeshProUGUI item_quantity;
    public BoxCollider2D interactible_box;

    public Image isSelected;

    [Header("Extras")]
    public bool isMovingObject = false;
    [ConditionalHide("isMovingObject", true)]
    public SlotType slotType;
    [ConditionalHide("isMovingObject", true)]
    public RectTransform rect;
    [ConditionalHide("isMovingObject", true)]
    public BoxCollider2D box_checktrigger;

    //PRIVATE VARIABLES
    string slot_item;
    bool isEmpty = true;
    Vector3 startPoint;
    //Vector3 offset = new Vector3(0.43f,0.43f);
    bool isEnabled = true;
    [HideInInspector]
    public InventoryItem Item_reference;

    #region SlotController Methods

    public void Fill(InventoryItem slot)
    {
        slot_item = slot.GetName();
        imageAnimator.SetBool("BeDefault", false);
        imageAnimator.SetTrigger(slot_item);

        item_quantity.text = slot.GetQuantity().ToString();
        Item_reference = slot;

        isEmpty = false;
    }

    public bool IsEmpty()
    {
        return isEmpty;
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
        imageAnimator.SetBool("BeDefault",true);
        imageAnimator.SetTrigger("default");
        item_quantity.text = "0";
        isEmpty = true;
        SetSelected(false);
    }

    void SummonItem(string itemName)
    {
        Vector3 p_position = Camera.main.ScreenToWorldPoint(transform.position);

        if (HasSomethingAt(p_position))
            return;

        switch (itemName)
        {
            case "Bloco Especial":
                SpecialBlockTilemap.GetSpecialBlockTilemap().GetTilemap().SetTile(new Vector3Int(Mathf.FloorToInt(p_position.x), Mathf.FloorToInt(p_position.y), 0), ItemFactory.GetFactory().ProduceSpecialBlock());
                SpecialBlockTilemap.GetSpecialBlockTilemap().GetTilemap().RefreshAllTiles();
                break;
            default:
                Instantiate(ItemFactory.GetFactory().ProduceItem(itemName), p_position + new Vector3(0,0,+2) /*+ offset */, Quaternion.identity);
                break;
        }

        isBeingHeld = false;

        FirebaseMethods.firebaseMethods.IncrementQttItems(itemName);
        item_quantity.text = Player.getInstance().GetPlayerInventory().DecreseQuantityFromItem(itemName).ToString();
    }

    bool HasSomethingAt(Vector3 position) {
        foreach (Tilemap tp in System.Array.FindAll(FindObjectsOfType<Tilemap>(), tm => !tm.name.StartsWith("Top") && !tm.name.StartsWith("Bottom") && !tm.name.Equals("Coin")))
        {
            if (tp.HasTile(new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0)))
            {
                return true;
            }
        }

        ContactFilter2D cf2d = new ContactFilter2D();
        if (box_checktrigger.IsTouching(cf2d.NoFilter()))
        {
            return true;
        }
        return false;
    }

    public void DecreseQuantityFromSlot()
    {
        item_quantity.text = (Int32.Parse(item_quantity.text) - 1).ToString();
    }

    public void SetEnabled(bool enabled) {
        isEnabled = enabled;
    }

    void GetClick()
    {
        if (Input.touchCount == 0)
            return;

        Vector3 position = Input.touchCount == 2 ? Input.GetTouch(1).position : Input.GetTouch(0).position;
        if (interactible_box.OverlapPoint(position) && GetQuantity() > 0)
        {
            InventoryController.GetInventoryController().SetVisibleSlotItem(Item_reference);
        }
    }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        startPoint = GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log(slotType.ToString().ToUpper() + "\nX: " + startPoint.x + "\nY: " + startPoint.y);

        isSelected = System.Array.Find(GetComponentsInChildren<Image>(), im => im.name.Equals("selectedImage"));
        isSelected.enabled = false;
    }

    bool isMoving;
    private void Update()
    {
        if (!isEnabled)
            return;

        if(!isMovingObject)
        {
            GetClick();
            return;
        }

        if(item_quantity.text.Equals("0"))
            return;

        if (Input.touchCount == 0 || (Input.touchCount > 1 && Input.GetTouch(1).phase.Equals(TouchPhase.Ended)))
        {
            if (isMoving)
            {

                isMoving = false;
                SummonItem(slot_item);
                rect.anchoredPosition = startPoint;

                InventoryController.GetInventoryController().UpdateUI();
                return;
            }

            isMoving = false;
            return;
        }

        Vector3 position = Input.touchCount == 2? Input.GetTouch(1).position : Input.GetTouch(0).position;
        //position = Camera.main.ScreenToWorldPoint(position);

        if (interactible_box.OverlapPoint(position) || isMoving)
        {
            if(!isBeingHeld)
            {
                isBeingHeld = true;
                HeldType = slotType;
            }

            if (!HeldType.Equals(slotType))
                return;

            isMoving = true;
            Vector3 Position = new Vector3(position.x, position.y, 0);
            rect.position = Position;
        }
    }
    
    public void SetSelected(bool value)
    {
        isSelected.enabled = value;
    }

    public bool IsSelected()
    {
        return isSelected.enabled;
    }
    #endregion
}

public enum SlotType
{
    PREVIOUS, CURRENT, NEXT
}