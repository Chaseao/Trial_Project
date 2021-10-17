using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InventoryEvents
{
    static public Action<Item> ItemGained;
    static public Action<Item> ItemLost;
    static public Action<Item> ItemUsed;
    static public Action<Item> CrystalEquipped;
    static public Action InventoryOpened;
    static public Action InventoryClosed;

    public static void GainItem(Item item)
    {
        ItemGained?.Invoke(item);
    }

    public static void LoseItem(Item item)
    {
        ItemLost?.Invoke(item);
    }

    public static void UseItem(Item item)
    {
        ItemUsed?.Invoke(item);
    }

    public static void EquipCrystal(Item crystalItem)
    {
        if (crystalItem.itemtype == ItemType.Crystal) {
            CrystalEquipped?.Invoke(crystalItem);
        }
        else
        {
            Debug.Log("Dude that wasn't a crystal you tried to equip");
        }
    }

    public static void OpenInventory()
    {
        InventoryOpened?.Invoke();
    }

    public static void CloseInventory()
    {
        InventoryClosed?.Invoke();
    }
}
