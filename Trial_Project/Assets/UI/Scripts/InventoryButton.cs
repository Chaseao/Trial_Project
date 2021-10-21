using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    bool inventoryIsOpen = false;

    private void OnEnable()
    {
        InventoryEvents.InventoryOpened += () => inventoryIsOpen = true;
        InventoryEvents.InventoryClosed += () => inventoryIsOpen = false;
    }

    private void OnDisable()
    {
        InventoryEvents.InventoryOpened -= () => inventoryIsOpen = true;
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
