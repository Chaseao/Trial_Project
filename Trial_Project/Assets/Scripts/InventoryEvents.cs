using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InventoryEvents
{
    static public Action<Item> ItemGained;
    static public Action<Item, int> MultipleItemsGained;
    static public Action<Item> ItemLost;
    static public Action<Item> UseItem;
    static public Action<Item> EquipCrystal;
    static public Action InventoryOpenned;
    static public Action InventoryClosed;

    public static void PickUpItem(Item item)
    {
        ItemGained?.Invoke(item);
    }

    public static void PickUpMultipleItems(Item item, int amount)
    {
        MultipleItemsGained?.Invoke(item, amount);
    }

    public static void LoseItem(Item item)
    {
        ItemLost?.Invoke(item);
    }

    public static void Use(Item item)
    {
        UseItem?.Invoke(item);
    }

    public static void Equip(Item crystalItem)
    {
        if (crystalItem.itemtype == ItemType.Crystal) {
            EquipCrystal?.Invoke(crystalItem);
        }
        else
        {
            Debug.Log("Dude that wasn't a crystal you tried to equip");
        }
    }

    public static void OpenInventory()
    {
        InventoryOpenned?.Invoke();
    }

    public static void CloseInventory()
    {
        InventoryClosed?.Invoke();
    }
}
