using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    bool inventoryIsOpen = false;

    private void OnEnable()
    {
        InventoryEvents.InventoryOpenned += () => inventoryIsOpen = true;
        InventoryEvents.InventoryClosed += () => inventoryIsOpen = false;
    }

    private void OnDisable()
    {
        InventoryEvents.InventoryOpenned -= () => inventoryIsOpen = true;
        InventoryEvents.InventoryClosed -= () => inventoryIsOpen = false;
    }

    public void Clicked()
    {
        if (inventoryIsOpen)
        {
            InventoryEvents.CloseInventory();
        }
        else
        {
            InventoryEvents.OpenInventory();
        }
    }
}
