using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InventoryEvents
{
    static public Action<Item> ItemPickedUp;
    static public Action<Item> ItemLost;
    static public Action<Item> UseItem;
    static public Action<CrystalObject> EquipCrystal;

    public static void PickUpItem(Item item)
    {
        ItemPickedUp?.Invoke(item);
    }

    public static void LoseItem(Item item)
    {
        ItemLost?.Invoke(item);
    }

    public static void Use(Item item)
    {
        UseItem?.Invoke(item);
    }

    public static void Equip(CrystalObject crystalObject)
    {
        EquipCrystal?.Invoke(crystalObject);
    }
}
