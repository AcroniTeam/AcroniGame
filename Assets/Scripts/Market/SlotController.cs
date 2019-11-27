using System;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlotController : MonoBehaviour
{
    //EXTERNAL REFENRENCES
    public Animator imageAnimator;
    public TextMeshProUGUI item_quantity;
    public BoxCollider2D interactible_box;
    public BoxCollider2D box_checktrigger;

    public SlotType slotType;    
    public RectTransform rect;

    string slot_item;
    bool isEmpty = true;
    Vector3 startPoint;
    //Vector3 offset = new Vector3(0.43f,0.43f);
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
                Instantiate(ItemFactory.GetFactory().ProduceItem(itemName), p_position + new Vector3(0,0,2) , Quaternion.identity);
                break;
        }

        isBeingHeld = false;

        FirebaseMethods.firebaseMethods.IncrementQttItems(itemName);
        Debug.Log(itemName);
        item_quantity.text = Player.getInstance().GetPlayerInventory().DecreseQuantityFromItem(itemName).ToString();
    }

    bool HasSomethingAt(Vector3 position) {
        foreach (Tilemap tp in FindObjectsOfType<Tilemap>())
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

    public void SetEnabled(bool enabled) {
        isEnabled = enabled;
    }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        startPoint = GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log(slotType.ToString().ToUpper() + "\nX: " + startPoint.x + "\nY: " + startPoint.y);
    }

    bool isMoving;
    private void Update()
    {
        if (!isEnabled)
            return;

        if(item_quantity.text.Equals("0"))
            return;

        if (Input.touchCount == 0 || (Input.touchCount > 1 && Input.GetTouch(1).phase.Equals(TouchPhase.Ended)))
        {
            if (isMoving)
            {

                isMoving = false;
                SummonItem(slot_item);
                rect.anchoredPosition = startPoint;

                if (item_quantity.text.Equals("0"))
                {
                    isMoving = false;
                    InventoryController.GetInventoryController().UpdateUI();

                    return;
                }
            }

            isMoving = false;
            return;
        }

        Vector3 position;
        position = Input.touchCount == 2 ? Input.GetTouch(1).position : Input.GetTouch(0).position;

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
    
    #endregion
}

public enum SlotType
{
    PREVIOUS, CURRENT, NEXT
}