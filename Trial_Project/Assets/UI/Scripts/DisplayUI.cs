using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUI : MonoBehaviour
{
    InventorySlotUI[] slotsUI;
    EquipmentSlotUI equipmentSlotUI;

    private void OnEnable()
    {
        slotsUI = GetComponentsInChildren<InventorySlotUI>();
        equipmentSlotUI = GetComponentInChildren<EquipmentSlotUI>();
    }

    public int GetAmountOfSlots() => slotsUI.Length;

    public void SetEquipmentSlot(Slot newSlot)
    {
        equipmentSlotUI.UpdateSlot(newSlot);
    }

    public void UpdateSlots(Slot[] slots)
    {
        for (int i = 0; i < slots.Length && i < slotsUI.Length; i++)
        {
            if (slots[i] != null)
            {
                slotsUI[i].UpdateSlot(slots[i]);
            }
            else
            {
                slotsUI[i].ClearSlot();
            }
        }
    }
}
