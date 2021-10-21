using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTester : MonoBehaviour
{
    [SerializeField] ItemObject itemObject;
    [SerializeReference] Item item;

    public void SetItem()
    {
        item = new Item(itemObject);
    }

    public void PickUpItem()
    {
        InventoryEvents.GainItem(item);
    }

    public void UseItem()
    {
        InventoryEvents.UseItem(item);
    }
}
