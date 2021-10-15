using UnityEngine;

public enum ItemType
{
    Heal,
    Key,
    Crystal
}

public abstract class ItemObject : ScriptableObject
{
    public int id;
    public Sprite ui_icon;
    public ItemType type;
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public Sprite ui_icon;
    public Item(ItemObject item)
    {
        name = item.name;
        id = item.id;
        ui_icon = item.ui_icon;
    }
}
