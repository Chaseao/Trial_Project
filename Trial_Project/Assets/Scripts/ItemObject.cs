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
    public int itemDurability;

    public Item(ItemObject item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.ItemType;
        isStackable = item.isStackable;
        itemObject = item;
    }

    public Item(CrystalObject item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.ItemType;
        isStackable = item.isStackable;
        itemDurability = item.MaxDurability;
        itemObject = item;
    }

    public Item(Item item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
        itemtype = item.itemtype;
        isStackable = item.isStackable;
        itemObject = item.itemObject;
    }
}
