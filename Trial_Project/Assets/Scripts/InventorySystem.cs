using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventorySystem : ScriptableObject
{
    [SerializeField] int totalAmountOfSlots;
    Dictionary<Item, int> items = new Dictionary<Item, int>();
    Item currentlyEquippedCrystal;


    public int TotalAmountOfSlots => totalAmountOfSlots;
    public Dictionary<Item, int> GetItemsInInventory() => items;
    public Item CurrentlyEquippedCrystal => currentlyEquippedCrystal;



    private void Awake()
    {
        items.Clear();
    }

    private void OnEnable()
    {
        InventoryEvents.ItemGained += AddItem;
        InventoryEvents.MultipleItemsGained += AddMultipleItems;
        InventoryEvents.ItemLost += LoseItem;
        InventoryEvents.UseItem += UseItem;
        InventoryEvents.EquipCrystal += Equip;
        currentlyEquippedCrystal = null;
    }

    private void OnDisable()
    {
        InventoryEvents.ItemGained -= AddItem;
        InventoryEvents.MultipleItemsGained -= AddMultipleItems;
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
            items[itemMatch] += 1;
        }
        else if (totalAmountOfSlots > items.Count)
        {
            Item item = new Item(newItem);
            items.Add(item, 1);
        }
        else
        {
            Debug.Log($"Inventory was full discarding {newItem.name}");
        }

    }

    public void AddMultipleItems(Item newItem, int amount)
    {
        Item itemMatch = FindMatchingItem(newItem);

        if (itemMatch != null && newItem.isStackable)
        {
            items[itemMatch] += amount;
        }
        else if (totalAmountOfSlots >= amount + items.Count)
        {
            Item item = new Item(newItem);
            items.Add(item, amount);
        }
        else if(totalAmountOfSlots > items.Count)
        {
            Item item = new Item(newItem);
            items.Add(item, totalAmountOfSlots - items.Count);
            Debug.Log($"Inventory was full discarding {amount + items.Count - totalAmountOfSlots} {newItem.name}");
        }
        else
        {
            Debug.Log($"Inventory was full discarding {amount} {newItem.name}");
        }

    }

    public void Equip(Item crystalItem)
    {

        if (items.ContainsKey(crystalItem))
        {
            if (currentlyEquippedCrystal != null)
            {
               InventoryEvents.PickUpItem(currentlyEquippedCrystal);
            }
            currentlyEquippedCrystal = crystalItem;
            InventoryEvents.ItemLost(crystalItem);
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
