using System.Collections;
using System.Collections.Generic;
using ExternalPackages.InventoryPackage._Scripts_.Inventory;
using UnityEngine;

class ItemModifiedException : System.Exception
{
    public ItemModifiedException(string message) : base(message)
    {
    }
}

//Attribute that allows us to right click->create
[CreateAssetMenu(fileName = "NewItem", menuName = "ItemSystem/Item")]
public class ItemTemplate : ScriptableObject
{
    public new string name = "item";
    public string description = "this is an item";

    private int id = -1;
    public int Id
    {
        get { return id; }
        set {
            id = value;
            throw new ItemModifiedException("Oh no you dont!"); 
        }
    }

    public Sprite icon;
    public bool isConsumable = true;
    [Header("Space on grid")]
    public int columns = 1;
    public int rows = 1;
    public List<ItemEffect> itemEffects;

    //returns whether or not the Item was successfully used
    public bool Use()
    {
        //Debug.Log("Used item: " + name);
        if (itemEffects.Count <= 0) return false;
        
        foreach (var effect in itemEffects)
        {
            effect.Apply();
        }
        return true;

    }
}
