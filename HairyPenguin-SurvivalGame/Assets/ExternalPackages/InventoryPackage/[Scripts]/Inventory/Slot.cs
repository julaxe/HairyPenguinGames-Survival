using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Node
{
    public int column;
    public int row;
    public bool inUse;
    public bool root;
}

public class Slot : MonoBehaviour
{
    private Bag bag;
    public Bag Bag
    {
        get { return bag; }
        set { bag = value; }
    }
   
    private Item itemInSlot = null;
    public Item Item
    {
        get{ return itemInSlot;}
        set
        {
            if(itemInSlot == value)
            {
                return;
            }
            itemInSlot = value;
            RefreshItem();
        }
    }
    private bool root;
    public bool Root
    {
        get{return root;}
        set
        {
            if (root == value)
            {
                return;
            }
            root = value;
        }
    }

    //collider of the slots
    private GameObject cllider;

    //possition in the grid
    public int column;
    public int row;


    private void Start()
    {      
        RefreshItem();
    }

    public void RefreshItem()
    {
        cllider = transform.Find("CollisionBox").gameObject;
        if (!itemInSlot)
       {
            cllider.GetComponent<Image>().color = new Color(0.8584906f, 0.8584906f, 0.8584906f, 0.8117647f);
        }
        else
        {
            cllider.GetComponent<Image>().color = new Color(0.8396226f, 0.0f, 0.0f, 0.8117647f);
        }
    }
}
