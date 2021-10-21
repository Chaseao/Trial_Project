using UnityEngine;


public abstract class Equippable
{
    abstract public void UseEquipped(Item item);
}

[System.Serializable]
public class EquippableItem
{
    public string name;
    public int id;
    public Sprite ui_icon;
    public ItemType itemtype;
    public bool isStackable;
    public ItemObject itemObject;
    public int itemUses;
    public int maxUses;

    public EquippableItem(ItemObject item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.ItemType;
        isStackable = item.isStackable;
        itemObject = item;
        itemUses = 1;
    }

    public EquippableItem(Item item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.itemtype;
        isStackable = item.isStackable;
        itemObject = item.itemObject;
        itemUses = item.itemUses;
    }
}

