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
        InventoryEvents.ItemLost += LoseItem;
        InventoryEvents.ItemUsed += UseItem;
        InventoryEvents.CrystalEquipped += Equip;
        currentlyEquippedCrystal = null;
    }

    private void OnDisable()
    {
        InventoryEvents.ItemGained -= AddItem;
        InventoryEvents.ItemLost -= LoseItem;
        InventoryEvents.ItemUsed -= UseItem;
        InventoryEvents.CrystalEquipped -= Equip;
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
        bool itemInInventory = CheckForItemOfSameType(newItem);

        if (itemInInventory)
        {
            newItem.itemObject.Use(newItem);
        }
    }

    public void AddItem(Item newItem)
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

    }

    public void Equip(Item itemToEquip)
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

    public void LoseItem(Item newItem)
    {
        Item matchingItem = FindMatchingItemOfSameType(newItem);

        if (matchingItem != null)
        {
            items[matchingItem]--;

            if(items[matchingItem] < 1)
            {
                items.Remove(matchingItem);
            }
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
