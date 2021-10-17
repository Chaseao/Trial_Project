using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlotUI : SlotUI
{
    new public void UseItem()
    {
        if (slot != null)
        {
            slot.item.itemObject.UseEquipped(slot.item);
        }
    }
}
