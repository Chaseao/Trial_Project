using UnityEngine;
[CreateAssetMenu(fileName = "New Key Object", menuName = "Inventory System/Items/Key")]

public class KeyObject : ItemObject
{
    //This class is fairly simple. It needs to derive from ItemObject.
    //You just need to set the Item type to key.

    private void OnEnable() => ItemType = ItemType.Key;

    public override void Use(Item item)
    {
        Debug.Log("Key item...");
    }

    public override void UseEquipped(Item item)
    {
        throw new System.NotImplementedException();
    }
}
