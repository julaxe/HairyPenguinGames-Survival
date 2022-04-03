using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


// Display item in the slot, update image, make clickable when there is an item, invisible when there is not
public class Item : MonoBehaviour
{
    private int itemCount = 1;
    public int ItemCount //getter ans setter for itemCount
    {
        get{return itemCount;}
        set
        {
            itemCount = value;
            text.text = itemCount.ToString();
        }
    }

    private ItemTemplate itemTemplate;

    public ItemTemplate ItemTemplate
    {
        get { return itemTemplate; }
        set
        {
            if(itemTemplate == value)
            {
                return;
            }
            itemTemplate = value;           
            RefreshItem();
        }
    }

    private Image icon;
    private TMPro.TextMeshProUGUI text;
    private BoxCollider2D boxcollider;


    private int numberOfSlots;
    private List<Slot> previousSlots = new List<Slot>();

    private float width;
    private float height;
    private bool dragging =false;
    private float tapTime = 0.5f;
    private float draggingTimer;

    [NonSerialized]
    public List<SlotNode> slotNodes = new List<SlotNode>(); //for the bag
    [NonSerialized]
    public List<Slot> slotsInUse = new List<Slot>();

    public float itemSlotSize = 96.0f;

    void Start()
    {
        RefreshItem();
    }

    private void Update()
    {
        if(dragging)
        {
            draggingTimer += Time.deltaTime;
            if(draggingTimer >= tapTime)
                transform.position = Touchscreen.current.touches[0].position.ReadValue();
        }
    }
    public void UseItemInSlot()
    {
        
    }

    public void ChangeNumberOfItems()
    {
        if (ItemCount < 1)
        {
            //set itemSlots in slot to null
        }
    }
    public void RefreshItem()
    {
        // get all the values when the item is change
        boxcollider = GetComponent<BoxCollider2D>();
        icon = GetComponent<Image>();
        text = transform.Find("ItemCount").GetComponent<TMPro.TextMeshProUGUI>();

        icon.sprite = ItemTemplate.icon;
        //new size
        width = 100 * ItemTemplate.columns;
        height  = 100 * ItemTemplate.rows;
        icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        icon.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        boxcollider.size = new Vector2(width - 20, height - 20);
        text.text = ItemCount.ToString();

        numberOfSlots = ItemTemplate.columns * ItemTemplate.rows;
        
    }
  
    

    public void onClickEventStart()
    {
        dragging = true;
    }

    public void onClickEventEnd()
    {
        dragging = false;
        if (draggingTimer >= tapTime)
        {
            MoveItemSlot();
        }
        else
        {
            if (itemTemplate.Use())
            {
                ItemCount -= 1;
                if (ItemCount <= 0)
                {
                    DeleteItem();
                }
            }
            //Debug.Log("Tapping!");
        }

        draggingTimer = 0.0f;
    }

    private void MoveItemSlot()
    {
        //check if it can be added to the current slots
        if(slotsInUse.Count == numberOfSlots)
        {
            //check if the item is consumable
            if (itemTemplate.isConsumable)
            {
                //we check if we are changing bags
                if (previousSlots[0].Bag != slotsInUse[0].Bag)
                {
                    //we check if the new bag has the same type of item
                    foreach (GameObject item in slotsInUse[0].Bag.listOfItems)
                    {
                        if (item.GetComponent<Item>().ItemTemplate == itemTemplate)
                        {
                            //we change add the items and change the bags
                            item.GetComponent<Item>().ItemCount += itemCount;
                            DeleteItem();
                            return;
                        }
                    }
                    //this means that there is no similar item in the new bag so we just add it
                }
            }
            //same amount of slots, now check if they are available.
            foreach (Slot slot in slotsInUse)
            {
                //check if the slot doesn't have an item, and if it has, check that is not being use by this item.
                if(slot.Item != null && !previousSlots.Find(x => x == slot))
                {
                    GoBackToPreviousPosition();
                    return;
                }
            }
            //if I'm here it means that the slots are available, so set the item.
            //also get the min,max for rows and columns.
            //add to bag and delete from old bag
            if(previousSlots[0].Bag != slotsInUse[0].Bag)
            {
                //we have to change bag.
                previousSlots[0].Bag.DeleteFromlist(this.gameObject);
                slotsInUse[0].Bag.AddItemToList(this.gameObject);
            }

            //set the new slots
            SetNewSlots();
        }
        else
        {
            GoBackToPreviousPosition();
        }
    }

    public void DeleteItem()
    {
        previousSlots[0].Bag.DeleteFromlist(this.gameObject);
        ClearPreviousSlots();
        Destroy(this.gameObject);
    }
    
    public void GoBackToPreviousPosition()
    {
        if(previousSlots.Count>0)
        {
            SetPositionWithSlots(previousSlots);
        }
    }
    private void SetPositionWithSlots(List<Slot> slots)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Root)
            {
                Vector3 newPos = new Vector3(slot.transform.position.x + (width*0.5f), slot.transform.position.y - (height*0.5f), 0.0f);
                
                transform.position = newPos;
            }
        }
    }
    private void ClearPreviousSlots()
    {
        foreach(Slot slot in previousSlots)
        {
            slot.Root = false;
            slot.Item = null;
        }
        previousSlots.Clear();
    }
    private void AddToBag()
    {
        SetNewSlots();
    }
    private void DeleteOldBag()
    {
        if(previousSlots.Count>1)
        {
            //previousSlots[0].bag.DeleteFromlist(this);
        }
    }
    public void SetSlotsInUse(List<Slot> newSlotsInUse)
    {
        slotsInUse = newSlotsInUse;
        SetNewSlots();
    }
    private void SetNewSlots()
    {
        ClearPreviousSlots();
        int minRow = slotsInUse[0].row;
        int minCol = slotsInUse[0].column;

        slotNodes.Clear();
        foreach (Slot slot in slotsInUse)
        {
            if (slot.row < minRow)
            {
                minRow = slot.row;
            }
            if (slot.column < minCol)
            {
                minCol = slot.column;
            }
            slot.Item = this;
            previousSlots.Add(slot);
            slotNodes.Add(new SlotNode(slot.column, slot.row, this.gameObject));
        }
        foreach (Slot slot in slotsInUse)
        {
            if (slot.row == minRow && slot.column == minCol)
            {
                //he is the root.
                slot.Root = true;
                break;
            }
        }
        SetPositionWithSlots(slotsInUse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slot")
        {
            if(!slotsInUse.Contains(collision.transform.parent.GetComponent<Slot>()))
                slotsInUse.Add(collision.transform.parent.GetComponent<Slot>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Slot")
        {
            slotsInUse.Remove(collision.transform.parent.GetComponent<Slot>());
        }
    }
}
