using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenerateItem
{
    public ItemTemplate Template;
    public int Count;
}
public class SlotNode
{
    public SlotNode(int column, int row, GameObject item = null)
    {
        this.column = column;
        this.row = row;
        this.item = item;
    }
    public int row;
    public int column;
    public GameObject item;
}
public class Bag : MonoBehaviour
{
    public List<GameObject> listOfItems;

    private SlotNode[,] listSlotNode;

    private GameObject itemPrefab;
    private Transform ItemsLocation;

    [Header("Initial Values")]
    public int rows = 5;
    public int columns = 5;
    public List<GenerateItem> initialItems;

    private bool bagIsActive;
    void Start()
    {
        listOfItems = new List<GameObject>();
        listSlotNode = new SlotNode[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                listSlotNode[i, j] = new SlotNode(i, j);
            }
        }
        itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        ItemsLocation = GameObject.Find("Canvas/Items").transform;

        foreach(GenerateItem item in initialItems)
        {
            AddNewItemToTheBag(item);
        }

        HideItems();

    }

    public void AddNewItemToTheBag(GenerateItem item)
    {
        if(item.Template.isConsumable)
        {
            //Add consumable item
            AddConsumable(item);
        }
        else
        {
            //Add No consumable item
            for (int i = 0; i < item.Count; i++)
            {
                AddNoConsumable(item);
            }
        }
    }
    // Update is called once per frame
    private void AddConsumable(GenerateItem template)
    {
        GameObject item = Instantiate(itemPrefab, ItemsLocation);
        item.GetComponent<Item>().ItemTemplate = template.Template;
        item.GetComponent<Item>().ItemCount = template.Count;
        AddAnItemToTheBagViaCode(item);
        item.SetActive(bagIsActive);
    }
    private void AddNoConsumable(GenerateItem template)
    {
        GameObject item = Instantiate(itemPrefab, ItemsLocation);
        item.GetComponent<Item>().ItemTemplate = template.Template;
        AddAnItemToTheBagViaCode(item);
        item.SetActive(bagIsActive);
    }

    public void HideItems()
    {
        foreach (GameObject item in listOfItems)
        {
            item.SetActive(false);
        }

        bagIsActive = false;
    }

    public void ShowItems()
    {
        foreach (GameObject item in listOfItems)
        {
            item.SetActive(true);
        }

        bagIsActive = true;
    }

    private void AddAnItemToTheBagViaCode(GameObject item)
    {
        //the item is already created
        //First I need to know if there is space in the grid.
        int ItemRows = item.GetComponent<Item>().ItemTemplate.rows;
        int ItemColumns = item.GetComponent<Item>().ItemTemplate.columns;
        SlotNode availableNode = null;
        foreach (SlotNode node in listSlotNode)
        {
            if(node.item != null) // go to next node
            {
                continue;
            }
            //for this node check if there is space
            int currentRow = node.row;
            int currentColumn = node.column;
            bool available = true;
            for (int i = 0; i < ItemRows; i++) //check every extra row
            { 
                if(currentRow + i < rows) //not outside the grid
                {
                    for (int j = 0; j < ItemColumns; j++) //check every extra column
                    {
                        if (currentColumn + j < columns) //not outside the grid
                        {
                            if (listSlotNode[currentColumn + j, currentRow + i].item != null)
                            {
                                available = false;
                                break;
                            }
                        }
                        else
                        {
                            available = false;
                            break;
                        }
                    }
                }
                else
                {
                    available = false;
                    break;
                }
            }
            if(available)
            {
                availableNode = node;
                break;
            }
        }
        if(availableNode != null) //there is space
        {
            AddNewItemInFreeSpace(item, availableNode);
        }
        else
        {
            Debug.Log("There is no space in the bag for another Item");
            Destroy(item);
        }
    }
    private void AddNewItemInFreeSpace(GameObject item, SlotNode root)
    {
        //add the item to the slots
        int currentRow = root.row;
        int currentColumn = root.column;
        int itemRows = item.GetComponent<Item>().ItemTemplate.rows;
        int itemColumns = item.GetComponent<Item>().ItemTemplate.columns;

        for(int i = 0; i < itemColumns; i++)
        {
            for(int j = 0; j < itemRows; j++)
            {
                listSlotNode[currentColumn + i, currentRow + j].item = item;
                item.GetComponent<Item>().slotNodes.Add(listSlotNode[currentColumn + i, currentRow + j]);
            }
        }
        AddItemToList(item);
    }
    public void DeleteFromlist(GameObject item)
    {
        listOfItems.Remove(item);
    }
    public void AddItemToList(GameObject item)
    {
        listOfItems.Add(item);
    }
}
