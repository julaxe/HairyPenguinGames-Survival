using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotGridDimensioner : MonoBehaviour
{
    private GameObject itemSlotPrefab;

    List<GameObject> listSlots;
    GameObject [,] gridSlots;

    private int rows;
    private int columns;
    public Bag currentBag;
    

    //connections with another bags.
    private void Start()
    {
        itemSlotPrefab = Resources.Load<GameObject>("Prefabs/ItemSlot");
        listSlots = new List<GameObject>();
    }

    public void SetCurrentBag(Bag bag)
    {
        currentBag = bag;
        LoadBag();
    }
    public void LoadBag()
    {
        ClearSlots();
        
        ReSizeGrid(currentBag.columns, currentBag.rows);

        currentBag.ShowItems();
    }

    public void UpdateBag()
    {
        foreach (GameObject item in currentBag.listOfItems)
        {
            //for each item we are going to update the slots and the slotInUse for the item
            List<Slot> newSlotsInUse = new List<Slot>();
            foreach (SlotNode node in item.GetComponent<Item>().slotNodes)
            {
                gridSlots[node.column, node.row].GetComponent<Slot>().Item = item.GetComponent<Item>();
                newSlotsInUse.Add(gridSlots[node.column, node.row].GetComponent<Slot>());
            }
            //update position for item
            item.GetComponent<Item>().SetSlotsInUse(newSlotsInUse);
        }
    }
    public void UnLoadBag()
    {
        currentBag.HideItems();
    }
    
    public void ClearSlots()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                gridSlots[i, j].GetComponent<Slot>().Item = null;
                gridSlots[i, j].GetComponent<Slot>().Bag = currentBag;
            }
        }
    }

    private void ReSizeGrid(int newColumns, int newRows)
    {
        rows = newColumns;
        columns = newRows;
        gridSlots = new GameObject[columns, rows];

        Transform Grid = transform.Find("InventoryBackground");

        //create more slots if we need more
        if (listSlots.Count < rows * columns)
        {
            for(int i = listSlots.Count; i < rows * columns; i++)
            {
                listSlots.Add(Instantiate(itemSlotPrefab, Grid));
            }
        }
        //desactivate all the slot so we can activate them in order
        for (int i = 0; i < listSlots.Count; i++)
        {
            listSlots[i].SetActive(false);
        }
        //organize the slots that we have in the new grid
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                gridSlots[i, j] = listSlots[(i * rows) + j];
                gridSlots[i, j].GetComponent<Slot>().column = i;
                gridSlots[i, j].GetComponent<Slot>().row = j;
                gridSlots[i, j].SetActive(true);
            }
        }

        
    }

}
