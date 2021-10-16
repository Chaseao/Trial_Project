using UnityEngine;

public enum ItemType
{
    Heal,
    Crystal,
    Key
}

public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite ui_icon;
    public ItemType ItemType { get; set; }
    public bool isStackable;

    abstract public void Use(Item item);
    abstract public void UseEquipped(Item item);
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite ui_icon;
    public ItemType itemtype;
    public bool isStackable;
    public ItemObject itemObject;
    public int itemUses;

    public Item(ItemObject item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.ItemType;
        isStackable = item.isStackable;
        itemObject = item;
        itemUses = 1;
    }

    public Item(Item item)
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
