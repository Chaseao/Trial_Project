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
    bool active = true;


    public int TotalAmountOfSlots => totalAmountOfSlots;
    public Dictionary<Item, int> GetItemsInInventory() => items;
    public Item CurrentlyEquippedCrystal => currentlyEquippedCrystal;
    public bool Active => active;


    private void Awake()
    {
        items.Clear();
    }

    private void OnEnable()
    {
        if (active) 
            Activate();
    }

    public void Activate()
    {
        InventoryEvents.ItemGained += AddItem;
        InventoryEvents.ItemLost += LoseItem;
        InventoryEvents.ItemUsed += UseItem;
        InventoryEvents.CrystalEquipped += Equip;
        currentlyEquippedCrystal = null;

        active = true;
    }

    public void Deactivate()
    {
        InventoryEvents.ItemGained -= AddItem;
        InventoryEvents.ItemLost -= LoseItem;
        InventoryEvents.ItemUsed -= UseItem;
        InventoryEvents.CrystalEquipped -= Equip;

        active = false;
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

    private void UseItem(Item newItem)
    {
        bool itemInInventory = CheckForItemOfSameType(newItem);

        if (itemInInventory)
        {
            newItem.itemObject.Use(newItem);
        }
    }

    private void AddItem(Item newItem)
    {
        Item itemMatch = FindMatchingItemOfSameType(newItem);

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

        InventoryEvents.UpdateInventory(this);
    }

    private void Equip(Item itemToEquip)
    {
        if (items.ContainsKey(itemToEquip))
        {
            if (currentlyEquippedCrystal != null)
            {
               InventoryEvents.GainItem(currentlyEquippedCrystal);
            }

            currentlyEquippedCrystal = itemToEquip;
            InventoryEvents.ItemLost(itemToEquip);
        }
    }

    private void LoseItem(Item newItem)
    {
        Item matchingItem = FindMatchingItemOfSameType(newItem);

        if (matchingItem != null)
        {
            items[matchingItem]--;

            if(items[matchingItem] < 1)
            {
                items.Remove(matchingItem);
            }

            InventoryEvents.UpdateInventory(this);
        }
    }

    private bool CheckForItemOfSameType(Item newItem)
    {
        bool inventoryHasItemOfSameType = false;

        foreach (Item item in items.Keys)
        {
            if (newItem.itemObject.Equals(item.itemObject))
            {
                inventoryHasItemOfSameType = true;
            }
        }

        return inventoryHasItemOfSameType;
    }

    private Item FindMatchingItemOfSameType(Item newItem)
    {
        Item matchingItemOfSameType = null;

        foreach (Item item in items.Keys)
        {
            if (newItem.itemObject.Equals(item.itemObject) && matchingItemOfSameType == null)
            {
                matchingItemOfSameType = item;
            }
        }

        return matchingItemOfSameType;
    }
}
