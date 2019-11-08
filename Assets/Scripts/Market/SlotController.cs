using System;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlotController : MonoBehaviour
{
    //EXTERNAL REFENRENCES
    public Animator imageAnimator;
    public TextMeshProUGUI item_quantity;
    public SlotType slotType;

    //PRIVATE VARIABLES
    BoxCollider2D box;
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

    public void Clear()
    {
        imageAnimator.SetTrigger("default");
        item_quantity.text = "0";
        isEmpty = true;
    }

    void SummonItem(string itemName)
    {
        if (HasSomethingAt(transform.position))
            return;

            switch (itemName)
            {
                case "Bloco Especial":
                    SpecialBlockTilemap.GetSpecialBlockTilemap().GetTilemap().SetTile(new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0), ItemFactory.GetFactory().ProduceSpecialBlock());
                    SpecialBlockTilemap.GetSpecialBlockTilemap().GetTilemap().RefreshAllTiles();
                    break;
                default:
                    Instantiate(ItemFactory.GetFactory().ProduceItem(itemName), transform.position /*+ offset */, Quaternion.identity);
                    break;
            }
        FirebaseMethods.firebaseMethods.IncrementQttItems(itemName);
        item_quantity.text = Player.getInstance().GetPlayerInventory().DecreseQuantityFromItem(itemName).ToString();
    }

    bool HasSomethingAt(Vector3 position) {
        foreach(Tilemap tp in FindObjectsOfType<Tilemap>())
        {
            if (tp.HasTile(new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0)))
            {
                return true;
            }
        }
        ContactFilter2D cf2d = new ContactFilter2D();
        if (box.OverlapCollider(cf2d.NoFilter(), Player.getInstance().GetColliders()) > 0)
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
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        if (!slotType.Equals(SlotType.CURRENT))
            return;

        startPoint = rect.position;
    }

    bool isMoving;
    private void Update()
    {
        
        if (!isEnabled)
            return;

        if (item_quantity.text.Equals("0"))
        {
            InventoryController.GetInventoryController().UpdateUI();
            return;
        }

        if (Input.touchCount == 0 || (Input.touchCount > 1 && Input.GetTouch(1).phase.Equals(TouchPhase.Ended)))
        {
            if (isMoving)
            {
                //if (InventoryController.GetInventoryController().GetCollider().OverlapCollider(new ContactFilter2D().NoFilter(), new Collider2D[] { box }) != 0)
                //{
                    isMoving = false;
                    SummonItem(slot_item);
                    rect.anchoredPosition = startPoint;
                //}
            }

            isMoving = false;
            return;
        }

        Vector3 position = Input.touchCount == 2? Input.GetTouch(1).position : Input.GetTouch(0).position;
        position = Camera.main.ScreenToWorldPoint(position);

        if (box.OverlapPoint(position) || isMoving)
        {
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