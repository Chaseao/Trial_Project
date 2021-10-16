using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject
{
    Dictionary<Item, int> items = new Dictionary<Item, int>();
    Item currentlyEquippedCrystal;
    public Item CurrentlyEquippedCrystal => currentlyEquippedCrystal;
    public Dictionary<Item, int> GetItemsInInventory() => items;

    private void Awake()
    {
        items.Clear();
    }

    private void OnEnable()
    {
        InventoryEvents.ItemPickedUp += AddItem;
        InventoryEvents.ItemLost += LoseItem;
        InventoryEvents.UseItem += UseItem;
        InventoryEvents.EquipCrystal += Equip;
    }

    private void OnDisable()
    {
        InventoryEvents.ItemPickedUp -= AddItem;
        InventoryEvents.ItemLost -= LoseItem;
        InventoryEvents.UseItem -= UseItem;
        InventoryEvents.EquipCrystal += Equip;
    }

    public void PrintItems()
    {
        foreach (Item item in items.Keys)
        {
            Debug.Log($"Contains {items[item]} {item.name} of type {item.itemtype}");
        }
        
        if(items.Count == 0)
        {
            Debug.Log("Inventory is currently empty");
        }
    }

    public void UseItem(Item newItem)
    {
        bool hasItem = CheckForItem(newItem);

        if (hasItem)
        {
            newItem.itemObject.Use(newItem);
        }
    }

    public void AddItem(Item newItem)
    {
        Item itemMatch = FindMatchingItem(newItem);

        if (itemMatch != null && newItem.isStackable)
        {
            items[itemMatch]++;
        }
        else
        {
            Item item = new Item(newItem);
            items.Add(item, 1);
        }

    }

    public void Equip(CrystalObject crystalObject)
    {
        Item newItem = new Item(crystalObject);

        bool hasItem = CheckForItem(newItem);

        if (hasItem)
        {
            currentlyEquippedCrystal = newItem;
        }
    }

    public void LoseItem(Item newItem)
    {
        Item matchingItem = FindMatchingItem(newItem);

        if (matchingItem != null)
        {
            items[matchingItem]--;
            if(items[matchingItem] < 1)
            {
                items.Remove(matchingItem);
            }
        }
    }

    private bool CheckForItem(Item newItem)
    {
        bool hasItem = false;

        foreach (Item item in items.Keys)
        {
            if (newItem.itemObject.Equals(item.itemObject))
            {
                hasItem = true;
            }
        }

        return hasItem;
    }

    private Item FindMatchingItem(Item newItem)
    {
        Item matchingItem = null;

        foreach (Item item in items.Keys)
        {
            if (newItem.itemObject.Equals(item.itemObject) && matchingItem == null)
            {
                matchingItem = item;
            }
        }

        return matchingItem;
    }
}
