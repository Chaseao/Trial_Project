using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DisplayUI))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventorySystem connectedInventory;
    [SerializeField] DisplayUI displayUI;
    int totalSlotsInSet;
    int totalSetsOfSlots;

    Slot[][] slots;
    int currentSlotSet = 0;

    List<GameObject> children = new List<GameObject>();

    private void Start()
    {
        foreach (CanvasRenderer childDisplayed in transform.GetComponentsInChildren<CanvasRenderer>())
        {
            if (!childDisplayed.gameObject.GetComponent<InventorySlotUI>())
            {
                children.Add(childDisplayed.gameObject);
            }
        }

        HideInventory();
        UpdateInventoryUI();
        UpdateEquipmentUI();
    }

    private void OnEnable()
    {
        InventoryEvents.ItemGained += UpdateInventoryUI;
        InventoryEvents.MultipleItemsGained += UpdateInventoryUI;
        InventoryEvents.ItemLost += UpdateInventoryUI;
        InventoryEvents.EquipCrystal += UpdateEquipmentUI;
        InventoryEvents.InventoryOpenned += ShowInventory;
        InventoryEvents.InventoryClosed += HideInventory;
    }

    private void OnDisable()
    {
        InventoryEvents.ItemGained -= UpdateInventoryUI;
        InventoryEvents.ItemGained -= UpdateInventoryUI;
        InventoryEvents.ItemLost -= UpdateInventoryUI;
        InventoryEvents.EquipCrystal -= UpdateEquipmentUI;
        InventoryEvents.InventoryOpenned -= ShowInventory;
        InventoryEvents.InventoryClosed -= HideInventory;
    }

    private void CreateArray()
    {
        slots = new Slot[totalSetsOfSlots][];
        for(int i = 0; i < totalSetsOfSlots; i++)
        {
            slots[i] = new Slot[totalSlotsInSet];
        }
    }

    public void UpdateEquipmentUI()
    {
        if (connectedInventory.CurrentlyEquippedCrystal != null)
        {
            Slot newSlot = new Slot(connectedInventory.CurrentlyEquippedCrystal, 1);
            displayUI.SetEquipmentSlot(newSlot);
            UpdateInventoryUI();
        }
    }

    public void UpdateEquipmentUI(Item notNeeded)
    {
        UpdateEquipmentUI();
    }

    public void UpdateInventoryUI()
    {
        Dictionary<Item, int> inventory = connectedInventory.GetItemsInInventory();
        totalSlotsInSet = displayUI.GetAmountOfSlots();
        totalSetsOfSlots = 1 + connectedInventory.TotalAmountOfSlots / totalSlotsInSet;
        CreateArray();
        int i = 0;
        int j = 0;

        foreach (Item item in inventory.Keys)
        {
            Slot slot = new Slot(item, inventory[item]);
            slots[j][i] = slot;
            i++;

            if (i >= totalSlotsInSet)
            {
                i = 0;
                j++;
            }

            if (j >= totalSetsOfSlots)
            {
                break;
            }
        }
        displayUI.UpdateSlots(slots[currentSlotSet]);
    }

    public void UpdateInventoryUI(Item notNeeded = null)
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI(Item notNeeded = null, int alsoNotNeeded = 1)
    {
        UpdateInventoryUI();
    }

    private void HideInventory()
    {
        foreach(GameObject child in children)
        {
            child.SetActive(false);
        }
    }

    private void ShowInventory()
    {
        foreach (GameObject child in children)
        {
            child.SetActive(true);
        }
    }

    public void PreviousInventorySlots()
    {
        if(currentSlotSet > 0)
        {
            currentSlotSet--;
        }

        displayUI.UpdateSlots(slots[currentSlotSet]);
    }

    public void NextInventorySlots()
    {
        if(currentSlotSet < totalSetsOfSlots - 1)
        {
            currentSlotSet++;
        }

        displayUI.UpdateSlots(slots[currentSlotSet]);
    }
}
